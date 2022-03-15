using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShoesV6
{
    public class iniHelperBBB
    {
        public string path;
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section,
        string key, string val, string filePath);
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section,
        string key, string def, StringBuilder retVal,
        int size, string filePath);
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern uint GetPrivateProfileSectionNames(IntPtr pszReturnBuffer, uint nSize, string lpFileName);

        public void IniWriteValue(string Section, string Key, string Value, string inipath)
        {
            WritePrivateProfileString(Section, Key, Value, Application.StartupPath + "\\ini\\" + inipath);
        }
        public string IniReadValue(string Section, string Key, string inipath)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, Application.StartupPath + "\\ini\\" + inipath);
            return temp.ToString();
        }

        public string[] SectionNames(string path)
        {
            path = Application.StartupPath + "\\ini\\" + path;
            uint MAX_BUFFER = 32767;
            string[] sections = new string[0];
            IntPtr pReturnedString = Marshal.AllocCoTaskMem((int)MAX_BUFFER);
            uint bytesReturned = GetPrivateProfileSectionNames(pReturnedString, MAX_BUFFER, path);
            if (bytesReturned == 0)
                return null;
            string local = Marshal.PtrToStringAuto(pReturnedString, (int)bytesReturned).ToString();
            sections = local.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
            Marshal.FreeCoTaskMem(pReturnedString);
            return sections;
        }
    }
}
