using System;
using System.Collections.Generic;
using System.Threading;

namespace DCOM.Helper
{
    public class QueueManager
    {
        private Queue<byte[]> byteQueue;
        private Queue<string> fileQueue;
        private Thread thread;
        private bool isRunning;

        public event Action<byte[]> ByteDataReady;
        public event Action<string> FileDataReady;

        public QueueManager()
        {
            byteQueue = new Queue<byte[]>();
            fileQueue = new Queue<string>();
            thread = new Thread(ProcessQueue);
            isRunning = true;
            thread.Start();
        }

        public void AddData(byte[] data)
        {
            lock (byteQueue)
            {
                byteQueue.Enqueue(data);
            }
        }

        public void AddFile(string filePath)
        {
            lock (fileQueue)
            {
                fileQueue.Enqueue(filePath);
            }
        }

        private void ProcessQueue()
        {
            while (isRunning)
            {
                if (byteQueue.Count > 0)
                {
                    byte[] data;
                    lock (byteQueue)
                    {
                        data = byteQueue.Dequeue();
                    }
                    ByteDataReady?.Invoke(data);
                }

                if (fileQueue.Count > 0)
                {
                    string filePath;
                    lock (fileQueue)
                    {
                        filePath = fileQueue.Dequeue();
                    }
                    FileDataReady?.Invoke(filePath);
                }

                // Add any additional processing or delays here

                // Sleep for a while to avoid consuming excessive CPU
                Thread.Sleep(100);
            }
        }

        public void Stop()
        {
            isRunning = false;
            thread.Join();
        }
    }

}
