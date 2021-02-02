using DCOM.Helper;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace DCOM.Model
{
    class ComConfig 
    {

        public List<int> BaudRate { get; set; }
        public List<string> ComName { get; set; }
        public Array StopBit { get; set; }
        public Array CheckMode { get; set; }
        public List<int> DataBits { get; set; }

        public ComConfig()
        {
            BaudRate = new List<int>() { 110, 300, 600, 1200, 2400, 4800, 9600, 14400, 19200, 38400, 56000, 57600, 115200};
            DataBits = new List<int>() { 5, 6, 7, 8 };

            StopBit = Enum.GetValues(typeof(StopBits));
            CheckMode = Enum.GetValues(typeof(Parity));
        }

    }
}
