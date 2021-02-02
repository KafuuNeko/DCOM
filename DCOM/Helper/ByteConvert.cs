using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DCOM.Helper
{
    class ByteConvert
    {
        public static string ToHex(byte data)
        {

            StringBuilder sb = new StringBuilder();


            int j = (data >> 4) & 0xF;
            char ch = (j < 10) ? ((char)((int)'0' + j)) : ((char)((int)'A' + j - 10));
            sb.Append(ch);

            j = data & 0xF;
            ch = (j < 10) ? ((char)((int)'0' + j)) : ((char)((int)'A' + j - 10));
            sb.Append(ch);

            return sb.ToString();
        }


        public static string ToHex(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; ++i)
            {
                if (i > 0) sb.Append(' ');
                sb.Append(ToHex(data[i]));
            }
            return sb.ToString();
        }

        private static int EffectiveDataLength(string str)
        {
            int result = 0;

            for (int i = 0; i < str.Length; ++i)
            {
                if ((str[i] >= 'a' && str[i] <= 'f') || (str[i] >= 'A' && str[i] <= 'F') || (str[i] >= '0' && str[i] <= '9'))
                    ++result;
            }

            return result;
        }

        public static byte[] ToBytes(string str)
        {
            bool flag = ((EffectiveDataLength(str) & 0x1) == 1);
            List<byte> result = new List<byte>();
            int tempWord = 0, tempByte;
            for (int i = 0; i < str.Length; ++i)
            {

                if (str[i] >= 'a' && str[i] <= 'f') tempByte = str[i] - 'a' + 10;
                else if (str[i] >= 'A' && str[i] <= 'F') tempByte = str[i] - 'A' + 10;
                else if (str[i] >= '0' && str[i] <= '9') tempByte = str[i] - '0';
                else continue;

                tempWord = ((tempWord << 4) + tempByte) & 0xFF;
                
                if (flag) result.Add((byte)tempWord);

                flag = !flag;
            }

            return result.ToArray();
        }
    }
}
