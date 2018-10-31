using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace ismClient
{
    class SocketSender
    {

        public void Sender(String filepath, System.Windows.Forms.ProgressBar bar_1)
        {

            Socket mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            mySocket.Connect(IPAddress.Loopback, 8192);
            FileStream fileStr = new FileStream(filepath, FileMode.Open, FileAccess.Read);


            //파일이름 전송
            String fileName = filepath.Split('\\')[filepath.Split('\\').Length - 1];

            byte[] buffer = BitConverter.GetBytes(fileName.Length);
            mySocket.Send(buffer);
            buffer = Encoding.UTF8.GetBytes(fileName);
            mySocket.Send(buffer);


            //파일전송
            int fileLength = (int)fileStr.Length;
            buffer = BitConverter.GetBytes(fileLength);
            mySocket.Send(buffer);
            
            int count = fileLength / 1024 + 1;

            bar_1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            bar_1.Minimum = 0;
            bar_1.Maximum = count;
            bar_1.Step = 1;
            bar_1.Value = 0;

            BinaryReader reader = new BinaryReader(fileStr);

            for (int i = 0; i < count; i++)
            {
                buffer = reader.ReadBytes(1024);

                mySocket.Send(buffer);
                bar_1.PerformStep();

            }

            reader.Close();
            mySocket.Close();
        }
    }
}
