using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ShoesV6
{
    public partial class VerifyPath : Form
    {
        public VerifyPath()
        {
            InitializeComponent();
            LoadData();
            chart1.ChartAreas[0].AxisX.Maximum = MaxX;
            chart1.ChartAreas[0].AxisX.Minimum = MinX;
            chart1.ChartAreas[0].AxisY.Maximum = MaxY;
            chart1.ChartAreas[0].AxisY.Minimum = MinY;
            chart1.ChartAreas[0].AxisX.Interval = incX;
            chart1.ChartAreas[0].AxisY.Interval = incY;
            for (int i = 0; i < Allinf.Count(); i++)
            {
                this.chart1.Series[0].Points.AddXY(Allinf[i][0], Allinf[i][1]);
            }
            numericUpDown_index.Maximum = Allinf.Count() - 1;
            numericUpDown_index.Value = 0;
        }
        List<double[]> Allinf = new List<double[]>();
        List<double[]> AllinfLabel;
        int MaxX, MinX, incX, MaxY, MinY, incY, pointindex, pointindexLast, pointindexLastMove = 0;
        bool change = false, move = false;

        private void button_RobotMove_Click(object sender, EventArgs e)
        {
            move = true;
            string path = "<Robot><TeachMode>";
            for (int i = pointindexLastMove; i >= pointindex; i--)
            {
                path = path + "<frame X=\"" + AllinfLabel[i][0] + "\" Y=\"" + AllinfLabel[i][1] + "\" Z=\"" + AllinfLabel[i][2] + "\" A=\"" + AllinfLabel[i][3] +
            "\" B=\"" + AllinfLabel[i][4] + "\" C=\"" + AllinfLabel[i][5] + "\"></frame>";
            }
            path = path + "<xyzabcFrameCount>" + (pointindexLastMove - pointindex + 1) + "</xyzabcFrameCount>" + "<check>" + 1 + "</check>" + "<Tool>" + "3" + "</Tool>" + "<Base>" + "1" + "</Base>" + "</TeachMode>" + "</Robot>";
            ShareArea.KUKA_EKI.Send(path);


        }

        iniHelper ini = new iniHelper();

        private void chart1_MouseDown(object sender, MouseEventArgs e)
        {
            HitTestResult result = new HitTestResult();
            result = chart1.HitTest(e.X, e.Y);
            if (result.Series != null && result.Object != null)
            {
                chart1.Series[0].Points[result.PointIndex].MarkerColor = Color.Red;
                label_index.Text = result.PointIndex.ToString();
                textBox_X.Text = Allinf[result.PointIndex][0].ToString("0.000");
                textBox_Y.Text = Allinf[result.PointIndex][1].ToString("0.000");
                textBox_Z.Text = Allinf[result.PointIndex][2].ToString("0.000");
                textBox_A.Text = Allinf[result.PointIndex][3].ToString("0.000");
                textBox_B.Text = Allinf[result.PointIndex][4].ToString("0.000");
                textBox_C.Text = Allinf[result.PointIndex][5].ToString("0.000");
                numericUpDown_index.Value = result.PointIndex;
                try
                {
                    numericUpDown_X.Value = Convert.ToDecimal(ini.IniReadValue("Point" + result.PointIndex.ToString(), "VerifyX", "VerifyPath.ini"));
                    numericUpDown_Y.Value = Convert.ToDecimal(ini.IniReadValue("Point" + result.PointIndex.ToString(), "VerifyY", "VerifyPath.ini"));
                    numericUpDown_Z.Value = Convert.ToDecimal(ini.IniReadValue("Point" + result.PointIndex.ToString(), "VerifyZ", "VerifyPath.ini"));
                    numericUpDown_B.Value = Convert.ToDecimal(ini.IniReadValue("Point" + result.PointIndex.ToString(), "VerifyB", "VerifyPath.ini"));
                }
                catch
                {
                    numericUpDown_X.Value = 0;
                    numericUpDown_Y.Value = 0;
                    numericUpDown_Z.Value = 0;
                    numericUpDown_A.Value = 0;
                    numericUpDown_B.Value = 0;
                    numericUpDown_C.Value = 0;
                }
            }
        }

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                HitTestResult result = new HitTestResult();
                result = chart1.HitTest(e.X, e.Y);
                if (result.Series != null && result.Object != null)
                {
                    // 獲取當前焦點x軸的值
                    string xValue = ObjectUtil.GetPropertyValue(result.Object, "AxisLabel").ToString();
                    // 獲取當前焦點所屬區域名稱
                    string areaName = ObjectUtil.GetPropertyValue(result.Object, "LegendText").ToString();
                    // 獲取當前焦點y軸的值
                    double yValue = result.Series.Points[result.PointIndex].YValues[0];

                    // 滑鼠經過時label顯示
                    label1.Visible = true;
                    label1.Text = "標籤:" + result.PointIndex + " X:" + AllinfLabel[result.PointIndex][0].ToString("0.000") + " Y:" + AllinfLabel[result.PointIndex][1].ToString("0.000") + " Z:" + AllinfLabel[result.PointIndex][2].ToString("0.000") + "\n A:" + AllinfLabel[result.PointIndex][3].ToString("0.000") + " B:" + AllinfLabel[result.PointIndex][4].ToString("0.000") + " C:" + AllinfLabel[result.PointIndex][5].ToString("0.000");
                    label1.Location = new Point(e.X, e.Y - 50);
                }
                else
                {
                    // 滑鼠離開時label隱藏
                    label1.Visible = false;
                }
            }
            catch (Exception se)
            {
                // 滑鼠離開時label隱藏
                label1.Visible = false;
            }
        }
        private void LoadData()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Application.StartupPath + $@"\MovePath\{DateTime.Now.ToString("yyyy-MM-dd")}");
            var latestFile = directoryInfo.GetFiles("*.txt").OrderByDescending(n => n.CreationTime).Take(1).ToArray();
            string Path = latestFile[0].FullName;
            //string Path = Application.StartupPath + @"\MovePath.txt";
            string line;

            StreamReader sr = new StreamReader(Path);
            string[] Allsplit;
            double[] xyzabc;
            while ((line = sr.ReadLine()) != null)
            {
                Allsplit = line.Split(' ');
                xyzabc = new double[6] { Convert.ToDouble(Allsplit[0]), Convert.ToDouble(Allsplit[1]), Convert.ToDouble(Allsplit[2]), Convert.ToDouble(Allsplit[3]), Convert.ToDouble(Allsplit[4]), Convert.ToDouble(Allsplit[5]) };
                Allinf.Add(xyzabc);
            }
            MaxX = (int)Allinf.Max(p => p[0]) + 10;
            MinX = (int)Allinf.Min(p => p[0]) - 10;
            incX = (MaxX - MinX) / 5;
            MaxY = (int)Allinf.Max(p => p[1]) + 10;
            MinY = (int)Allinf.Min(p => p[1]) - 10;
            incY = (MinY - MaxY) / 10;
            AllinfLabel = Clone<double[]>(Allinf);
        }
        private class ObjectUtil
        {
            public static object GetPropertyValue(object info, string field)
            {
                if (info == null) return null;
                Type t = info.GetType();
                IEnumerable<System.Reflection.PropertyInfo> property = from pi in t.GetProperties() where pi.Name.ToLower() == field.ToLower() select pi;
                return property.First().GetValue(info, null);
            }
        }

        private static List<T> Clone<T>(object List)
        {
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, List);
                objectStream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(objectStream) as List<T>;
            }
        }

        private void numericUpDown_index_ValueChanged(object sender, EventArgs e)
        {
            if (change == true)
            {
                chart1.Series[0].Points[pointindexLast].MarkerColor = Color.Gray;
            }
            move = false;
            pointindex = (int)numericUpDown_index.Value;
            chart1.Series[0].Points[pointindex].MarkerColor = Color.Red;
            label_index.Text = pointindex.ToString();
            textBox_X.Text = Allinf[pointindex][0].ToString("0.000");
            textBox_Y.Text = Allinf[pointindex][1].ToString("0.000");
            textBox_Z.Text = Allinf[pointindex][2].ToString("0.000");
            textBox_A.Text = Allinf[pointindex][3].ToString("0.000");
            textBox_B.Text = Allinf[pointindex][4].ToString("0.000");
            textBox_C.Text = Allinf[pointindex][5].ToString("0.000");
            try
            {
                numericUpDown_X.Value = Convert.ToDecimal(ini.IniReadValue("Point" + pointindex.ToString(), "VerifyX", "VerifyPath.ini"));
                numericUpDown_Y.Value = Convert.ToDecimal(ini.IniReadValue("Point" + pointindex.ToString(), "VerifyY", "VerifyPath.ini"));
                numericUpDown_Z.Value = Convert.ToDecimal(ini.IniReadValue("Point" + pointindex.ToString(), "VerifyZ", "VerifyPath.ini"));
                numericUpDown_B.Value = Convert.ToDecimal(ini.IniReadValue("Point" + pointindex.ToString(), "VerifyB", "VerifyPath.ini"));
            }
            catch
            {
                numericUpDown_X.Value = 0;
                numericUpDown_Y.Value = 0;
                numericUpDown_Z.Value = 0;
                numericUpDown_A.Value = 0;
                numericUpDown_B.Value = 0;
                numericUpDown_C.Value = 0;
            }
            pointindexLast = pointindex;
            change = true;
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            double value;
            switch (numericUpDown.Name)
            {
                case "numericUpDown_X":
                    value = (double)numericUpDown_X.Value;
                    AllinfLabel[pointindex][0] = Allinf[pointindex][0] + value;
                    if (checkBox_Auto.Checked && move)
                    {
                        Thread.Sleep(100);
                        string path = "<Robot><TeachMode>";
                        path = path + "<frame X=\"" + AllinfLabel[pointindex][0] + "\" Y=\"" + AllinfLabel[pointindex][1] + "\" Z=\"" + AllinfLabel[pointindex][2] + "\" A=\"" + AllinfLabel[pointindex][3] + "\" B=\"" + AllinfLabel[pointindex][4] + "\" C=\"" + AllinfLabel[pointindex][5] + "\"></frame>";
                        path = path + "<check>" + 1 + "</check>" + "<Tool>" + "3" + "</Tool>" + "<Base>" + "1" + "</Base>" + "</TeachMode>" + "</Robot>";
                    }
                    break;
                case "numericUpDown_Y":
                    value = (double)numericUpDown_Y.Value;
                    AllinfLabel[pointindex][1] = Allinf[pointindex][1] + value;
                    if (checkBox_Auto.Checked && move)
                    {
                        Thread.Sleep(100);
                        string path = "<Robot><TeachMode>";
                        path = path + "<frame X=\"" + AllinfLabel[pointindex][0] + "\" Y=\"" + AllinfLabel[pointindex][1] + "\" Z=\"" + AllinfLabel[pointindex][2] + "\" A=\"" + AllinfLabel[pointindex][3] + "\" B=\"" + AllinfLabel[pointindex][4] + "\" C=\"" + AllinfLabel[pointindex][5] + "\"></frame>";
                        path = path + "<check>" + 1 + "</check>" + "<Tool>" + "3" + "</Tool>" + "<Base>" + "1" + "</Base>" + "</TeachMode>" + "</Robot>";
                    }
                    break;
                case "numericUpDown_Z":
                    value = (double)numericUpDown_Z.Value;
                    AllinfLabel[pointindex][2] = Allinf[pointindex][2] + value;
                    if (checkBox_Auto.Checked && move)
                    {
                        Thread.Sleep(100);
                        string path = "<Robot><TeachMode>";
                        path = path + "<frame X=\"" + AllinfLabel[pointindex][0] + "\" Y=\"" + AllinfLabel[pointindex][1] + "\" Z=\"" + AllinfLabel[pointindex][2] + "\" A=\"" + AllinfLabel[pointindex][3] + "\" B=\"" + AllinfLabel[pointindex][4] + "\" C=\"" + AllinfLabel[pointindex][5] + "\"></frame>";
                        path = path + "<check>" + 1 + "</check>" + "<Tool>" + "3" + "</Tool>" + "<Base>" + "1" + "</Base>" + "</TeachMode>" + "</Robot>";
                    }
                    break;
                case "numericUpDown_A":
                    value = (double)numericUpDown_A.Value;
                    AllinfLabel[pointindex][3] = Allinf[pointindex][3] + value;
                    if (checkBox_Auto.Checked && move)
                    {
                        Thread.Sleep(100);
                        string path = "<Robot><TeachMode>";
                        path = path + "<frame X=\"" + AllinfLabel[pointindex][0] + "\" Y=\"" + AllinfLabel[pointindex][1] + "\" Z=\"" + AllinfLabel[pointindex][2] + "\" A=\"" + AllinfLabel[pointindex][3] + "\" B=\"" + AllinfLabel[pointindex][4] + "\" C=\"" + AllinfLabel[pointindex][5] + "\"></frame>";
                        path = path + "<check>" + 1 + "</check>" + "<Tool>" + "3" + "</Tool>" + "<Base>" + "1" + "</Base>" + "</TeachMode>" + "</Robot>";
                    }
                    break;
                case "numericUpDown_B":
                    value = (double)numericUpDown_B.Value;
                    AllinfLabel[pointindex][4] = Allinf[pointindex][4] + value;
                    if (checkBox_Auto.Checked && move)
                    {
                        Thread.Sleep(100);
                        string path = "<Robot><TeachMode>";
                        path = path + "<frame X=\"" + AllinfLabel[pointindex][0] + "\" Y=\"" + AllinfLabel[pointindex][1] + "\" Z=\"" + AllinfLabel[pointindex][2] + "\" A=\"" + AllinfLabel[pointindex][3] + "\" B=\"" + AllinfLabel[pointindex][4] + "\" C=\"" + AllinfLabel[pointindex][5] + "\"></frame>";
                        path = path + "<check>" + 1 + "</check>" + "<Tool>" + "3" + "</Tool>" + "<Base>" + "1" + "</Base>" + "</TeachMode>" + "</Robot>";
                    }
                    break;
                case "numericUpDown_C":
                    value = (double)numericUpDown_C.Value;
                    AllinfLabel[pointindex][5] = Allinf[pointindex][5] + value;
                    if (checkBox_Auto.Checked && move)
                    {
                        Thread.Sleep(100);
                        string path = "<Robot><TeachMode>";
                        path = path + "<frame X=\"" + AllinfLabel[pointindex][0] + "\" Y=\"" + AllinfLabel[pointindex][1] + "\" Z=\"" + AllinfLabel[pointindex][2] + "\" A=\"" + AllinfLabel[pointindex][3] + "\" B=\"" + AllinfLabel[pointindex][4] + "\" C=\"" + AllinfLabel[pointindex][5] + "\"></frame>";
                        path = path + "<check>" + 1 + "</check>" + "<Tool>" + "3" + "</Tool>" + "<Base>" + "1" + "</Base>" + "</TeachMode>" + "</Robot>";
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
