using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShoesV6
{
    public partial class IOConnect : Form
    {
        public IOConnect()
        {
            InitializeComponent();
            this.MaximizeBox = false;
        }

        private void IOConnect_Load(object sender, EventArgs e)
        {
            string[] portsName = SerialPort.GetPortNames();
            comboBox1_com.DataSource = portsName;
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            if (!ShareArea.SerialPort.IsOpen)
            {
                try
                {
                    ShareArea.SerialPort.PortName = (string)comboBox1_com.SelectedItem;
                    ShareArea.SerialPort.BaudRate = 9600;
                    ShareArea.SerialPort.DataBits = 8;
                    ShareArea.SerialPort.Parity = Parity.None;
                    ShareArea.SerialPort.StopBits = StopBits.One;
                    ShareArea.SerialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
                    ShareArea.SerialPort.Open();
                    ShareArea.IOstate = true;
                    if (ShareArea.SerialPort.IsOpen)
                        this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(20);
            if (e.EventType != SerialData.Chars)
            {
                return;
            }
            if (ShareArea.SerialPort.BytesToRead <= 0)
            {
                return;
            }
            byte[] ReadBuf = new byte[ShareArea.SerialPort.BytesToRead];
            ShareArea.SerialPort.Read(ReadBuf, 0, ReadBuf.Length);
            string str = Convert.ToString(ReadBuf[3], 2);
            str = str.PadLeft(8, '0');
            int[] input = new int[str.Count()];
            for (int i = 0; i < input.Count(); i++)
            {
                input[i] = Convert.ToInt16(str.Substring(i, 1));
            }
            if (input[7] == 1)
                ShareArea.input[0] = true;
            else if (input[6] == 1)
                ShareArea.input[1] = true;
            ShareArea.inputState[0] = input[7];
            ShareArea.inputState[1] = input[6];
            Thread.Sleep(100);
            ShareArea.input[0] = false;
            ShareArea.input[1] = false;

        }
    }
}
