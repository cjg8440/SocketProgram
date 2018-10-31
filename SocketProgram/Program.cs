using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace SocketServer
{
    class Program
    {
        private static void Main()
        {
            Socket mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint point = new IPEndPoint(IPAddress.Loopback, 8192);
            mySocket.Bind(point);
            mySocket.Listen(1);
            mySocket = mySocket.Accept();
            byte[] buffer = new byte[4];
            mySocket.Receive(buffer);
            int fileLength = BitConverter.ToInt32(buffer, 0);
            buffer = new Byte[1024];
            int totalLength = 0;

            FileStream fileStr = new FileStream("music.mp3", FileMode.Create, FileAccess.Write);
            BinaryWriter writer = new BinaryWriter(fileStr);

            while (totalLength < fileLength)
            {
                int receiveLength = mySocket.Receive(buffer);
                writer.Write(buffer, 0, receiveLength);
                totalLength += receiveLength;
            }

            writer.Close();
            mySocket.Close();
        }


    }
}
