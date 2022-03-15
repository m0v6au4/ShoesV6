using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using uEye;
using System.Windows.Forms;
using System.Threading;

namespace ShoesV6
{
    public class CamClass
    {
        public uEye.Types.CameraInformation[] cameraList;
        private IntPtr _handle;
        private List<int> camID;
        public Camera cam;
        private uEye.Defines.Status uEyeRET;
        private iniHelper ini = new iniHelper();
        private int w, h, x, y, PixelClock, FrameRate;
        private double Exposure;
        private bool _connectState = false;
        //private Thread Thread;
        public delegate void ChangeDelegate(bool state);
        public event ChangeDelegate ChangeEvent_Cam;

        private bool ConnectStateSender //相機 連線狀態
        {
            get
            {
                return _connectState;
            }
            set
            {
                if (_connectState != value)
                {
                    ChangeEvent_Cam(value);
                    ShareArea.Connectstate[1] = value;
                }
                _connectState = value;
            }
        }

        public IntPtr Handle
        {
            get
            {
                return _handle;
            }
            set
            {
                _handle = value;
            }
        }
        public void CamClassIni()
        {
            camID = new List<int>();
            uEye.Info.Camera.GetCameraList(out cameraList);
            LoadCameraPara();
            try
            {
                if ("UI514xCP-M".Equals(cameraList[0].Model))
                    camID.Add(cameraList[0].CameraID);
            }
            catch
            {
                MessageBox.Show("未找到相機");
                return;
            }

            Camerainit();

        }
        private void Camerainit()
        {
            cam = new Camera();
            try
            {
                uEyeRET = cam.Init(camID[0]);
            }
            catch
            {
                MessageBox.Show("相機型號錯誤");
                return;
            }
            if (uEyeRET != uEye.Defines.Status.Success)
            {
                MessageBox.Show("Camera Initial Filed");
                return;
            }
            uEyeRET = cam.PixelFormat.Set(uEye.Defines.ColorMode.Mono8);
            if (uEyeRET != uEye.Defines.Status.Success)
            {
                MessageBox.Show("Cemera Set PixelFormat Failed");
                return;
            }
            cam.Size.AOI.Set(x, y, w, h);
            cam.Timing.PixelClock.Set(PixelClock);
            cam.Timing.Framerate.Set(FrameRate);
            cam.Timing.Exposure.Set(Exposure);
            uEyeRET = cam.Memory.Allocate();
            Int32 s32MemID;
            uEyeRET = cam.Memory.GetLast(out s32MemID);
            uEyeRET = cam.Memory.SetActive(s32MemID);
            if (uEyeRET != uEye.Defines.Status.Success)
            {
                MessageBox.Show("Cemera Allocate Memory failed.");
                return;
            }

                                  
            OpenTriggerMode();           
            ConnectStateSender = true;
            //Thread = new Thread(Cam_ConnectCheck);
            //Thread.Start();
        }
        private void LoadCameraPara()
        {

            w = int.Parse(ini.IniReadValue("Camera", "Width", "CamConfig.ini"));
            h = int.Parse(ini.IniReadValue("Camera", "Height", "CamConfig.ini"));
            x = int.Parse(ini.IniReadValue("Camera", "X", "CamConfig.ini"));
            y = int.Parse(ini.IniReadValue("Camera", "Y", "CamConfig.ini"));
            PixelClock = int.Parse(ini.IniReadValue("Camera", "PixelClock", "CamConfig.ini"));
            FrameRate = int.Parse(ini.IniReadValue("Camera", "FrameRate", "CamConfig.ini"));
            Exposure = Convert.ToDouble(ini.IniReadValue("Camera", "Exposure", "CamConfig.ini"));
            string str = ini.IniReadValue("Calibrate", "hABC", "LaserConfig.ini");
            string[] hABC = str.Split(',');
            ShareArea.abc[0, 0] = double.Parse(hABC[0]);
            ShareArea.abc[0, 1] = double.Parse(hABC[1]);
            ShareArea.abc[0, 2] = double.Parse(hABC[2]);
            str = ini.IniReadValue("Calibrate", "rABC", "LaserConfig.ini");
            string[] rABC = str.Split(',');
            ShareArea.abc[1, 0] = double.Parse(rABC[0]);
            ShareArea.abc[1, 1] = double.Parse(rABC[1]);
            ShareArea.abc[1, 2] = double.Parse(rABC[2]);
            ShareArea.Collinear = int.Parse(ini.IniReadValue("Calibrate", "Collinear", "LaserConfig.ini"));
        }
        private void Live_Frame_Event(object sender, EventArgs e)
        {
            Camera cam = (Camera)sender;
            int s32MemID;
            cam.Memory.GetLast(out s32MemID);
            cam.Memory.Lock(s32MemID);
            cam.Display.Render(s32MemID, _handle, uEye.Defines.DisplayRenderMode.FitToWindow);
            cam.Memory.Unlock(s32MemID);
        }
        public void OpenTriggerMode()
        {
            cam.EventExtTrigger += Live_Frame_Event;
            cam.EventExtTrigger += SaveImage;
            uEyeRET = cam.Trigger.Prescaler.Frame.Set(4);
            uEyeRET = cam.Trigger.Set(uEye.Defines.TriggerMode.Lo_Hi);
            cam.Acquisition.Capture();
        }
        public void CloseTriggerMode()
        {
            cam.EventFrame += Live_Frame_Event;
            uEyeRET = cam.Trigger.Set(uEye.Defines.TriggerMode.Continuous);
            cam.Acquisition.Capture();
        }
        public void Camera_Exit()
        {
            cam.Exit();
        }
        private void SaveImage(object sender, EventArgs e)
        {
            Bitmap bmp;

            int s32MemID;
            if (ShareArea.isscan)
            {
                cam.Memory.GetLast(out s32MemID);
                cam.Memory.Lock(s32MemID);
                cam.Memory.ToBitmap(s32MemID, out bmp);
                ShareArea.bitmaps.Enqueue(new Bitmap(bmp));
                cam.Memory.Unlock(s32MemID);
            }
        }

        public void Cam_ConnectCheck()
        {
            uEye.Info.Camera.GetCameraList(out cameraList);
            bool result = cameraList.Count() > 0;

            ConnectStateSender = result;
        }
        public void changeExposure(double Exposure)
        {
            cam.Timing.Exposure.Set(Exposure);
        }
        
    }
}
