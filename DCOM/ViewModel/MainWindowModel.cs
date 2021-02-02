using DCOM.Helper;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Windows;

namespace DCOM.ViewModel
{
    class MainWindowModel : ViewModelBase
    {
        public MainWindowModel()
        {
            InitCommand();
            PutLog("打开程序");
        }

        #region FieldDefine
        private SerialPort serialPort;
        
        private int numberBytesSendInt = 0;

        private int receiveDisplayType = 0;
        private List<byte> receiveBuffer = new List<byte>();
        private string receiveData = string.Empty;

        private string openOrCloseButtonContent = "打开串口";
        private string logText = string.Empty;
        private int sendDataType = 0;
        private string sendDataLog = string.Empty;
        private string sendDataText = string.Empty;
        private string numberBytesReceived = "0";
        private string numberBytesSend = "0";
        #endregion

        #region Property

        public int ReceiveDisplayType
        {
            get { return receiveDisplayType; }
            set { receiveDisplayType = value; RaisePropertyChanged(); DisplayReceiveData(); }
        }

        public string ReceiveData
        {
            get { return receiveData; }
            set { receiveData = value; RaisePropertyChanged(); }
        }

        public string OpenOrCloseButtonContent
        {
            get { return openOrCloseButtonContent; }
            set { openOrCloseButtonContent = value; RaisePropertyChanged(); }
        }

        public string LogText
        {
            get { return logText; }
            set { logText = value; RaisePropertyChanged(); }
        }

        public string SendDataText
        {
            get { return sendDataText; }
            set { sendDataText = value; RaisePropertyChanged(); }
        }

        
        public string NumberBytesReceived
        {
            get { return numberBytesReceived; }
            set { numberBytesReceived = value; RaisePropertyChanged(); }
        }

        
        public string NumberBytesSend
        {
            get { return numberBytesSend; }
            set { numberBytesSend = value; RaisePropertyChanged(); }
        }

        
        public int SendDataType
        {
            get { return sendDataType; }
            set { sendDataType = value; RaisePropertyChanged(); }
        }

        

        public string SendDataLog
        {
            get { return sendDataLog; }
            set { sendDataLog = value; RaisePropertyChanged(); }
        }


        #endregion


        private void PutLog(string text)
        {
            LogText += System.DateTime.Now.ToString();
            LogText += " : " + text + '\n';
        }

        private void PutSendDataLog(int type, string text)
        {
            SendDataLog += System.DateTime.Now.ToString() + '(' + (type == 1 ? "十六进制" : "文本发送") + ')' + '\n';
            SendDataLog += text + '\n';
        }


        #region Command

        private void InitCommand()
        {
            OpenOrCloseCommand = new RelayCommand(OpenOrCloseCom);
            ClearOutputCommand = new RelayCommand(ClearOutput);
            SendDataCommand = new RelayCommand(SendData);
        }

        public RelayCommand OpenOrCloseCommand { get; set; }
        public RelayCommand ClearOutputCommand { get; set; }
        public RelayCommand SendDataCommand { get; set; }
        #endregion


        #region CommandRealize

        private void ClearOutput()
        {
            receiveBuffer.Clear();
            ReceiveData = string.Empty;
            SendDataLog = string.Empty;
            numberBytesSendInt = 0;
            NumberBytesReceived = receiveBuffer.Count.ToString();
            NumberBytesSend = numberBytesSendInt.ToString();

            PutLog("清空数据");
        }

        private void OpenOrCloseCom()
        {
            if(serialPort != null && serialPort.IsOpen)
            {
                CloseCom();
                if(serialPort == null)
                {
                    PutLog("成功关闭串口");
                    OpenOrCloseButtonContent = "打开串口";
                }
                else
                {
                    PutLog("关闭串口失败");
                }
            }
            else
            {
                if (OpenCOM())
                {
                    PutLog("成功打开串口");
                    OpenOrCloseButtonContent = "关闭串口";
                }
                else
                {
                    PutLog("打开串口失败");
                }
            }
        }

        private void SendData()
        {
             
            if(SendDataType == 0)
            {
                PutSendDataLog(SendDataType, SendDataText);
                serialPort.Write(SendDataText);
                numberBytesSendInt += SendDataText.Length;
            }
            else
            {
                byte[] buffer = ByteConvert.ToBytes(SendDataText);
                serialPort.Write(buffer, 0, buffer.Length);
                PutSendDataLog(SendDataType, ByteConvert.ToHex(buffer));
                numberBytesSendInt += buffer.Length;
            }

            NumberBytesSend = numberBytesSendInt.ToString();


        }

        #endregion


        private void DisplayReceiveData()
        {
            if (receiveDisplayType == 0)
            {
                ReceiveData = ByteConvert.ToHex(receiveBuffer.ToArray());
            }
            else
            {
                ReceiveData = Encoding.GetEncoding("GBK").GetString(receiveBuffer.ToArray());
            }
        }

        #region 串口操作
        private bool OpenCOM()
        {
            if(serialPort != null)
            {
                CloseCom();
            }

            try
            {
                serialPort = new SerialPort("COM3");

                serialPort.BaudRate = 115200;
                serialPort.Parity = Parity.None;
                serialPort.DataBits = 8;
                serialPort.StopBits = StopBits.One;
                
                serialPort.RtsEnable = false;
                
                serialPort.DataReceived += SerialPort_DataReceived;

                serialPort.Open();

                return serialPort.IsOpen;
            } 
            catch (Exception)
            {
                serialPort = null;
            }
            return false;
        }

        private void CloseCom()
        {
            serialPort.Close();
            if (!serialPort.IsOpen) serialPort = null;
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] readBuffer = new byte[serialPort.ReadBufferSize];
            int size = serialPort.Read(readBuffer, 0, readBuffer.Length);
            for (int i = 0; i < size; ++i)
            {
                receiveBuffer.Add(readBuffer[i]);
            }
            NumberBytesReceived = receiveBuffer.Count.ToString();
            DisplayReceiveData();
        }

        #endregion


    }
}
