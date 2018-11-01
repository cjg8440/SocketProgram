using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace ismServer
{
    class Program
    {
        private static void Main()
        {
            Socket mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint point = new IPEndPoint(IPAddress.Any, 8192);
            mySocket.Bind(point);
            mySocket.Listen(1);
            System.Console.WriteLine(DateTime.Now.ToString() + " - Server Start!");


            while (true)
            {
                SocketReceive receive = new SocketReceive();

                receive.file_Receive(mySocket.Accept());

            }


            
            mySocket.Close();
        }
    }
}
