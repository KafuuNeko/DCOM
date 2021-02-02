using DCOM.Helper;
using DCOM.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows;

namespace DCOM.ViewModel
{
    class MainWindowModel : ViewModelBase
    {
        public MainWindowModel()
        {
            InitCommand();
            InitTimeTask();

            var encodings = Encoding.GetEncodings();
            EncodingName = new List<string>();
            for (int i = 0; i < encodings.Length; ++i)
                EncodingName.Add(encodings[i].Name);

            this.ComConfig = new ComConfig();
            RefreshSerialPortList();

            PutLog("打开程序");
        }

        #region Time task

        private void InitTimeTask()
        {
            timedTask.Elapsed += TimedTask;
            timedTask.Enabled = true;
            timedTask.Start();
            timedTask.AutoReset = true;
        }

        private void TimedTask(Object source, ElapsedEventArgs e)
        {
            if(dataUpdate)
            {
                //Update the number of received bytes displayed
                NumberBytesReceived = receiveBuffer.Count.ToString();
                //Update the displayed data
                DisplayReceiveData();
                dataUpdate = false;
            }
        }

        #endregion

        #region Serial port property
        private string   comName    = string.Empty;
        private int      baudRate   = 115200;
        private Parity   parity     = System.IO.Ports.Parity.None;
        private int      dataBits   = 8;
        private StopBits stopBits   = StopBits.One;
        private bool     rtsEnable  = false;

        public string ComName
        {
            get { return comName; }
            set { comName = value; RaisePropertyChanged(); }
        }


        public string BaudRate 
        { 
            get { return baudRate.ToString(); }
            set 
            { 
                try
                {
                    baudRate = Convert.ToInt32(value);
                } catch (Exception) { baudRate = 0; }
                
                RaisePropertyChanged(); 
            }
        }

        public string StopBit
        {
            get { return stopBits.ToString(); }
            set { stopBits = ComHelper.GetStopBits(value); RaisePropertyChanged(); }
        }

        public string Parity
        {
            get { return parity.ToString(); }
            set { parity = ComHelper.GetParity(value); RaisePropertyChanged(); }
        }

        public string DataBits
        {
            get { return dataBits.ToString(); }
            set
            {
                try
                {
                    dataBits = Convert.ToInt32(value);
                }
                catch (Exception) { dataBits = 0; }

                RaisePropertyChanged();
            }
        }

        public bool RtsEnable
        {
            get { return rtsEnable; }
            set { rtsEnable = value; RaisePropertyChanged(); }
        }

        #endregion

        #region Field define

        private System.Timers.Timer timedTask = new System.Timers.Timer(10);

        private bool dataUpdate = false;

        //Used to record the total number of bytes sent
        private int numberBytesSendInt = 0;

        //Receive data display type, 0 for hexadecimal display, 1 for hexadecimal display text display
        private int receiveDisplayType = 0;

        //Used to hold the received bytes of data
        private List<byte> receiveBuffer = new List<byte>();

        //The text that the received data is ultimately presented to the user
        private string receiveData = string.Empty;

        //Open or close the serial port button prompt content
        private string openOrCloseButtonContent = "打开串口";

        private string logText = string.Empty;

        //Send data type, 0 for text, 1 for hexadecimal
        private int sendDataType = 0;

        private string sendDataLog = string.Empty;

        private string sendDataText = string.Empty;

        private string numberBytesReceived = "0";

        private string numberBytesSend = "0";

        private string receiveDataEncoding = "gb2312";

        private string sendDataEncoding = "gb2312";

        private ComConfig comConfig;

        public int maxShowByteCount = 6000;
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

        public ComConfig ComConfig
        {
            get { return comConfig; }
            set { comConfig = value; RaisePropertyChanged(); }
        }

        public List<string> EncodingName { get; set; }

        public string ReceiveDataEncoding
        {
            get { return receiveDataEncoding; }
            set { receiveDataEncoding = value; RaisePropertyChanged(); }
        }

        public string SendDataEncoding
        {
            get { return sendDataEncoding; }
            set { sendDataEncoding = value; RaisePropertyChanged(); }
        }

        public string MaxShowByteCount
        {
            get { return maxShowByteCount.ToString(); }
            set
            {
                try
                {
                    maxShowByteCount = Convert.ToInt32(value);
                }
                catch (Exception) { maxShowByteCount = 0; }

                RaisePropertyChanged();
            }
        }
        #endregion

        #region Serial port operation
        private SerialPort serialPort;
        /* Open a serial connection based on data set by the serial port */
        private bool OpenCOM()
        {
            if (serialPort != null)
            {
                CloseCom();
            }

            try
            {
                serialPort = new SerialPort(comName);

                serialPort.BaudRate = baudRate;
                serialPort.Parity = parity;
                serialPort.DataBits = dataBits;
                serialPort.StopBits = stopBits;

                serialPort.RtsEnable = rtsEnable;

                serialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);

                serialPort.Open();

                return serialPort.IsOpen;
            }
            catch (Exception)
            {
                serialPort = null;
            }
            return false;
        }

        /* Closes the serial port connection, setting the serial port to NULL if successful */
        private void CloseCom()
        {
            serialPort.Close();
            if (!serialPort.IsOpen) serialPort = null;
        }

        /* Scan available serial ports */
        private List<string> ScanCom()
        {
            List<string> list = new List<string>();
            for (int i = 1; i <= 30; ++i)
            {
                try
                {
                    SerialPort sp = new SerialPort("COM" + i.ToString());
                    sp.Open();
                    sp.Close();
                    list.Add("COM" + i.ToString());
                }
                catch (Exception) { }
            }

            return list;
        }

        #endregion

        #region Output operations
        private void PutLog(string text)
        {
            LogText += System.DateTime.Now.ToString();
            LogText += " : " + text + '\n';
        }

        private void PutSendDataLog(int type, string text)
        {
            if (text.Length == 0) return;
            SendDataLog += System.DateTime.Now.ToString() + '(' + (type == 1 ? "十六进制" : sendDataEncoding) + ')' + '\n';
            SendDataLog += text + '\n';
        }

        private void PutSendFileLog(string fileName)
        {
            SendDataLog += System.DateTime.Now.ToString() + "(File)\n";
            SendDataLog += fileName + '\n';
        }

        /* Displays data according to the display type set by the user */
        private void DisplayReceiveData()
        {
            byte[] buffer = receiveBuffer.ToArray();
            

            int offset = 0, count = buffer.Length;
            if(count > maxShowByteCount)
            {
                offset = count - maxShowByteCount;
                count = maxShowByteCount;
            }

            if (receiveDisplayType == 0)
                ReceiveData = ByteConvert.ToHex(buffer, offset, count);
            else
                ReceiveData = Encoding.GetEncoding(receiveDataEncoding).GetString(buffer, offset, count);
        }
        #endregion

        #region Command

        private void InitCommand()
        {
            OpenOrCloseCommand = new RelayCommand(OpenOrCloseCom);
            ClearOutputCommand = new RelayCommand(ClearOutput);
            SendDataCommand = new RelayCommand(SendData);
            RefreshSerialPortListCommand = new RelayCommand(RefreshSerialPortList);
            ReceiveDataSaveFileCommand = new RelayCommand(ReceiveDataSaveFile);
            SendFileCommand = new RelayCommand(SendFile);
        }

        public RelayCommand OpenOrCloseCommand { get; set; }
        public RelayCommand ClearOutputCommand { get; set; }
        public RelayCommand SendDataCommand { get; set; }
        public RelayCommand RefreshSerialPortListCommand { get; set; }
        public RelayCommand ReceiveDataSaveFileCommand { get; set; }
        public RelayCommand SendFileCommand { get; set; }
        #endregion

        #region Command realize

        /* Calling this function resets ReceiveData/SendDataLog to Empty and reset the number of bytes sent and received to zero */
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

        /* Open or close the serial port and update the content prompt on the button */
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
                PutLog("尝试打开" + comName + ", BaudRate=" + baudRate + ", Parity=" + parity.ToString() + ", DataBits=" + dataBits + ", StopBits=" + stopBits + ", " + (rtsEnable ? "RTS Start" : "RTS Close"));
                if (OpenCOM())
                {
                    PutLog("成功打开串口" );
                    OpenOrCloseButtonContent = "关闭串口";
                }
                else
                {
                    PutLog("打开串口失败");
                }
            }
        }

        /* Attempt to send data */
        private void SendData()
        {
            if(serialPort == null || !serialPort.IsOpen)
            {
                PutLog("请先连接串口后再发送数据!");
                return;
            }
            
            try
            {
                byte[] buffer;

                if (SendDataType == 0)
                {
                    buffer = Encoding.GetEncoding(sendDataEncoding).GetBytes(SendDataText);
                    PutSendDataLog(SendDataType, SendDataText);
                }
                else
                {
                    buffer = ByteConvert.ToBytes(SendDataText);
                    PutSendDataLog(SendDataType, ByteConvert.ToHex(buffer));
                }

                if (buffer.Length == 0)
                {
                    PutLog("发送数据为空!");
                    return;
                }

                serialPort.Write(buffer, 0, buffer.Length);

                numberBytesSendInt += buffer.Length;

                NumberBytesSend = numberBytesSendInt.ToString();
            }
            catch (Exception e)
            {
                PutLog(e.ToString());
            }
        }

        public void RefreshSerialPortList()
        {
            ComConfig.ComName = ScanCom();
            RaisePropertyChanged("ComConfig");
            if (ComConfig.ComName != null && ComConfig.ComName.Count != 0) ComName = ComConfig.ComName[0];
        }


        public void ReceiveDataSaveFile()
        {

            CommonSaveFileDialog dialog = new CommonSaveFileDialog();
            if(dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                PutLog("正在尝试将接收区数据保存到 " + dialog.FileName);
                new Thread(() =>
                {
                    
                    byte[] buffer;
                    if(File.Exists(dialog.FileName)) File.Delete(dialog.FileName);
                    using (var fileStream = new FileStream(dialog.FileName, FileMode.OpenOrCreate))
                    {
                        try
                        {
                            buffer = receiveBuffer.ToArray();
                            fileStream.Write(buffer, 0, buffer.Length);
                            PutLog("成功将接收区数据保存到文件 " + dialog.FileName);
                        }
                        catch (Exception)
                        {
                            PutLog("文件保存失败 " + dialog.FileName);
                        }
                    }

                }).Start();
            }
        }

        public void SendFile()
        {
            if (serialPort == null || !serialPort.IsOpen)
            {
                PutLog("请先连接串口后再发送数据!");
                return;
            }

            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            if(dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                PutLog("正在尝试发送文件 " + dialog.FileName);

                new Thread(() => 
                {
                    int count;
                    byte[] buffer = new byte[16];
                    using (var fileStream = new FileStream(dialog.FileName, FileMode.Open))
                    {
                        try
                        {
                            while ((count = fileStream.Read(buffer, 0, 16)) > 0)
                            {
                                serialPort.Write(buffer, 0, count);
                                numberBytesSendInt += count;
                                NumberBytesSend = numberBytesSendInt.ToString();
                            }

                            PutLog("成功发送文件 " + dialog.FileName);
                            PutSendFileLog(dialog.FileName);
                        }
                        catch(Exception)
                        {
                            PutLog("文件发送失败 " + dialog.FileName);
                        }
                    }

                }).Start();
                
            }
            
        }

        #endregion

        #region Event

        /* The serial port receives the data event callback function */
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            
            byte[] readBuffer = new byte[sp.ReadBufferSize];

            while(sp.BytesToRead > 0)
            {
                int size = sp.Read(readBuffer, 0, readBuffer.Length);
                for(int i = 0; i < size; ++i)
                    receiveBuffer.Add(readBuffer[i]);
            }

            dataUpdate = true;

        }

        #endregion

    }
}
