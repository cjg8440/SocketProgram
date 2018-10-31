using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Net;

namespace SocketClient
{
    class Program
    {

        private static void Main()
        {
            Socket mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            mySocket.Connect(IPAddress.Loopback, 8192);
            FileStream fileStr = new FileStream("test.jpg", FileMode.Open, FileAccess.Read);
            int fileLength = (int)fileStr.Length;
            byte[] buffer = BitConverter.GetBytes(fileLength);
            mySocket.Send(buffer);

            int count = fileLength / 1024 + 1;

            BinaryReader reader = new BinaryReader(fileStr);

            for(int i =0; i < count; i++)
            {
                buffer = reader.ReadBytes(1024);

                mySocket.Send(buffer);
            }

            reader.Close();
            mySocket.Close();
        }

    }
}
