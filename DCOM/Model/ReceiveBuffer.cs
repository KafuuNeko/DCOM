using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCOM.Model
{
    class ReceiveBuffer
    {
        public class Block
        {
            public DateTime Time { get; set; }
            public byte[] Data { get; set; }
        }

        private List<Block> blocks = new List<Block>();

        public int Count { get; set; }
        public int BlockCount
        {
            get { return blocks.Count; }
        }

        public ReceiveBuffer()
        {
            Count = 0;
        }

        public void Add(byte[] data)
        {
            blocks.Add(new Block() { Time = System.DateTime.Now, Data = data });
            Count += data.Length;
        }

        public void Add(byte[] data, int offset, int count)
        {
            byte[] temp = new byte[count];
            Array.Copy(data, offset, temp, 0, count);
            Add(temp);
        }

        public void Clear()
        {
            Count = 0;
            blocks = new List<Block>();
        }

        public Block FindBlock(int blockIndex)
        {
            return blocks[blockIndex];
        }

        public byte[] ToArray(int index)
        {
            return blocks[index].Data;
        }

        public byte[] ToArray()
        {
            int bc = BlockCount;
            byte[] result = new byte[Count];
            int offset = 0;

            for(int i = 0; i < bc; ++i)
            {
                Array.Copy(blocks[i].Data, 0, result, offset, blocks[i].Data.Length);
                offset += blocks[i].Data.Length;
            }

            return result;
        }

    }
}
