using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ShoesV6
{
    public class KUKA_EKI //KUKA EKI的Class
    {
        private string _ip;
        private int _port;
        private int _timeout;
        private ManualResetEvent _timeoutEvent = new ManualResetEvent(false);
        private Exception _socketexception;
        private bool _isConnectionSuccessful = false;
        private Thread EKI_ConnectCheckThread;
        private string _replyMessage;
        private bool _connectState = false;
        private StateObject _stateObject;  // Tcp\IP狀態Class
        private IAsyncResult _asyncResult;  //非同步作業狀態
        private XmlDocument _xmlDocument = new XmlDocument();  //用來將xml格式解包
        private double[] _axisPos = new double[6];
        private double[] _xyzabcPos = new double[6];
        private int _checkPathCount = 0;
        private bool _kukaCheckFlag = false;
        private bool _readFlag = false;
        private bool _kukaIdle = false;
        private int MoveTag;
        private int _MoveTag;
        private byte[] _testByte = new byte[1];




        public delegate void ChangeDelegate(bool state);
        public event ChangeDelegate ChangeEvent;
        public delegate void TagChange(bool state);
        public event TagChange numericUpDown_enable;

        private int ReceiveData
        {
            set
            {
                if (value == 23 && numericUpDown_enable != null)
                    numericUpDown_enable(true);
            }
        }

        private bool ConnectStateSender //EKI 連線狀態
        {
            get
            {
                return _connectState;
            }
            set
            {
                if (_connectState != value)
                {
                    ChangeEvent(value);
                    ShareArea.Connectstate[0] = value;
                }
                _connectState = value;
            }
        }

        public string IP  //EKI連線:IP
        {
            get
            {
                return _ip;
            }
            set
            {
                _ip = value;
            }
        }

        public int Port  //EKI連線:Port
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
            }
        }

        public int Timeout  //EKI連線:Timeout ;單位:ms
        {
            get
            {
                return _timeout;
            }
            set
            {
                _timeout = value;
            }
        }

        public string ReplyMessage
        {
            get
            {
                return _replyMessage;
            }
        }

        public bool ConnectState
        {
            get
            {
                return _connectState;
            }
        }

        public double[] AxisPos  //手臂6軸位置
        {
            get
            {
                return _axisPos;
            }
        }

        public double[] XYZABC_Pos  //手臂尤拉角位置
        {
            get
            {
                return _xyzabcPos;
            }
        }

        public int CheckPathCount  //確認路徑點數
        {
            get
            {
                return _checkPathCount;
            }
        }

        public bool KukaCheckFlag  //手臂檢查旗標
        {
            get
            {
                return _kukaCheckFlag;
            }
            set
            {
                _kukaCheckFlag = value;
            }
        }

        public bool KukaReadFlag  //手臂檢查旗標
        {
            get
            {
                return _readFlag;
            }
            set
            {
                _readFlag = value;
            }
        }

        public bool KukaIdle
        {
            get
            {
                return _kukaIdle;
            }
            set
            {
                _kukaIdle = value;
            }
        }

        public bool EKI_Connect()  //EKI連線
        {            
            TcpClient client;
            _replyMessage = "";
            _timeoutEvent.Reset();
            _isConnectionSuccessful = false;
            if (_connectState)                   //myClient有被new出來的話 結束
            {
                _replyMessage = "EKI已連線";
                return true;
            }
            else
            {
                _stateObject = null;
                client = null;
                client = new TcpClient();  //TcpClient Class實例化
                _stateObject = new StateObject(client);  //Tcp\IP狀態Class實例化
            }
            try
            {

                _asyncResult = _stateObject.Client.BeginConnect(_ip, _port, new AsyncCallback(EndConnectCallback), _stateObject);  // Tcp\IP非同步連線
                if (_timeoutEvent.WaitOne(_timeout, false))
                {
                    if (_isConnectionSuccessful)
                    {

                        _stateObject.Client.Client.Poll(0, SelectMode.SelectRead);
                        //bool result = _stateObject.Client.Client.Receive(_testByte, SocketFlags.Peek) == 0;

                        //if (!result)
                        //{
                        ConnectStateSender = true;
                        EKI_ConnectCheckThread = null;
                        EKI_ConnectCheckThread = new Thread(EKI_ConnectCheck);
                        NetworkStream stream = _stateObject.Client.GetStream();  //取得網路資料串流
                        stream.BeginRead(_stateObject.Buffer, 0, _stateObject.BufferSize, new AsyncCallback(EndReadCallback), _stateObject);  //從資料流中讀取資料
                        if (!EKI_ConnectCheckThread.IsAlive)
                        {
                            EKI_ConnectCheckThread.IsBackground = true;
                            EKI_ConnectCheckThread.Start();
                        }

                        return true;
                        //}
                        //else
                        //{
                        //    ConnectStateSender = false;
                        //    throw _socketexception;
                        //}
                    }
                    else
                    {
                        ConnectStateSender = false;
                        ShareArea.txtLog.WriteLog(1, "Robot 連線失敗");
                        //沒有連線成功丟出Exception
                        throw _socketexception;
                    }
                }
                else
                {
                    ConnectStateSender = false;
                    //當等候時間逾時
                    _stateObject.Client.Close();
                    ShareArea.txtLog.WriteLog(1, "Robot 連線逾時");
                    throw new TimeoutException("Time Out Exception !");
                }

            }
            catch (Exception ex)
            {
                _replyMessage = " Exception:" + ex.Message;
                MessageBox.Show("Robot連線逾時");
                ShareArea.txtLog.WriteLog(1, "Robot" + _replyMessage);
                return false;
            }

        }

        private void EndConnectCallback(IAsyncResult ar)  //BeginConnect作業完成事件
        {
            _isConnectionSuccessful = false;  //檢查回傳物件是否存在(存在代表連線成功)

            StateObject state = (StateObject)ar.AsyncState;  //取得非同步作業狀態

            TcpClient client = state.Client;  //取出StateObject中的TcpClient
            try
            {
                if (client.Client != null)
                {
                    client.EndConnect(ar);
                    _isConnectionSuccessful = true;
                    //通知等候結束
                    _timeoutEvent.Set();
                }


            }
            catch (Exception ex)
            {
                //發生連線意外
                _isConnectionSuccessful = false;
                _socketexception = ex;
            }

        }

        private void EndWriteCallback(IAsyncResult ar)   //BeginWrite作業完成事件
        {
            StateObject state = (StateObject)ar.AsyncState;   //取得非同步作業狀態
            TcpClient client = state.Client;   //取出StateObject中的TcpClient

            try
            {
                NetworkStream stream = client.GetStream();  //取得網路資料串流
                stream.EndWrite(ar);  //結束資料流寫入

                if (stream.CanRead)  //判斷資料流是否可以讀取
                {
                    stream.BeginRead(state.Buffer, 0, state.BufferSize, new AsyncCallback(EndReadCallback), state);  //從資料流中讀取資料
                }
            }
            catch (Exception ex)
            {
                client.Close();  // Tcp\Ip連線關閉
                state = null;  //T cp\IP狀態物件變為null
                Console.WriteLine(string.Format("Ready (last error: {0})", ex.Message));
            }
        }

        private void EndReadCallback(IAsyncResult ar)   //BeginWrite作業完成事件
        {
            StateObject state = (StateObject)ar.AsyncState;  //取得非同步作業狀態
            TcpClient client = state.Client;  //取出StateObject中的TcpClient
            NetworkStream stream = client.GetStream();  //取得網路資料串流

            string tmp;  //布林訊號暫存
            int bytesRead = stream.EndRead(ar);  //結束資料流讀取並會得接收資料的byte大小

            if (bytesRead > 0)  //判斷接收資料的byte大小是否大於0
            {
                state.Data.Clear();  //資料清空
                state.Data.Append(Encoding.UTF8.GetString(state.Buffer, 0, bytesRead));  //將要資料的Byte轉為字串放入state.Data
                string readData = state.Data.ToString();  //state.Data取出
                _xmlDocument.LoadXml(readData);  //放入_xmlDocument中

                ////////////////////////////////////////當前手臂的6六軸位置跟尤拉角位置/////////////////////////////
                try
                {
                    ////////////////////////////////////////當前手臂的6六軸位置////////////////////////////////////
                    _axisPos[0] = double.Parse(_xmlDocument.SelectSingleNode("/Position/A1").InnerText);
                    _axisPos[1] = double.Parse(_xmlDocument.SelectSingleNode("/Position/A2").InnerText);
                    _axisPos[2] = double.Parse(_xmlDocument.SelectSingleNode("/Position/A3").InnerText);
                    _axisPos[3] = double.Parse(_xmlDocument.SelectSingleNode("/Position/A4").InnerText);
                    _axisPos[4] = double.Parse(_xmlDocument.SelectSingleNode("/Position/A5").InnerText);
                    _axisPos[5] = double.Parse(_xmlDocument.SelectSingleNode("/Position/A6").InnerText);
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    ///////////////////////////////////////////當前手臂的尤拉角位置///////////////////////////////////
                    _xyzabcPos[0] = double.Parse(_xmlDocument.SelectSingleNode("/Position/X").InnerText);
                    _xyzabcPos[1] = double.Parse(_xmlDocument.SelectSingleNode("/Position/Y").InnerText);
                    _xyzabcPos[2] = double.Parse(_xmlDocument.SelectSingleNode("/Position/Z").InnerText);
                    _xyzabcPos[3] = double.Parse(_xmlDocument.SelectSingleNode("/Position/A").InnerText);
                    _xyzabcPos[4] = double.Parse(_xmlDocument.SelectSingleNode("/Position/B").InnerText);
                    _xyzabcPos[5] = double.Parse(_xmlDocument.SelectSingleNode("/Position/C").InnerText);
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                catch (Exception)
                {
                    _axisPos[0] = 0;
                    _axisPos[1] = 0;
                    _axisPos[2] = 0;
                    _axisPos[3] = 0;
                    _axisPos[4] = 0;
                    _axisPos[5] = 0;
                    _xyzabcPos[0] = 0;
                    _xyzabcPos[1] = 0;
                    _xyzabcPos[2] = 0;
                    _xyzabcPos[3] = 0;
                    _xyzabcPos[4] = 0;
                    _xyzabcPos[5] = 0;
                }
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                ////////////////////////////////////////////////確認路徑點數//////////////////////////////////////////////
                try
                {
                    _checkPathCount = int.Parse(_xmlDocument.SelectSingleNode("/Position/Check_PathCount").InnerText);
                }
                catch (Exception)
                {
                    _checkPathCount = 0;
                }
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////


                ////////////////////////////////////////////////確認手臂動作狀態/////////////////////////////////////////////
                try
                {
                    tmp = _xmlDocument.SelectSingleNode("/Position/Idle").InnerText;
                    if (tmp == "0")
                    {
                        _kukaIdle = false;
                    }
                    else
                    {
                        _kukaIdle = true;
                    }
                    Console.WriteLine("Idle:" + _kukaIdle.ToString());
                }
                catch (Exception)
                {

                }
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///
                try
                {
                    MoveTag = int.Parse(_xmlDocument.SelectSingleNode("/Safe/MoveTag").InnerText);
                    ShareArea.txtLog.WriteLog(1, "Robot step:" + MoveTag);
                    Console.WriteLine("MoveTag:" + MoveTag);
                }
                catch (Exception)
                {

                }
                ReceiveData = MoveTag;
                _readFlag = true;
                stream.BeginRead(state.Buffer, 0, state.BufferSize, new AsyncCallback(EndReadCallback), state);  //從資料流中讀取資料
            }
        }

        public void Send(string data)  //EKI傳送資料
        {
            try
            {
                if (_stateObject.Client.Connected)  //判斷Tcp\Ip是否有連線
                {
                    NetworkStream stream = _stateObject.Client.GetStream();  //取得網路資料串流
                    if (stream.CanWrite)
                    {
                        byte[] send = Encoding.UTF8.GetBytes(data);   //將要發送的字串轉為Byte
                        stream.BeginWrite(send, 0, send.Length, new AsyncCallback(EndWriteCallback), _stateObject);   //資料寫入資料流中
                    }
                }
                else
                {
                    ConnectStateSender = false;  //連線狀態變為false
                    _stateObject.Client.Close();  // Tcp\Ip連線關閉
                    _stateObject = null;  //T cp\IP狀態物件變為null
                    Console.WriteLine(string.Format("Ready (last error: {0})", "Connect Failed!"));

                }
            }
            catch (Exception ex)
            {
                ConnectStateSender = false;  //連線狀態變為false
                _stateObject.Client.Close();  // Tcp\Ip連線關閉
                _stateObject = null;  // Tcp\IP狀態物件變為null
                Console.WriteLine(string.Format("Ready (last error: {0})", ex.ToString()));
            }
        }

        private void EKI_ConnectCheck()
        {
            while (_connectState)
            {
                _stateObject.Client.Client.Poll(0, SelectMode.SelectRead);
                try
                {
                    bool result = _stateObject.Client.Client.Receive(_testByte, SocketFlags.Peek) == 0;
                    ConnectStateSender = !result;
                }
                catch
                {
                    ShareArea.txtLog.WriteLog(1, "Robot 連線被關閉");
                    Console.WriteLine("連線被關閉");
                }
                Thread.Sleep(1000);
            }
        }
    }


    internal class StateObject  // Tcp\IP狀態Class
    {
        public readonly TcpClient Client;
        public readonly byte[] Buffer;  //接收資料的Buffer矩陣
        public readonly StringBuilder Data;  //接收的資料字串
        public readonly int BufferSize = 65536;  //接收資料的Buffer矩陣的大小

        public StateObject(TcpClient client)  //建構式
        {
            this.Client = client;
            this.Data = new StringBuilder();  //StringBuilder實例化
            this.Buffer = new byte[this.BufferSize];  //矩陣實例化
        }
    }
}
