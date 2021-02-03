using System;
using System.IO;
using System.IO.Ports;
using System.Runtime.Serialization.Formatters.Binary;

namespace DCOM.Helper
{
    [Serializable]
    class AppSetting
    {

        #region Serial port property
        public string comName = string.Empty;
        public int baudRate = 115200;
        public Parity parity = System.IO.Ports.Parity.None;
        public int dataBits = 8;
        public StopBits stopBits = StopBits.One;
        public bool rtsEnable = false;
        #endregion

        #region Field define

        public string receiveDataEncoding = "gb2312";

        public string sendDataEncoding = "gb2312";

        public int maxShowByteCount = 6000;

        public bool logLineFeedSplitsTimeAndContent = false;

        public bool sendLogLineFeedSplitsTimeAndContent = true;

        public bool receiveLogLineFeedSplitsTimeAndContent = true;

        public string receiveLogTimeFormat = "yyyy/MM/dd HH:mm:ss  fff:ffffff";

        public string sendLogTimeFormat = "yyyy/MM/dd HH:mm:ss";

        public string logTimeFormat = "yyyy/MM/dd HH:mm:ss";

        public int fileSendingDelay = 0;
        #endregion

        public void Save()
        {
            using (var fs = new FileStream("AppSetting.bin", FileMode.OpenOrCreate))
            {
                new BinaryFormatter().Serialize(fs, this);
            }
        }

        public static AppSetting Load()
        {
            using (var fs = new FileStream("AppSetting.bin", FileMode.OpenOrCreate))
            {
                try
                {
                    var obj = new BinaryFormatter().Deserialize(fs);
                    return obj as AppSetting;
                } catch (Exception)
                {
                    return new AppSetting();
                }
            }
        }

    }
}
