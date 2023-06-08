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

namespace DCOM.ViewModel
{
    class MainWindowModel : ViewModelBase
    {
        private AppSetting setting = AppSetting.Load();

        public MainWindowModel()
        {
            InitQueueManager();
            InitCommand();
            InitTimeTask();

            var encodings = Encoding.GetEncodings();
            EncodingName = new List<string>();
            for (int i = 0; i < encodings.Length; ++i)
                EncodingName.Add(encodings[i].Name);

            this.ComConfig = new ComConfig();
            RefreshSerialPortList();

            PutLog("Start");

        }

        #region Time task

        private void InitTimeTask()
        {
            timedTask.Elapsed += TimedTask;
            timedTask.Enabled = true;
            timedTask.Start();
            timedTask.AutoReset = false;
        }

        private void TimedTask(Object source, ElapsedEventArgs e)
        {
            if (dataUpdate)
            {
                dataUpdate = false;
                //Update the number of received bytes displayed
                NumberBytesReceived = receiveBuffer.Count.ToString();
                //Update the displayed data
                DisplayReceiveData();
            }
            timedTask.Start();
        }

        #endregion

        #region Serial port property

        public string ComName
        {
            get { return setting.comName; }
            set { setting.comName = value; RaisePropertyChanged(); setting.Save(); }
        }


        public string BaudRate
        {
            get { return setting.baudRate.ToString(); }
            set
            {
                try
                {
                    setting.baudRate = Convert.ToInt32(value);
                }
                catch (Exception) { setting.baudRate = 0; }

                RaisePropertyChanged();
                setting.Save();
            }
        }

        public string StopBit
        {
            get { return setting.stopBits.ToString(); }
            set { setting.stopBits = ComHelper.GetStopBits(value); RaisePropertyChanged(); setting.Save(); }
        }

        public string Parity
        {
            get { return setting.parity.ToString(); }
            set { setting.parity = ComHelper.GetParity(value); RaisePropertyChanged(); setting.Save(); }
        }

        public string DataBits
        {
            get { return setting.dataBits.ToString(); }
            set
            {
                try
                {
                    setting.dataBits = Convert.ToInt32(value);
                }
                catch (Exception) { setting.dataBits = 0; }

                RaisePropertyChanged();
                setting.Save();
            }
        }

        public bool RtsEnable
        {
            get { return setting.rtsEnable; }
            set { setting.rtsEnable = value; RaisePropertyChanged(); setting.Save(); }
        }

        #endregion

        #region Field define

        private ComConfig comConfig;

        private System.Timers.Timer timedTask = new System.Timers.Timer(100);

        private bool dataUpdate = false;

        //Used to record the total number of bytes sent
        private int numberBytesSendInt = 0;

        //Receive data display type, 0 for hexadecimal display, 1 for hexadecimal display text display
        private int receiveDisplayType = 0;

        //Used to hold the received bytes of data
        private ReceiveBuffer receiveBuffer = new ReceiveBuffer();

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

        private Thread sendFileThread = null;

        private bool sendFileStatus = false;

        private QueueManager queueManager;

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
            get { return setting.receiveDataEncoding; }
            set { setting.receiveDataEncoding = value; RaisePropertyChanged(); setting.Save(); }
        }

        public string SendDataEncoding
        {
            get { return setting.sendDataEncoding; }
            set { setting.sendDataEncoding = value; RaisePropertyChanged(); setting.Save(); }
        }

        public string MaxShowByteCount
        {
            get { return setting.maxShowByteCount.ToString(); }
            set
            {
                try
                {
                    setting.maxShowByteCount = Convert.ToInt32(value);
                }
                catch (Exception) { setting.maxShowByteCount = 0; }

                RaisePropertyChanged();
                setting.Save();
            }
        }

        public bool LogLineFeedSplitsTimeAndContent
        {
            get { return setting.logLineFeedSplitsTimeAndContent; }
            set { setting.logLineFeedSplitsTimeAndContent = value; RaisePropertyChanged(); setting.Save(); }
        }

        public bool SendLogLineFeedSplitsTimeAndContent
        {
            get { return setting.sendLogLineFeedSplitsTimeAndContent; }
            set { setting.sendLogLineFeedSplitsTimeAndContent = value; RaisePropertyChanged(); setting.Save(); }
        }

        public bool ReceiveLogLineFeedSplitsTimeAndContent
        {
            get { return setting.receiveLogLineFeedSplitsTimeAndContent; }
            set { setting.receiveLogLineFeedSplitsTimeAndContent = value; RaisePropertyChanged(); setting.Save(); }
        }

        public string ReceiveLogTimeFormat
        {
            get { return setting.receiveLogTimeFormat; }
            set { setting.receiveLogTimeFormat = value; setting.Save(); }
        }

        public string SendLogTimeFormat
        {
            get { return setting.sendLogTimeFormat; }
            set { setting.sendLogTimeFormat = value; setting.Save(); }
        }

        public string LogTimeFormat
        {
            get { return setting.logTimeFormat; }
            set { setting.logTimeFormat = value; setting.Save(); }
        }

        public string FileSendingDelay
        {
            get { return setting.fileSendingDelay.ToString(); }
            set
            {
                try
                {
                    setting.fileSendingDelay = Convert.ToInt32(value);
                }
                catch (Exception) { setting.fileSendingDelay = 0; }

                RaisePropertyChanged();
                setting.Save();
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
                serialPort = new SerialPort(setting.comName);

                serialPort.BaudRate = setting.baudRate;
                serialPort.Parity = setting.parity;
                serialPort.DataBits = setting.dataBits;
                serialPort.StopBits = setting.stopBits;

                serialPort.RtsEnable = setting.rtsEnable;

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

        #region QueueManager
        private void InitQueueManager()
        {
            this.queueManager = new QueueManager();

            this.queueManager.ByteDataReady += (data) =>
            {
                serialPort.Write(data, 0, data.Length);
                numberBytesSendInt += data.Length;
                NumberBytesSend = numberBytesSendInt.ToString();
            };

        }

        #endregion

        #region Output operations
        private void PutLog(string text)
        {
            LogText += System.DateTime.Now.ToString(setting.logTimeFormat);
            LogText += setting.logLineFeedSplitsTimeAndContent ? '\n' : '：';
            LogText += text + '\n';
        }

        private void PutSendDataLog(int type, string text)
        {

            if (text.Length == 0) return;
            SendDataLog += System.DateTime.Now.ToString(setting.sendLogTimeFormat) + '(' + (type == 1 ? "十六进制" : setting.sendDataEncoding) + ')';
            SendDataLog += setting.sendLogLineFeedSplitsTimeAndContent ? '\n' : '：';
            SendDataLog += text + '\n';
        }

        private void PutSendFileLog(string fileName)
        {
            SendDataLog += System.DateTime.Now.ToString(setting.sendLogTimeFormat) + "(File)";
            SendDataLog += setting.sendLogLineFeedSplitsTimeAndContent ? '\n' : '：';
            SendDataLog += fileName + '\n';
        }

        /* Displays data according to the display type set by the user */
        private void DisplayReceiveData()
        {
            if (receiveDisplayType >= 0 && receiveDisplayType <= 2)
            {
                byte[] buffer = receiveBuffer.ToArray();

                int offset = 0, count = buffer.Length;
                if (count > setting.maxShowByteCount)
                {
                    offset = count - setting.maxShowByteCount;
                    count = setting.maxShowByteCount;
                }

                if (receiveDisplayType == 0)
                    ReceiveData = ByteConvert.BytesToHexString(buffer, offset, count);
                else if (receiveDisplayType == 1)
                    ReceiveData = Encoding.GetEncoding(setting.receiveDataEncoding).GetString(buffer, offset, count);
                else
                    ReceiveData = ByteConvert.BytesToBinString(buffer, offset, count);
            }
            else
            {
                int count = 0;

                ReceiveBuffer.Block tempBlock;
                Stack<string> stack = new Stack<string>();

                for (int i = receiveBuffer.BlockCount - 1; i >= 0; --i)
                {
                    tempBlock = receiveBuffer.FindBlock(i);

                    int putCount = setting.maxShowByteCount - count;
                    if (putCount > tempBlock.Data.Length)
                    {
                        putCount = tempBlock.Data.Length;
                    }

                    if (putCount == 0) break;

                    string head = tempBlock.Time.ToString(setting.receiveLogTimeFormat) + (setting.receiveLogLineFeedSplitsTimeAndContent ? '\n' : ':');

                    if (receiveDisplayType == 3)
                        stack.Push(head + ByteConvert.BytesToHexString(tempBlock.Data, tempBlock.Data.Length - putCount, putCount) + '\n');
                    else if (receiveDisplayType == 4)
                        stack.Push(head + Encoding.GetEncoding(setting.receiveDataEncoding).GetString(tempBlock.Data, tempBlock.Data.Length - putCount, putCount) + '\n');
                    else
                        stack.Push(head + ByteConvert.BytesToBinString(tempBlock.Data, tempBlock.Data.Length - putCount, putCount) + '\n');

                    count += tempBlock.Data.Length;
                    if (count > setting.maxShowByteCount) break;
                }

                StringBuilder finalDisplay = new StringBuilder();
                while (stack.Count > 0) finalDisplay.Append(stack.Pop());
                ReceiveData = finalDisplay.ToString();
            }
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
            if (serialPort != null && serialPort.IsOpen)
            {
                CloseCom();
                if (serialPort == null)
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
                PutLog("尝试打开" + setting.comName
                    + ", BaudRate=" + setting.baudRate
                    + ", Parity=" + setting.parity.ToString()
                    + ", DataBits=" + setting.dataBits
                    + ", StopBits=" + setting.stopBits + ", "
                    + (setting.rtsEnable ? "RTS Start" : "RTS Close"));
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

        /* Attempt to send data */
        private void SendData()
        {
            if (serialPort == null || !serialPort.IsOpen)
            {
                PutLog("请先连接串口后再发送数据!");
                return;
            }

            try
            {
                byte[] buffer;

                if (SendDataType == 0)
                {
                    buffer = Encoding.GetEncoding(setting.sendDataEncoding).GetBytes(SendDataText);
                    PutSendDataLog(SendDataType, SendDataText);
                }
                else if (SendDataType == 1)
                {
                    buffer = ByteConvert.HexStringToBytes(SendDataText);
                    PutSendDataLog(SendDataType, ByteConvert.BytesToHexString(buffer));
                }
                else
                {
                    buffer = ByteConvert.BinStringToBytes(SendDataText);
                    PutSendDataLog(SendDataType, ByteConvert.BytesToBinString(buffer));
                }

                if (buffer.Length == 0)
                {
                    PutLog("发送数据为空!");
                    return;
                }

                this.queueManager.AddData(buffer);
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
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                PutLog("正在尝试将接收区数据保存到 " + dialog.FileName);
                new Thread(() =>
                {
                    byte[] buffer;
                    if (File.Exists(dialog.FileName)) File.Delete(dialog.FileName);
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

            if (sendFileStatus)
            {
                PutLog("正在尝试中止发送文件!");
                sendFileStatus = false;
                return;
            }

            if (sendFileThread != null)
            {
                PutLog("请等待上一次发送文件线程结束!");
                return;
            }

            sendFileStatus = true;
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                PutLog("正在尝试发送文件(发送文件过程中再次点击发送文件即可中止发送) " + dialog.FileName);

                sendFileThread = new Thread(() =>
                {
                    int count;
                    byte[] buffer = new byte[8];
                    using (var fileStream = new FileStream(dialog.FileName, FileMode.Open))
                    {
                        try
                        {
                            while ((count = fileStream.Read(buffer, 0, 8)) > 0)
                            {
                                if (!sendFileStatus) break;
                                serialPort.Write(buffer, 0, count);
                                numberBytesSendInt += count;
                                NumberBytesSend = numberBytesSendInt.ToString();
                                Thread.Sleep(setting.fileSendingDelay);
                            }

                            if (sendFileStatus)
                            {
                                PutLog("成功发送文件 " + dialog.FileName);
                                PutSendFileLog(dialog.FileName);
                            }
                            else
                            {
                                PutLog("已中止文件发送 " + dialog.FileName);
                            }
                        }
                        catch (Exception)
                        {
                            PutLog("文件发送失败 " + dialog.FileName);
                        }
                    }

                    sendFileStatus = false;
                    sendFileThread = null;
                });
                sendFileThread.Start();


            }

        }

        #endregion

        #region Event

        /* The serial port receives the data event callback function */
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;

            byte[] readBuffer = new byte[sp.ReadBufferSize];
            while (sp.BytesToRead > 0)
            {
                int size = sp.Read(readBuffer, 0, readBuffer.Length);
                receiveBuffer.Add(readBuffer, 0, size);
            }

            dataUpdate = true;
        }

        #endregion

    }
}
