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
        public static string ByteToHexString(byte data)
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


        public static string BytesToHexString(byte[] data, int offset = 0, int count = 0)
        {
            StringBuilder sb = new StringBuilder();

            int size = count;

            if (size == 0) size = data.Length;

            for (int i = 0; i < size; ++i)
            {
                if (i > 0) sb.Append(' ');
                sb.Append(ByteToHexString(data[offset + i]));
            }

            return sb.ToString();
        }

        private static int HexStringEffectiveDataLength(string str)
        {
            int result = 0;

            for (int i = 0; i < str.Length; ++i)
            {
                if ((str[i] >= 'a' && str[i] <= 'f') || (str[i] >= 'A' && str[i] <= 'F') || (str[i] >= '0' && str[i] <= '9'))
                    ++result;
            }

            return result;
        }

        public static byte[] HexStringToBytes(string str)
        {
            bool flag = ((HexStringEffectiveDataLength(str) & 0x1) == 1);
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

        public static string ByteToBinString(byte data)
        {
            StringBuilder sb = new StringBuilder();

            for(int i = 7; i >= 0; --i)
            {
                sb.Append((data >> i) & 0x1);
            }
            return sb.ToString();
        }


        public static string BytesToBinString(byte[] data, int offset = 0, int count = 0)
        {
            StringBuilder sb = new StringBuilder();

            int size = count;

            if (size == 0) size = data.Length;

            for (int i = 0; i < size; ++i)
            {
                if (i > 0) sb.Append(' ');
                sb.Append(ByteToBinString(data[offset + i]));
            }

            return sb.ToString();
        }

        private static int BinStringEffectiveDataLength(string str)
        {
            int result = 0;

            for (int i = 0; i < str.Length; ++i)
            {
                if (str[i] == '0' || str[i] == '1') ++result;
            }

            return result;
        }

        public static byte[] BinStringToBytes(string str)
        {
            List<byte> result = new List<byte>();
            int metering = (8 - (BinStringEffectiveDataLength(str) % 8)) % 8;

            int tempByte = 0;
            for (int i = 0; i < str.Length; ++i)
            {
                if (str[i] == '0' || str[i] == '1') tempByte = (tempByte << 1) + (str[i] - '0');
                else continue;

                if(++metering == 8)
                {
                    metering = 0;
                    result.Add((byte)tempByte);
                }
            }

            return result.ToArray();
        }


    }
}
