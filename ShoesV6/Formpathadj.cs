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
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;

namespace ShoesV6
{
    public partial class Formpathadj : Form
    {
        public Formpathadj()
        {
            bool _ini;
            InitializeComponent();
            LoadData(out _ini);
            if (!_ini)
            {
                this.Enabled = false;
                return;
            }
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
            numericUpDown_index.Value = 0;
            numericUpDown_X.Enabled = false;
            numericUpDown_Y.Enabled = false;
            numericUpDown_Z.Enabled = false;
            numericUpDown_A.Enabled = false;
            numericUpDown_B.Enabled = false;
            numericUpDown_C.Enabled = false;
        }
        List<double[]> Allinf = new List<double[]>();
        List<double[]> AllinfLabel;
        ShareArea.shiftdata shiftdata = new ShareArea.shiftdata();
        int MaxX, MinX, incX, MaxY, MinY, incY, pointindex, pointindexLast, pointindexLastMove = 0;
        bool change = false, move = false, _ekistate = false, save = false;
        string ModelSize = ShareArea.name;
        private void numericUpDown_index_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox_Auto.Checked)
            {
                numericUpDown_X.Enabled = false;
                numericUpDown_Y.Enabled = false;
                numericUpDown_Z.Enabled = false;
                numericUpDown_A.Enabled = false;
                numericUpDown_B.Enabled = false;
                numericUpDown_C.Enabled = false;
            }
            if (change == true)
            {
                chart1.Series[0].Points[pointindexLast].MarkerColor = Color.Gray;
            }
            if (!save)
            {
                AllinfLabel[pointindex][0] = Allinf[pointindex][0];
                AllinfLabel[pointindex][1] = Allinf[pointindex][1];
                AllinfLabel[pointindex][2] = Allinf[pointindex][2];
                AllinfLabel[pointindex][4] = Allinf[pointindex][4];
            }
            save = false;
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

            var data = shiftdata.shift.Select((c) => new { c }).Where(n => n.c[2] - 1 == pointindex).ToArray();
            if (data.Count() == 0)
            {
                numericUpDown_X.Value = 0;
                numericUpDown_Y.Value = 0;
                numericUpDown_Z.Value = 0;
                numericUpDown_A.Value = 0;
                numericUpDown_B.Value = 0;
                numericUpDown_C.Value = 0;
            }
            else
            {
                numericUpDown_X.Value = Convert.ToDecimal(data[0].c[5]);
                numericUpDown_Y.Value = Convert.ToDecimal(data[0].c[6]);
                numericUpDown_Z.Value = Convert.ToDecimal(data[0].c[7]);
                numericUpDown_B.Value = Convert.ToDecimal(data[0].c[8]);
            }

            pointindexLast = pointindex;
            change = true;
            GC.Collect();
        }

        private void Formpathadj_FormClosing(object sender, FormClosingEventArgs e)
        {
            var data = shiftdata.shift.Select((c, index) => new { index, c }).OrderBy(n => n.c[2]).ToArray();
            using (System.IO.StreamWriter sr = new System.IO.StreamWriter(Application.StartupPath + @"\shiftdata\" + ShareArea.name + ".txt", false))
            {
                for (int i = 0; i < data.Count(); i++)
                {
                    string writedata = data[i].c[0].ToString("0") + "," + data[i].c[1].ToString("0") + "," + data[i].c[2].ToString("0") + "," + data[i].c[3].ToString("0.000") + "," +
                        data[i].c[4].ToString("0.000") + "," + data[i].c[5].ToString("0.00") + "," + data[i].c[6].ToString("0.00") + "," + data[i].c[7].ToString("0.00") + "," +
                        data[i].c[8].ToString("0.00") + ",";
                    sr.WriteLine(writedata);
                }
            }
            Dispose();
        }

        private void checkBox_Auto_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Auto.Checked && _ekistate)
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    numericUpDown_X.Enabled = true;
                    numericUpDown_Y.Enabled = true;
                    numericUpDown_Z.Enabled = true;
                    numericUpDown_A.Enabled = true;
                    numericUpDown_B.Enabled = true;
                    numericUpDown_C.Enabled = true;
                });
            }
            //else
            if (!checkBox_Auto.Checked)
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    numericUpDown_X.Enabled = false;
                    numericUpDown_Y.Enabled = false;
                    numericUpDown_Z.Enabled = false;
                    numericUpDown_A.Enabled = false;
                    numericUpDown_B.Enabled = false;
                    numericUpDown_C.Enabled = false;
                });
            }
            _ekistate = false;
        }

        private void button_RobotMove_Click(object sender, EventArgs e)
        {
            move = true;
            string path = "<Robot><TeachMode>";
            if (pointindexLastMove > pointindex)
            {
                for (int i = pointindexLastMove; i >= pointindex; i--)
                {
                    path = path + "<frame X=\"" + AllinfLabel[i][0] + "\" Y=\"" + AllinfLabel[i][1] + "\" Z=\"" + AllinfLabel[i][2] + "\" A=\"" + AllinfLabel[i][3] +
                "\" B=\"" + AllinfLabel[i][4] + "\" C=\"" + AllinfLabel[i][5] + "\"></frame>";
                }
                path = path + "<xyzabcFrameCount>" + (pointindexLastMove - pointindex + 1) + "</xyzabcFrameCount>" + "<check>" + 1 + "</check>" + "<Tool>" + "3" + "</Tool>" + "<Base>" + "1" + "</Base>" + "</TeachMode>" + "</Robot>";
                ShareArea.KUKA_EKI.Send(path);

            }
            else if (pointindexLastMove < pointindex)
            {
                for (int i = pointindexLastMove; i <= pointindex; i++)
                {
                    path = path + "<frame X=\"" + AllinfLabel[i][0] + "\" Y=\"" + AllinfLabel[i][1] + "\" Z=\"" + AllinfLabel[i][2] + "\" A=\"" + AllinfLabel[i][3] +
                "\" B=\"" + AllinfLabel[i][4] + "\" C=\"" + AllinfLabel[i][5] + "\"></frame>";
                }
                path = path + "<xyzabcFrameCount>" + (pointindex - pointindexLastMove + 1) + "</xyzabcFrameCount>" + "<check>" + 1 + "</check>" + "<Tool>" + "3" + "</Tool>" + "<Base>" + "1" + "</Base>" + "</TeachMode>" + "</Robot>";
                ShareArea.KUKA_EKI.Send(path);
            }
            pointindexLastMove = pointindex;
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
                    if (move)
                    {
                        Thread.Sleep(100);
                        string path = "<Robot><TeachMode>";
                        path = path + "<frame X=\"" + AllinfLabel[pointindex][0] + "\" Y=\"" + AllinfLabel[pointindex][1] + "\" Z=\"" + AllinfLabel[pointindex][2] + "\" A=\"" + AllinfLabel[pointindex][3] + "\" B=\"" + AllinfLabel[pointindex][4] + "\" C=\"" + AllinfLabel[pointindex][5] + "\"></frame>";
                        path = path + "<xyzabcFrameCount>" + 1 + "</xyzabcFrameCount>" + "<check>" + 1 + "</check>" + "<Tool>" + "3" + "</Tool>" + "<Base>" + "1" + "</Base>" + "</TeachMode>" + "</Robot>";
                        ShareArea.KUKA_EKI.Send(path);
                    }
                    break;
                case "numericUpDown_Y":
                    value = (double)numericUpDown_Y.Value;
                    AllinfLabel[pointindex][1] = Allinf[pointindex][1] + value;
                    if (move)
                    {
                        Thread.Sleep(100);
                        string path = "<Robot><TeachMode>";
                        path = path + "<frame X=\"" + AllinfLabel[pointindex][0] + "\" Y=\"" + AllinfLabel[pointindex][1] + "\" Z=\"" + AllinfLabel[pointindex][2] + "\" A=\"" + AllinfLabel[pointindex][3] + "\" B=\"" + AllinfLabel[pointindex][4] + "\" C=\"" + AllinfLabel[pointindex][5] + "\"></frame>";
                        path = path + "<xyzabcFrameCount>" + 1 + "</xyzabcFrameCount>" + "<check>" + 1 + "</check>" + "<Tool>" + "3" + "</Tool>" + "<Base>" + "1" + "</Base>" + "</TeachMode>" + "</Robot>";
                        ShareArea.KUKA_EKI.Send(path);
                    }
                    break;
                case "numericUpDown_Z":
                    value = (double)numericUpDown_Z.Value;
                    AllinfLabel[pointindex][2] = Allinf[pointindex][2] + value;
                    if (move)
                    {
                        Thread.Sleep(100);
                        string path = "<Robot><TeachMode>";
                        path = path + "<frame X=\"" + AllinfLabel[pointindex][0] + "\" Y=\"" + AllinfLabel[pointindex][1] + "\" Z=\"" + AllinfLabel[pointindex][2] + "\" A=\"" + AllinfLabel[pointindex][3] + "\" B=\"" + AllinfLabel[pointindex][4] + "\" C=\"" + AllinfLabel[pointindex][5] + "\"></frame>";
                        path = path + "<xyzabcFrameCount>" + 1 + "</xyzabcFrameCount>" + "<check>" + 1 + "</check>" + "<Tool>" + "3" + "</Tool>" + "<Base>" + "1" + "</Base>" + "</TeachMode>" + "</Robot>";
                        ShareArea.KUKA_EKI.Send(path);
                    }
                    break;
                case "numericUpDown_A":
                    value = (double)numericUpDown_A.Value;
                    AllinfLabel[pointindex][3] = Allinf[pointindex][3] + value;
                    if (move)
                    {
                        Thread.Sleep(100);
                        string path = "<Robot><TeachMode>";
                        path = path + "<frame X=\"" + AllinfLabel[pointindex][0] + "\" Y=\"" + AllinfLabel[pointindex][1] + "\" Z=\"" + AllinfLabel[pointindex][2] + "\" A=\"" + AllinfLabel[pointindex][3] + "\" B=\"" + AllinfLabel[pointindex][4] + "\" C=\"" + AllinfLabel[pointindex][5] + "\"></frame>";
                        path = path + "<xyzabcFrameCount>" + 1 + "</xyzabcFrameCount>" + "<check>" + 1 + "</check>" + "<Tool>" + "3" + "</Tool>" + "<Base>" + "1" + "</Base>" + "</TeachMode>" + "</Robot>";
                        ShareArea.KUKA_EKI.Send(path);
                    }
                    break;
                case "numericUpDown_B":
                    value = (double)numericUpDown_B.Value;
                    AllinfLabel[pointindex][4] = Allinf[pointindex][4] + value;
                    if (move)
                    {
                        Thread.Sleep(100);
                        string path = "<Robot><TeachMode>";
                        path = path + "<frame X=\"" + AllinfLabel[pointindex][0] + "\" Y=\"" + AllinfLabel[pointindex][1] + "\" Z=\"" + AllinfLabel[pointindex][2] + "\" A=\"" + AllinfLabel[pointindex][3] + "\" B=\"" + AllinfLabel[pointindex][4] + "\" C=\"" + AllinfLabel[pointindex][5] + "\"></frame>";
                        path = path + "<xyzabcFrameCount>" + 1 + "</xyzabcFrameCount>" + "<check>" + 1 + "</check>" + "<Tool>" + "3" + "</Tool>" + "<Base>" + "1" + "</Base>" + "</TeachMode>" + "</Robot>";
                        ShareArea.KUKA_EKI.Send(path);
                    }
                    break;
                case "numericUpDown_C":
                    value = (double)numericUpDown_C.Value;
                    AllinfLabel[pointindex][5] = Allinf[pointindex][5] + value;
                    if (move)
                    {
                        Thread.Sleep(100);
                        string path = "<Robot><TeachMode>";
                        path = path + "<frame X=\"" + AllinfLabel[pointindex][0] + "\" Y=\"" + AllinfLabel[pointindex][1] + "\" Z=\"" + AllinfLabel[pointindex][2] + "\" A=\"" + AllinfLabel[pointindex][3] + "\" B=\"" + AllinfLabel[pointindex][4] + "\" C=\"" + AllinfLabel[pointindex][5] + "\"></frame>";
                        path = path + "<xyzabcFrameCount>" + 1 + "</xyzabcFrameCount>" + "<check>" + 1 + "</check>" + "<Tool>" + "3" + "</Tool>" + "<Base>" + "1" + "</Base>" + "</TeachMode>" + "</Robot>";
                        ShareArea.KUKA_EKI.Send(path);
                    }
                    break;
                default:
                    break;
            }
            numericUpDown_X.Enabled = false;
            numericUpDown_Y.Enabled = false;
            numericUpDown_Z.Enabled = false;
            numericUpDown_A.Enabled = false;
            numericUpDown_B.Enabled = false;
            numericUpDown_C.Enabled = false;
            GC.Collect();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            save = true;
            double[] datas = new double[9];
            datas[0] = Allinf.Count();
            datas[1] = ShareArea.half;
            datas[2] = Convert.ToDouble(numericUpDown_index.Value) + 1;
            datas[3] = Convert.ToDouble(textBox_X.Text);
            datas[4] = Convert.ToDouble(textBox_Y.Text);
            datas[5] = Convert.ToDouble(numericUpDown_X.Value);
            datas[6] = Convert.ToDouble(numericUpDown_Y.Value);
            datas[7] = Convert.ToDouble(numericUpDown_Z.Value);
            datas[8] = Convert.ToDouble(numericUpDown_B.Value);

            var data = shiftdata.shift.Select((c, index) => new { index, c }).Where(n => n.c[2] - 1 == Convert.ToInt16(numericUpDown_index.Value)).ToArray();
            if (data.Count() != 0)
            {
                shiftdata.shift.RemoveAt(data[0].index);
            }
            shiftdata.shift.Add(datas);
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
            GC.Collect();
        }

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

                var data = shiftdata.shift.Select((c) => new { c }).Where(n => n.c[2] - 1 == result.PointIndex).ToArray();
                if (data.Count() == 0)
                {
                    numericUpDown_X.Value = 0;
                    numericUpDown_Y.Value = 0;
                    numericUpDown_Z.Value = 0;
                    numericUpDown_A.Value = 0;
                    numericUpDown_B.Value = 0;
                    numericUpDown_C.Value = 0;
                }
                else
                {
                    numericUpDown_X.Value = Convert.ToDecimal(data[0].c[5]);
                    numericUpDown_Y.Value = Convert.ToDecimal(data[0].c[6]);
                    numericUpDown_Z.Value = Convert.ToDecimal(data[0].c[7]);
                    numericUpDown_B.Value = Convert.ToDecimal(data[0].c[8]);
                }

            }
            GC.Collect();
        }

        private void LoadData(out bool _ini)
        {
            _ini = true;
            ShareArea.KUKA_EKI.numericUpDown_enable += KUKA_EKI_numericUpDown_enable;
            string line;
            string[] Allsplit;
            double[] xyzabc;
            try
            {
                using (StreamReader sr = new StreamReader(Application.StartupPath + @"\path\" + ShareArea.name + "path.txt"))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        Allsplit = line.Split(' ');
                        xyzabc = new double[6] { Convert.ToDouble(Allsplit[0]), Convert.ToDouble(Allsplit[1]), Convert.ToDouble(Allsplit[2]), Convert.ToDouble(Allsplit[3]), Convert.ToDouble(Allsplit[4]), Convert.ToDouble(Allsplit[5]) };
                        Allinf.Add(xyzabc);
                    }
                }
            }
            catch
            {
                _ini = false;
                MessageBox.Show("此規格未建立模型");
                return;
            }
            MaxX = (int)Allinf.Max(p => p[0]) + 10;
            MinX = (int)Allinf.Min(p => p[0]) - 10;
            incX = (MaxX - MinX) / 5;
            MaxY = (int)Allinf.Max(p => p[1]) + 10;
            MinY = (int)Allinf.Min(p => p[1]) - 10;
            incY = (MinY - MaxY) / 10;
            AllinfLabel = Clone<double[]>(Allinf);
            numericUpDown_index.Maximum = Allinf.Count() - 1;
            try
            {
                string[] filename = ShareArea.name.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
                shiftdata.name = filename[1];
                using (StreamReader sr = new StreamReader(Application.StartupPath + @"\shiftdata\" + ShareArea.name + ".txt"))
                {
                    shiftdata.shift = new List<double[]>();
                    while ((line = sr.ReadLine()) != null)
                    {
                        Allsplit = line.Split(',');
                        double[] datas = new double[9];
                        datas[0] = Convert.ToDouble(Allsplit[0]);
                        datas[1] = Convert.ToDouble(Allsplit[1]);
                        datas[2] = Convert.ToDouble(Allsplit[2]);
                        datas[3] = Convert.ToDouble(Allsplit[3]);
                        datas[4] = Convert.ToDouble(Allsplit[4]);
                        datas[5] = Convert.ToDouble(Allsplit[5]);
                        datas[6] = Convert.ToDouble(Allsplit[6]);
                        datas[7] = Convert.ToDouble(Allsplit[7]);
                        datas[8] = Convert.ToDouble(Allsplit[8]);
                        shiftdata.shift.Add(datas);
                    }
                }
            }
            catch
            {
                _ini = false;
                MessageBox.Show("此規格未建立模型");
                return;
            }
        }

        private void KUKA_EKI_numericUpDown_enable(bool state)
        {
            _ekistate = true;
            if (state == true && IsHandleCreated && checkBox_Auto.Checked)
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    numericUpDown_X.Enabled = true;
                    numericUpDown_Y.Enabled = true;
                    numericUpDown_Z.Enabled = true;
                    numericUpDown_A.Enabled = true;
                    numericUpDown_B.Enabled = true;
                    numericUpDown_C.Enabled = true;
                });
            }
        }
        #region support
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
        #endregion
    }
}
