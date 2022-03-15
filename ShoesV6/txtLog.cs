using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace ShoesV6
{
    public class txtLog
    {
        private static string logPath = Application.StartupPath + @"\Log";
        private static string Today = DateTime.Now.ToString("yyyy-MM-dd");
        public txtLog()
        {
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
            if (!File.Exists(logPath + "\\" + Today + ".log"))
            {
                File.Create(logPath + "\\" + Today + ".log").Close();
            }
        }
        public void WriteLog(int num, string LogMsg)
        {
            string TodayMillisecond = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            switch (num)
            {
                case 1:
                    using (StreamWriter sw = File.AppendText(logPath + "\\" + Today + ".log"))
                    {
                        sw.Write(TodayMillisecond + "---->");
                        sw.WriteLine(LogMsg);
                        sw.WriteLine("");
                    }
                    break;
                case 2:// robot step
                    using (StreamWriter sw = File.AppendText(logPath + "\\" + Today + ".log"))
                    {
                        sw.Write(TodayMillisecond + "---->");
                        sw.WriteLine(LogMsg);
                        sw.WriteLine("");
                    }
                    break;
                default:
                    break;
            }

        }
    }
}
