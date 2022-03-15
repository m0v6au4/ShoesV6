using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//ABB////////
using ABB.Robotics.Controllers.Discovery;
using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.FileSystemDomain;
using ABB.Robotics.Controllers.RapidDomain;
using ABB.Robotics.Controllers.IOSystemDomain;
using System.Threading;
using System.IO;
///////////

namespace ShoesV6
{
    class ABBRobot
    {
        //ABB////////
        Controller controller;
        Mastership Mastership;
        NetworkScanner controllerFinder;
        ControllerInfoCollection controllerInfoCollection;
        ABB.Robotics.Controllers.RapidDomain.Task task1;
        FileSystem fileSystem;
        DigitalSignal doDoLim = null;
        Signal DOLim = null;
        string modName = "Bottom";
        Thread Thread_SendPath;
        ///////////
        public void RobotConnect()
        {
            string localDir = Application.StartupPath;
            string remoteDir = "/hd0a/IRB1200_CV77/HOME/";
            try
            {

                this.controllerFinder = new NetworkScanner();
                this.controllerFinder.Scan();
                this.controllerInfoCollection = controllerFinder.Controllers;
                this.controller = new Controller(this.controllerInfoCollection[0]);
                this.controller.Logon(UserInfo.DefaultUser);

                this.fileSystem = this.controller.FileSystem;
                this.fileSystem.LocalDirectory = localDir;
                this.fileSystem.RemoteDirectory = remoteDir;
                if (controller.OperatingMode != ControllerOperatingMode.Auto)
                {
                    MessageBox.Show("請切到Auto模式");
                    return;
                }
                //ShareArea.Robotstate = true;           
                task1 = controller.Rapid.GetTask("T_ROB1");
                using (Mastership m = Mastership.Request(controller.Rapid))
                {

                    controller.Rapid.Stop(StopMode.Immediate);
                    task1.SetProgramPointer("MainModule", "main");
                    if (controller.State == ControllerState.MotorsOff)
                    {
                        controller.State = ControllerState.MotorsOn;
                    }
                    controller.Rapid.Start();
                }
                DOLim = controller.IOSystem.GetSignal("Dolim");
                doDoLim = (DigitalSignal)DOLim;
                //Thread_SendPath = new Thread(SendPath);
                //Thread_SendPath.IsBackground = true;
                //Thread_SendPath.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show(ex.ToString());
            }
        }

        //private void SendPath()
        //{
        //    List<string> RobotPosition = new List<string>();
        //    List<string> RobotQ = new List<string>();
        //    while (true)
        //    {
        //        Thread.Sleep(500);
        //        if (doDoLim.IsSet && ShareArea.Q_Queue.Count() > 0)
        //        {
        //            ShareArea.Q_Queue.TryDequeue(out RobotQ);
        //            ShareArea.Pos_Queue.TryDequeue(out RobotPosition);
        //            try
        //            {
        //                using (Mastership = Mastership.Request(controller.Rapid))
        //                {
        //                    RapidData rd = controller.Rapid.GetRapidData(task1.Name, "MovePath", "count");
        //                    if (rd.Value is Num)
        //                    {
        //                        Num num = (Num)rd.Value;
        //                        num.Value = RobotPosition.Count();
        //                        rd.Value = num;
        //                    }

        //                    rd = controller.Rapid.GetRapidData(task1.Name, "MovePath", "path");
        //                    for (int i = 0; i < RobotPosition.Count(); i++)
        //                    {
        //                        RobTarget rt = (RobTarget)rd.ReadItem(i);
        //                        rt.Trans.FillFromString2("[" + RobotPosition[i] + "]");
        //                        rt.Rot.FillFromString2("[" + RobotQ[i] + "]");
        //                        rt.Extax.FillFromString2("[9E+09,9E+09,9E+09,9E+09,9E+09,255]");
        //                        rd.WriteItem(rt, i);
        //                    }
        //                }
        //                Console.WriteLine("寫入完成");
        //                Console.WriteLine(DateTime.Now.ToString("HH:mm:ss"));
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex);
        //                MessageBox.Show(ex.Message);
        //            }
        //            finally
        //            {
        //                using (StreamWriter strw = new StreamWriter(Application.StartupPath + @"\MovePath.txt", false))
        //                {
        //                    for (int i = 0; i < RobotPosition.Count(); i++)
        //                    {
        //                        strw.Write(RobotPosition[i]);
        //                        strw.WriteLine(RobotQ[i]);
        //                    }
        //                }
        //                string[] Title = { "MODULE " + modName, "PROC " + "broutine()", "ConfJ \\off;", "!ConfL \\off;" };
        //                System.IO.File.WriteAllLines(Application.StartupPath + "\\" + modName + ".mod", Title);
        //                using (StreamWriter strW = new StreamWriter(Application.StartupPath + "\\" + modName + ".mod", true))
        //                {
        //                    for (int i = 0; i < RobotPosition.Count(); i++)
        //                    {
        //                        string t;
        //                        t = "MoveL" + " " + "[[" + RobotPosition[i] + "]," + "[" + RobotQ[i] + "],[0,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,255]], v400, z50, tool1,\\WObj:=wobjtracktest1;";
        //                        strW.WriteLine(t);
        //                    }
        //                    string End = "EndPROC \nEndModule";
        //                    strW.WriteLine(End);
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
