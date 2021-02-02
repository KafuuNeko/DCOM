using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCOM.Helper
{
    class ComHelper
    {
        public static StopBits GetStopBits(string emp)
        {
            switch (emp)
            {
                case "One": return StopBits.One;
                case "Two": return StopBits.Two; 
                case "OnePointFive": return StopBits.OnePointFive; 
            }
            return StopBits.None;
        }

        public static Parity GetParity(string emp)
        {
            switch (emp)
            {
                case "None": return Parity.None;
                case "Even": return Parity.Even;
                case "Mark": return Parity.Mark; 
                case "Odd": return Parity.Odd;
                case "Space": return Parity.Space;
            }
            return Parity.None;
        }
    }
}
