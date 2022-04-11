using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using ShoesV6.Properties;
using System.IO;
using Automation.BDaq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace ShoesV6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ShareArea.cameraSetting.Handle = pictureBox1.Handle;
            ShareArea.AdsClient.ConnectionStateChanged += AdsClient_ConnectionStateChanged;
            ShareArea.AdsClient.AdsNotificationEx += ADSClient_AdsNotificationEx;
            LoadOtherPara();
            Thread_updateUI = new Thread(UIevent);
            Thread_updateUI.IsBackground = true;
            Thread_updateUI.Start();
            Thread_scan = new Thread(scan);
            Thread_scan.IsBackground = true;
            Thread_Robot = new Thread(checkKUKA);
            Thread_Robot.IsBackground = true;
            Thread_Sendpath = new Thread(Sendpath);
            Thread_Sendpath.IsBackground = true;
            Thread_CamConnect = new Thread(ShareArea.cameraSetting.CamClassIni);
            Thread_CamConnect.IsBackground = true;
            Thread_Ads = new Thread(Adsconnect);
            Thread_Ads.IsBackground = true;
            Thread_sample = new Thread(scan);
            Thread_sample.IsBackground = true;
            Thread_test = new Thread(scantest);
            Thread_test.IsBackground = true;
            DeviceConnect();
            CheckFileExist();
            refresh_comboBox_Cameramodel();
            refresh_comboBox_ModelSize();
            ShareArea.txtLog.WriteLog(1, "程式初始化成功");
        }
        Thread Thread_updateUI, Thread_scan, Thread_Robot, Thread_Sendpath, Thread_CamConnect, Thread_Ads, Thread_DeviceConnect, Thread_sample;
        Thread Thread_test;
        iniHelper ini = new iniHelper();
        InstantDiCtrl DiCtrl = new InstantDiCtrl();
        Stopwatch sw_sensor1 = new Stopwatch();
        Stopwatch sw_sensor2 = new Stopwatch();
        uint hConnect = new uint();
        uint heartbeat, heartbeatHandle, RobotEGunNoUse;
        double ShoeSize;
        public delegate void IOChange(bool state);
        public event IOChange IOchange;
        bool _IOstate = false;
        private bool IOConnectSender
        {
            set
            {
                if (_IOstate != value)
                {
                    IOchange(value);
                    ShareArea.Connectstate[3] = value;
                }
                _IOstate = value;
            }
        }
        private void refresh_comboBox_Cameramodel()
        {
            comboBox_Cameramodel.Items.Clear();
            string[] a = ini.SectionNames("combobox.ini");
            a = ini.SectionNames("combobox_cam.ini");
            if (a != null)
            {
                for (int i = 0; i < a.Count(); i++)
                {
                    string N = ini.IniReadValue(a[i], "Name", "combobox_cam.ini");
                    comboBox_Cameramodel.Items.Add(N);
                }
            }
        }
        private void refresh_comboBox_ModelSize()
        {
            comboBox_ModelSize.Items.Clear();
            string[] a = ini.SectionNames("combobox.ini");
            if (a != null)
            {
                for (int i = 0; i < a.Count(); i++)
                {
                    string N = ini.IniReadValue(a[i], "Name", "combobox.ini");
                    comboBox_model.Items.Add(N);
                    comboBox_ModelSize.Items.Add(N);
                }
            }
        }
        private void btn_CamConnect_Click(object sender, EventArgs e)
        {
            if (!Thread_CamConnect.IsAlive && !ShareArea.Connectstate[1])
            {
                ShareArea.cameraSetting.ChangeEvent_Cam += CameraSetting_ChangeEvent_Cam;
                Thread_CamConnect = new Thread(ShareArea.cameraSetting.CamClassIni);
                Thread_CamConnect.IsBackground = true;
                Thread_CamConnect.Start();
            }
            //if (!Thread_updateUI.IsAlive)
            //    Thread_updateUI.Start();
        }

        private void DeviceConnect()
        {
            btn_CamConnect_Click(null, null);
            btn_IOConnect_Click(null, null);
            btn_TC_Click(null, null);
        }
        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl2.SelectedTab == EM)
            {
                text_Threshold.Text = ShareArea.Threshold.ToString();
                text_Dilate.Text = ShareArea.Dilate.ToString();
                text_Erode.Text = ShareArea.Erode.ToString();
                text_shiftX.Text = ShareArea.shiftX.ToString();
                text_shiftY.Text = ShareArea.shiftY.ToString();
                text_shrink.Text = ShareArea.shrink.ToString();
                text_shiftHeight.Text = ShareArea.shiftHeight.ToString();
                //text_angle.Text = ShareArea.angle.ToString();
                textBox_Exposure.Text = ShareArea.Exposure.ToString();
            }
        }
        private void LoadOtherPara()
        {
            ShareArea.Threshold = int.Parse(ini.IniReadValue("Other", "Threshold", "OtherConfig.ini"));
            ShareArea.Dilate = int.Parse(ini.IniReadValue("Other", "Dilate", "OtherConfig.ini"));
            ShareArea.Erode = int.Parse(ini.IniReadValue("Other", "Erode", "OtherConfig.ini"));
            ShareArea.par_y = double.Parse(ini.IniReadValue("Other", "par_y", "OtherConfig.ini"));
            ShareArea.shiftY = int.Parse(ini.IniReadValue("Other", "shiftY", "OtherConfig.ini"));
            ShareArea.shiftX = int.Parse(ini.IniReadValue("Other", "shiftX", "OtherConfig.ini"));
            ShareArea.shrink = int.Parse(ini.IniReadValue("Other", "shrink", "OtherConfig.ini"));
            ShareArea.shiftHeight = int.Parse(ini.IniReadValue("Other", "shiftHeight", "OtherConfig.ini"));
            //ShareArea.angle = int.Parse(ini.IniReadValue("Other", "angle", "OtherConfig.ini"));
            ShareArea.Exposure = double.Parse(ini.IniReadValue("Camera", "Exposure", "CamConfig.ini"));
        }

        private void btn_SavePara_Click(object sender, EventArgs e)
        {
            ini.IniWriteValue("Other", "Threshold", text_Threshold.Text, "OtherConfig.ini");
            ini.IniWriteValue("Other", "Dilate", text_Dilate.Text, "OtherConfig.ini");
            ini.IniWriteValue("Other", "Erode", text_Erode.Text, "OtherConfig.ini");
            ini.IniWriteValue("Other", "shrink", text_shrink.Text, "OtherConfig.ini");
            ini.IniWriteValue("Other", "shiftY", text_shiftY.Text, "OtherConfig.ini");
            ini.IniWriteValue("Other", "shiftX", text_shiftX.Text, "OtherConfig.ini");
            ini.IniWriteValue("Other", "shiftHeight", text_shiftHeight.Text, "OtherConfig.ini");
            ini.IniWriteValue("Other", "angle", text_angle.Text, "OtherConfig.ini");
        }

        private void btn_RobConnect_Click(object sender, EventArgs e)
        {
            if (!Thread_Robot.IsAlive)
            {
                Thread_Robot = new Thread(checkKUKA);
                Thread_Robot.IsBackground = true;
                Thread_Robot.Start();
            }
            //if (!Thread_updateUI.IsAlive)
            //    
        }
        private void btn_IOConnect_Click(object sender, EventArgs e)
        {
            string IOfilepath = startpath + @"\ini\IOdata.xml";
            ErrorCode err = ErrorCode.Success;
            try
            {
                DiCtrl.SelectedDevice = new DeviceInformation(1);
            }
            catch (Exception ex)
            {
                ShareArea.txtLog.WriteLog(1, "IO:" + ex);
                MessageBox.Show("請確認IO連線");
                return;
            }
            DiCtrl.LoadProfile(IOfilepath);
            DiCtrl.Interrupt += new System.EventHandler<Automation.BDaq.DiSnapEventArgs>(this.instantDiCtrl1_Interrupt);
            DiintChannel[] diintChannels = DiCtrl.DiintChannels;
            diintChannels[0].Enabled = true;
            diintChannels[1].Enabled = true;
            err = DiCtrl.SnapStart();
            IOchange += Form1_IOchange;
            IOConnectSender = true;
        }


        private void btn_VerifyPath_Click(object sender, EventArgs e)
        {
            VerifyPath verifyPath = new VerifyPath();
            verifyPath.ShowDialog();
            verifyPath.Close();
            GC.Collect();
        }

        private void comboBox_ModelSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] Allfilecollection;
            Allfilecollection = Directory.GetFiles(startpath + @"\shiftdata\" + comboBox_ModelSize.Text + @"\", "*.txt");
            ShareArea.shiftdatas.Clear();
            foreach (string path in Allfilecollection)
            {
                string[] filename = path.Split(new string[] { startpath + @"\shiftdata\" + comboBox_ModelSize.Text + @"\" }, StringSplitOptions.RemoveEmptyEntries);
                string line;
                string[] result;
                ShareArea.shiftdata shiftdata = new ShareArea.shiftdata();
                shiftdata.name = filename[0];
                shiftdata.shift = new List<double[]>();
                using (StreamReader sr = new StreamReader(path))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        double[] datas = new double[9];
                        result = line.Split(',');
                        datas[0] = Convert.ToDouble(result[0]);
                        datas[1] = Convert.ToDouble(result[1]);
                        datas[2] = Convert.ToDouble(result[2]);
                        datas[3] = Convert.ToDouble(result[3]);
                        datas[4] = Convert.ToDouble(result[4]);
                        datas[5] = Convert.ToDouble(result[5]);
                        datas[6] = Convert.ToDouble(result[6]);
                        datas[7] = Convert.ToDouble(result[7]);
                        datas[8] = Convert.ToDouble(result[8]);
                        shiftdata.shift.Add(datas);
                    }
                }
                ShareArea.shiftdatas.Add(shiftdata);
            }

            ShareArea.txtLog.WriteLog(1, "載入" + comboBox_ModelSize.Text + "資料成功");
            btn_Scan.Enabled = true;
        }

        private void btn_addSample_Click(object sender, EventArgs e)
        {

            if (comboBox_model.Text == "" || comboBox_direction.Text == "" || comboBox_size.Text == "")
            {
                MessageBox.Show("請確認型號與規格");
                return;
            }
            else if (!Directory.Exists(startpath + @"\shiftdata\" + comboBox_model.Text))
            {
                Directory.CreateDirectory(startpath + @"\shiftdata\" + comboBox_model.Text);
                if (!Directory.Exists(startpath + @"\path\" + comboBox_model.Text))
                {
                    Directory.CreateDirectory(startpath + @"\path\" + comboBox_model.Text);

                }
                comboBox_model.Items.Add(comboBox_model.Text);
                ini.IniWriteValue(comboBox_model.Text, "Name", comboBox_model.Text, "combobox.ini");
            }

            if (!File.Exists(startpath + @"\shiftdata\" + comboBox_model.Text + @"\" + comboBox_size.Text + comboBox_direction.Text + ".txt") && comboBox_size.Text != "" && comboBox_direction.Text != "")
            {
                File.Create(startpath + @"\shiftdata\" + comboBox_model.Text + @"\" + comboBox_size.Text + comboBox_direction.Text + ".txt").Close();
            }
            refreshDataGridView();
        }
        private void Adsconnect()
        {
            while (!ShareArea.AdsClient.IsConnected)
            {
                try
                {
                    ShareArea.AdsClient.Connect("5.98.66.181.1.1", 851);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    ShareArea.AdsClient.Disconnect();
                }
                Thread.Sleep(500);
            }
        }
        private void AdsClient_ConnectionStateChanged(object sender, TwinCAT.ConnectionStateChangedEventArgs e)
        {
            if (e.NewState == TwinCAT.ConnectionState.Disconnected)
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    pictureBox_IOConnect.BackgroundImage = Resources.icons8_red_circle_16;
                });
                ShareArea.txtLog.WriteLog(1, "與主控重新連線");
                if (!Thread_Ads.IsAlive)
                {
                    Thread_Ads = new Thread(Adsconnect);
                    Thread_Ads.IsBackground = true;

                    Thread_Ads.Start();
                }
                else
                    return;

                Thread.Sleep(250);
            }
            else if (e.NewState == TwinCAT.ConnectionState.Connected)
            {
                hConnect = ShareArea.AdsClient.AddDeviceNotificationEx("X400Global.X42A", new TwinCAT.Ads.NotificationSettings(TwinCAT.Ads.AdsTransMode.OnChange, 100, 0), null, typeof(bool));
                ShoeSize = ShareArea.AdsClient.AddDeviceNotificationEx("OutsolePC.OutsoleSize", new TwinCAT.Ads.NotificationSettings(TwinCAT.Ads.AdsTransMode.OnChange, 100, 0), null, typeof(double));
                RobotEGunNoUse = ShareArea.AdsClient.AddDeviceNotificationEx("RobotGlobal.RobotEGunNoUse", new TwinCAT.Ads.NotificationSettings(TwinCAT.Ads.AdsTransMode.OnChange, 100, 0), null, typeof(bool));
                heartbeatHandle = ShareArea.AdsClient.CreateVariableHandle("OutsolePC.OutsolePCHeartBeat");
                ShareArea.txtLog.WriteLog(1, "主控連線成功");

                this.Invoke((MethodInvoker)delegate ()
                {
                    Heartbeat_Timer.Enabled = true;
                    pictureBox_IOConnect.BackgroundImage = Resources.icons8_green_circle_16;
                });
            }
        }

        private void Heartbeat_Timer_Tick(object sender, EventArgs e)
        {
            heartbeat++;
            try
            {
                ShareArea.AdsClient.WriteAny(heartbeatHandle, heartbeat);
            }
            catch (Exception ex)
            {
                ShareArea.txtLog.WriteLog(1, "主控連線關閉");
                Console.WriteLine(ex);
                Heartbeat_Timer.Enabled = false;
                ShareArea.AdsClient.Disconnect();
            }
        }

        private void btn_pathadj_Click(object sender, EventArgs e)
        {
            Formpathadj formpathadj = new Formpathadj();
            if (formpathadj.Enabled)
                formpathadj.ShowDialog();
            formpathadj.Close();
        }

        private void btn_Scan_Click(object sender, EventArgs e)
        {
            if (Thread_sample.IsAlive)
                Thread_sample.Abort();
            if (!Thread_scan.IsAlive)
            {
                Thread_scan.Start();
            }
        }
        private void btn_startsample_Click(object sender, EventArgs e)
        {
            if (!File.Exists(startpath + @"\shiftdata\" + comboBox_model.Text + @"\" + comboBox_size.Text + comboBox_direction.Text + ".txt"))
            {
                MessageBox.Show("未建立模型");
                return;
            }
            if (Thread_scan.IsAlive)
                Thread_scan.Abort();
            if (!Thread_sample.IsAlive)
            {
                Thread_sample = new Thread(scan);
                Thread_sample.IsBackground = true;
                Thread_sample.Start("sample");
                btn_resample.Enabled = true;
            }
            Thread_sample.Join();
            btn_pathadj_Click(null, null);
        }
        private void checkKUKA()
        {
            ShareArea.KUKA_EKI.ChangeEvent += KUKA_EKI_ChangeEvent;
            ShareArea.KUKA_EKI.IP = "192.168.10.125";
            ShareArea.KUKA_EKI.Port = 54600;
            ShareArea.KUKA_EKI.Timeout = 5000;
            ShareArea.KUKA_EKI.EKI_Connect();
            if (!Thread_Sendpath.IsAlive)
                Thread_Sendpath.Start();
        }

        private void Sendpath()
        {
            List<double[]> Robotabc = new List<double[]>();
            List<double[]> RobotPosition = new List<double[]>();
            int pathC = 0;
            while (true)
            {
                Thread.Sleep(250);
                string path = "<Robot><Path>";
                if (ShareArea.KUKA_EKI.KukaIdle && !ShareArea.isshifpath && ShareArea.XYZ_Queue.Count() > 0)
                {
                    ShareArea.XYZ_Queue.TryDequeue(out RobotPosition);
                    ShareArea.ABC_Queue.TryDequeue(out Robotabc);
                    for (int j = 0; j < Robotabc.Count(); j++)
                    {
                        path = path + "<frame X=\"" + RobotPosition[j][0] + "\" Y=\"" + RobotPosition[j][1] + "\" Z=\"" + RobotPosition[j][2] + "\" A=\"" + Robotabc[j][0] + "\" B=\"" + Robotabc[j][1] + "\" C=\"" + Robotabc[j][2] + "\"></frame>";
                    }
                    path = path + "<xyzabcFrameCount>" + Robotabc.Count.ToString() + "</xyzabcFrameCount>" + "<check>" + 1 + "</check>";
                    path = path + "<Tool>" + "3" + "</Tool>" + "<Base>" + "1" + "</Base>" + "</Path>" + "</Robot>";
                    ShareArea.KUKA_EKI.Send(path);
                    pathC++;
                    Console.WriteLine("pathC=" + pathC);
                    Robotabc.Clear();
                    RobotPosition.Clear();
                    string[] Title = { "" };
                    System.IO.File.WriteAllLines(startpath + @"\XML.txt", Title);
                    using (StreamWriter strw = new StreamWriter(startpath + @"\XML.txt", false))
                    {
                        strw.WriteLine(path);
                        for (int i = 0; i < RobotPosition.Count(); i++) ;
                    }
                }
            }
        }
        private void ADSClient_AdsNotificationEx(object sender, TwinCAT.Ads.AdsNotificationExEventArgs e)
        {
            if (e.Handle == hConnect && (bool)e.Value == true)
            {
                btn_RobConnect_Click(null, null);
            }
            if (e.Handle == ShoeSize)
            {
                ShareArea.size = (double)e.Value;
            }
            if (e.Handle == RobotEGunNoUse && ShareArea.Connectstate[0] == true)
            {
                if ((bool)e.Value == true)
                    ShareArea.KUKA_EKI.Send("<Robot><Gun>0</Gun></Robot>");
                else
                    ShareArea.KUKA_EKI.Send("<Robot><Gun>1</Gun></Robot>");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox_TriggerMode.Checked)
            {
                ShareArea.cameraSetting.CloseTriggerMode();
            }
            else
            {
                ShareArea.cameraSetting.OpenTriggerMode();
            }
        }

        private void textBox_Exposure_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double para;
                try
                {
                    para = double.Parse(textBox_Exposure.Text);
                    ShareArea.cameraSetting.changeExposure(para);
                    ShareArea.Exposure = para;
                }
                catch
                {
                    MessageBox.Show("請輸入數字");
                }

            }
        }


        private void btn_TC_Click(object sender, EventArgs e)
        {
            if (!Thread_Ads.IsAlive)
                Thread_Ads.Start();
        }

        private void instantDiCtrl1_Interrupt(object sender, Automation.BDaq.DiSnapEventArgs e)
        {
            if (e.SrcNum == 0 && sw_sensor1.ElapsedMilliseconds == 0)
            {
                ShareArea.input[0] = true;
                sw_sensor1.Start();
            }
            else if (e.SrcNum == 8 && sw_sensor2.ElapsedMilliseconds == 0)
            {
                ShareArea.input[1] = true;
                sw_sensor2.Start();
            }
            if (sw_sensor1.ElapsedMilliseconds > 5000)
                sw_sensor1.Reset();
            else if (sw_sensor2.ElapsedMilliseconds > 5000)
                sw_sensor2.Reset();
        }

        #region UIevent
        private void UIevent()
        {
            while (true)
            {
                if (ShareArea.Connectstate[1])
                    ShareArea.cameraSetting.Cam_ConnectCheck();
                if (ShareArea.Connectstate[3])
                    IO_ConnectCheck();
                Thread.Sleep(1000);
            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ShareArea.size = Convert.ToDouble(comboBox1.Text);
        }

        private void text_Threshold_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btn_savecamrecipe_Click(object sender, EventArgs e)
        {
            if (comboBox_Cameramodel.Text == "")
            {
                MessageBox.Show("請輸入型號");
                return;
            }
            else if (!File.Exists(startpath + @"\camerarecipe\" + comboBox_Cameramodel.Text + ".txt"))
            {
                File.Create(startpath + @"\camerarecipe\" + comboBox_Cameramodel.Text + ".txt").Close();
            }
            try
            {
                int.Parse(text_Threshold.Text);
                int.Parse(text_angle.Text);
                int.Parse(text_shiftHeight.Text);
                int.Parse(text_shiftY.Text);
                int.Parse(text_shiftX.Text);
                int.Parse(text_shrink.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("請輸入數字");
                return;
            }
            ini.IniWriteValue(comboBox_Cameramodel.Text, "Name", comboBox_Cameramodel.Text, "combobox_cam.ini");
            using (System.IO.StreamWriter sr = new System.IO.StreamWriter(startpath + @"\camerarecipe\" + comboBox_Cameramodel.Text + ".txt", false))
            {
                string para = text_Threshold.Text + "," + text_shrink.Text + "," + text_shiftX.Text + "," + text_shiftY.Text + "," +
                    text_shiftHeight.Text + "," + text_angle.Text + ",";
                sr.WriteLine(para);
            }
            refresh_comboBox_Cameramodel();
        }

        private void comboBox_Cameramodel_SelectedIndexChanged(object sender, EventArgs e)
        {
            string line;
            string[] result;
            try
            {
                using (StreamReader sr = new StreamReader(startpath + @"\camerarecipe\" + comboBox_Cameramodel.Text + ".txt"))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        result = line.Split(',');
                        ShareArea.Threshold = Convert.ToInt16(result[0]);
                        ShareArea.shrink = Convert.ToInt16(result[1]);
                        ShareArea.shiftX = Convert.ToInt16(result[2]);
                        ShareArea.shiftY = Convert.ToInt16(result[3]);
                        ShareArea.shiftHeight = Convert.ToInt16(result[4]);
                        ShareArea.angle = Convert.ToInt16(result[5]);
                        text_Threshold.Text = result[0];
                        text_shrink.Text = result[1];
                        text_shiftX.Text = result[2];
                        text_shiftY.Text = result[3];
                        text_shiftHeight.Text = result[4];
                        text_angle.Text = result[5];
                    }
                }
                ShareArea.txtLog.WriteLog(1, "載入" + comboBox_Cameramodel.Text + "相機參數成功");
                btn_Scan.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_removecamerarecipe_Click(object sender, EventArgs e)
        {
            if (comboBox_Cameramodel.Text != "")
            {
                if (File.Exists(startpath + @"\camerarecipe\" + comboBox_Cameramodel.Text + ".txt"))
                    File.Delete(startpath + @"\camerarecipe\" + comboBox_Cameramodel.Text + ".txt");
                ini.IniWriteValue(comboBox_Cameramodel.Text, null, null, "combobox_cam.ini");
                comboBox_Cameramodel.Items.Remove(comboBox_Cameramodel.Text);
                comboBox_Cameramodel.Text = "";
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int para;
                try
                {
                    para = int.Parse(textBox1.Text);
                    ShareArea.shiftX = para;
                }
                catch
                {
                    MessageBox.Show("請輸入數字");
                }

            }
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            FtpWebRequest request;
            byte[] fileContents;
            bool exist = CheckFTPFileExist(comboBox_model.Text, "ftp://192.168.10.100/GoldSample/Bottom/");
            try
            {
                if (!exist)
                {
                    request = (FtpWebRequest)WebRequest.Create("ftp://192.168.10.100/GoldSample/Bottom/" + comboBox_model.Text + "/" + comboBox_size.Text);
                    request.Credentials = new NetworkCredential("beckhoff", "12345");
                    request.KeepAlive = false;
                    request.Method = WebRequestMethods.Ftp.MakeDirectory;
                    using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                    {
                        Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
                    }
                }
                else
                {
                    exist = CheckFTPFileExist(comboBox_size.Text, "ftp://192.168.10.100/GoldSample/Bottom/" + comboBox_model.Text + "/");
                    if (!exist)
                    {
                        request = (FtpWebRequest)WebRequest.Create("ftp://192.168.10.100/GoldSample/Bottom/" + comboBox_model.Text + "/" + comboBox_size.Text);
                        request.Credentials = new NetworkCredential("beckhoff", "12345");
                        request.KeepAlive = false;
                        request.Method = WebRequestMethods.Ftp.MakeDirectory;
                        using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                        {
                            Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
                        }
                    }
                }

                request = (FtpWebRequest)WebRequest.Create("ftp://192.168.10.100/GoldSample/Bottom/" + comboBox_model.Text + "/" + comboBox_size.Text + "/" + comboBox_direction.Text + "LowPointHeight.pcd");
                request.Credentials = new NetworkCredential("beckhoff", "12345");
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.KeepAlive = false;
                using (StreamReader source = new StreamReader(startpath + "\\PCD\\LowPointHeight.pcd"))
                {
                    fileContents = Encoding.UTF8.GetBytes(source.ReadToEnd());
                }
                request.ContentLength = fileContents.Length;
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(fileContents, 0, fileContents.Length);
                }
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            GC.Collect();
        }

        private bool CheckFTPFileExist(string path, string remotepath)
        {
            FtpWebRequest request;
            StreamReader reader;
            string line;
            bool exist = false;
            request = (FtpWebRequest)WebRequest.Create(remotepath);
            request.Credentials = new NetworkCredential("beckhoff", "12345");
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.KeepAlive = false;

            using (WebResponse webResponse = request.GetResponse())
            {
                reader = new StreamReader(webResponse.GetResponseStream());
                line = reader.ReadLine();
                while (line != null)
                {
                    if (line == path)
                    {
                        exist = true;
                        break;
                    }
                    line = reader.ReadLine();
                }
            }
            if (reader != null)
                reader.Close();
            return exist;
        }
        private void CheckFileExist()
        {
            FtpWebRequest request;
            bool exist = CheckFTPFileExist(DateTime.Now.ToString("yyyy-MM-dd"), "ftp://192.168.10.100/");
            if (!exist)
            {
                request = (FtpWebRequest)WebRequest.Create("ftp://192.168.10.100/" + DateTime.Now.ToString("yyyy-MM-dd"));
                request.Credentials = new NetworkCredential("beckhoff", "12345");
                request.KeepAlive = false;
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
                }
            }
            else
            {
                exist = CheckFTPFileExist("Bottom", "ftp://192.168.10.100/" + DateTime.Now.ToString("yyyy-MM-dd") + "/");
                if (!exist)
                {
                    request = (FtpWebRequest)WebRequest.Create("ftp://192.168.10.100/" + DateTime.Now.ToString("yyyy-MM-dd") + "/Bottom");
                    request.Credentials = new NetworkCredential("beckhoff", "12345");
                    request.KeepAlive = false;
                    request.Method = WebRequestMethods.Ftp.MakeDirectory;
                    using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                    {
                        Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
                    }
                }
            }
            if (!Directory.Exists(startpath + @"\PCD\" + DateTime.Now.ToString("yyyy-MM-dd")))
            {
                Directory.CreateDirectory(startpath + @"\PCD\" + DateTime.Now.ToString("yyyy-MM-dd"));
            }
            if (!Directory.Exists(startpath + @"\MovePath\" + DateTime.Now.ToString("yyyy-MM-dd")))
            {
                Directory.CreateDirectory(startpath + @"\MovePath\" + DateTime.Now.ToString("yyyy-MM-dd"));
            }
            //Directory.Delete(startpath + $@"\MovePath\2022-03-12 - 複製", true);
            DirectoryInfo directoryInfo = new DirectoryInfo(startpath + $@"\MovePath");
            var di = directoryInfo.GetDirectories().OrderBy(n => n.CreationTime).ToArray();
            if (di.Count() > 30)
            {
                Directory.Delete(di[0].FullName, true);
            }
            directoryInfo = new DirectoryInfo(startpath + $@"\PCD");
            di = directoryInfo.GetDirectories().OrderBy(n => n.CreationTime).ToArray();
            if (di.Count() > 3)
            {
                Directory.Delete(di[0].FullName, true);
            }
            directoryInfo = new DirectoryInfo(startpath + $@"\Log");
            var fi = directoryInfo.GetFiles().OrderBy(n => n.CreationTime).ToArray();
            if (fi.Count() > 30)
            {
                File.Delete(fi[0].FullName);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            string[] Allfilecollection;
            string[] filename;
            Allfilecollection = Directory.GetFiles($@"C:\Users\user\Desktop\ShoesV6\ShoesV6\bin\x64\Debug\MovePath\{DateTime.Now.ToString("yyyy-MM-dd")}", "*.txt");
            sw2.Start();
            foreach (var filePath in Allfilecollection)
            {
                filename = Regex.Split(filePath, $@"C:\\Users\\user\\Desktop\\ShoesV6\\ShoesV6\\bin\\x64\\Debug\\MovePath\\{DateTime.Now.ToString("yyyy-MM-dd")}\\");
                UpLoadMovePath(filename[1]);
            }
            sw2.Stop();
            Console.WriteLine(sw2.ElapsedMilliseconds);

        }

        private void text_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (e.KeyCode == Keys.Enter)
            {
                int para;
                switch (textBox.Name)
                {
                    case "text_Threshold":
                        try
                        {
                            para = int.Parse(text_Threshold.Text);
                            ShareArea.Threshold = para;
                        }
                        catch
                        {
                            MessageBox.Show("請輸入數字");
                        }
                        break;
                    case "text_shrink":
                        try
                        {
                            para = int.Parse(text_shrink.Text);
                            ShareArea.shrink = para;
                        }
                        catch
                        {
                            MessageBox.Show("請輸入數字");
                        }
                        break;
                    case "text_shiftX":
                        try
                        {
                            para = int.Parse(text_shiftX.Text);
                            ShareArea.shiftX = para;
                        }
                        catch
                        {
                            MessageBox.Show("請輸入數字");
                        }
                        break;
                    case "text_shiftY":
                        try
                        {
                            para = int.Parse(text_shiftY.Text);
                            ShareArea.shiftY = para;
                        }
                        catch
                        {
                            MessageBox.Show("請輸入數字");
                        }
                        break;
                    case "text_shiftHeight":
                        try
                        {
                            para = int.Parse(text_shiftHeight.Text);
                            ShareArea.shiftHeight = para;
                        }
                        catch
                        {
                            MessageBox.Show("請輸入數字");
                        }
                        break;
                    case "text_angle":
                        try
                        {
                            para = int.Parse(text_angle.Text);
                            ShareArea.angle = para;
                        }
                        catch
                        {
                            MessageBox.Show("請輸入數字");
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Thread_test.IsAlive)
            {
                Thread_test.Start();
            }
        }

        private void UpLoadMovePath(string filename)
        {
            FtpWebRequest request;
            byte[] fileContents;
            request = (FtpWebRequest)WebRequest.Create("ftp://192.168.10.100/" + DateTime.Now.ToString("yyyy-MM-dd") + "/Bottom/" + filename);
            request.Credentials = new NetworkCredential("beckhoff", "12345");
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.KeepAlive = false;
            using (StreamReader source = new StreamReader($@"C:\Users\user\Desktop\ShoesV6\ShoesV6\bin\x64\Debug\MovePath\{DateTime.Now.ToString("yyyy-MM-dd")}\" + filename))
            {
                fileContents = Encoding.UTF8.GetBytes(source.ReadToEnd());
            }
            request.ContentLength = fileContents.Length;
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(fileContents, 0, fileContents.Length);
            }
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
            }
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (comboBox_model.Text != "" && comboBox_size.Text == "" && comboBox_direction.Text == "")
            {
                if (Directory.Exists(startpath + @"\shiftdata\" + comboBox_model.Text))
                {
                    //string[] strtemp = Directory.GetFiles(startpath + @"\shiftdata\" + comboBox_model.Text + @"\");
                    //foreach (string path in strtemp)
                    //{
                    //    File.Delete(path);
                    //}
                    Directory.Delete(startpath + @"\shiftdata\" + comboBox_model.Text, true);
                }
                if (Directory.Exists(startpath + @"\path\" + comboBox_model.Text))
                {

                    //string[] strtemp = Directory.GetFiles(startpath + @"\path\" + comboBox_model.Text + @"\");
                    //foreach (string path in strtemp)
                    //{
                    //    File.Delete(path);
                    //}
                    Directory.Delete(startpath + @"\path\" + comboBox_model.Text, true);
                }
                ini.IniWriteValue(comboBox_model.Text, null, null, "combobox.ini");
                comboBox_model.Items.Remove(comboBox_model.Text);
                comboBox_model.Text = "";
                refresh_comboBox_ModelSize();
            }
            else if (comboBox_model.Text != "" && comboBox_size.Text != "" && comboBox_direction.Text != "")
            {
                if (File.Exists(startpath + @"\shiftdata\" + comboBox_model.Text + @"\" + comboBox_size.Text + comboBox_direction.Text + ".txt"))
                    File.Delete(startpath + @"\shiftdata\" + comboBox_model.Text + @"\" + comboBox_size.Text + comboBox_direction.Text + ".txt");
            }
            refreshDataGridView();
        }


        private void btn_resample_Click(object sender, EventArgs e)
        {
            try
            {
                ShareArea.Threshold = int.Parse(text_Threshold.Text);
                ShareArea.angle = int.Parse(text_angle.Text);
                ShareArea.shiftHeight = int.Parse(text_shiftHeight.Text);
                ShareArea.shiftY = int.Parse(text_shiftY.Text);
                ShareArea.shiftX = int.Parse(text_shiftX.Text);
                ShareArea.shrink = int.Parse(text_shrink.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show("請輸入數字");
                return;
            }
            if (Thread_scan.IsAlive)
                Thread_scan.Abort();
            if (!Thread_sample.IsAlive)
            {
                Thread_sample = new Thread(resample);
                Thread_sample.IsBackground = true;
                Thread_sample.Start();
            }
            Thread_sample.Join();
            btn_pathadj_Click(null, null);
        }

        private void KUKA_EKI_ChangeEvent(bool state)
        {
            if (state)
            {
                ShareArea.txtLog.WriteLog(1, "Robot 連線成功");
                pictureBox_KUKA.BackgroundImage = Resources.icons8_green_circle_16;
            }
            else
            {
                ShareArea.txtLog.WriteLog(1, "Robot 連線斷線");
                pictureBox_KUKA.BackgroundImage = Resources.icons8_red_circle_16;
            }
        }
        private void CameraSetting_ChangeEvent_Cam(bool state)
        {
            if (state)
            {
                ShareArea.txtLog.WriteLog(1, "Camera 連線成功");
                pictureBox_Cam.BackgroundImage = Resources.icons8_green_circle_16;
            }
            else
            {
                ShareArea.txtLog.WriteLog(1, "Camera 連線斷線");
                pictureBox_Cam.BackgroundImage = Resources.icons8_red_circle_16;
            }

        }
        private void Form1_IOchange(bool state)
        {
            if (state)
            {
                pictureBox_IOConnect.BackgroundImage = Resources.icons8_green_circle_16;
                ShareArea.txtLog.WriteLog(1, "IO 連線成功");
            }
            else
            {
                pictureBox_IOConnect.BackgroundImage = Resources.icons8_red_circle_16;
                ShareArea.txtLog.WriteLog(1, "IO 連線斷線");
            }
        }

        private void IO_ConnectCheck()
        {
            byte portData = 0;
            ErrorCode err = ErrorCode.ErrorBufferIsNull;
            err = DiCtrl.Read(0, out portData);
            if (err == ErrorCode.Success)
                IOConnectSender = true;
            else
            {
                IOConnectSender = false;
                DiCtrl.SnapStop();
                MessageBox.Show("IO斷線請重新連線");
            }
        }
        private void refreshDataGridView()
        {
            dataGridView.Rows.Clear();
            string[] Allfilecollection;
            try
            {
                Allfilecollection = Directory.GetFiles(startpath + @"\shiftdata\" + comboBox_model.Text + @"\", "*.txt");

                foreach (string path in Allfilecollection)
                {
                    string[] filename = path.Split(new string[] { startpath + @"\shiftdata\" + comboBox_model.Text + @"\", ".txt" }, StringSplitOptions.RemoveEmptyEntries);
                    dataGridView.Rows.Add(filename[0]);
                }
            }
            catch { }
        }
        private void comboBox_model_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox_model.Text != "" && comboBox_size.Text != "" && comboBox_direction.Text != "")
                ShareArea.name = $@"{comboBox_model.Text}\{comboBox_size.Text + comboBox_direction.Text}";
            switch (comboBox.Name)
            {
                case "comboBox_model":
                    refreshDataGridView();
                    label10.Text = comboBox_model.Text;
                    label10.Visible = true;
                    comboBox_size.Enabled = true;
                    break;
                case "comboBox_size":
                    comboBox_direction.Enabled = true;
                    break;
                case "comboBox_direction":
                    break;
                default:
                    break;
            }
        }

        #endregion


    }
}
