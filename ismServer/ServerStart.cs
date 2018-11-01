using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ismServer
{
    class ServerStart
    {
        private Socket mySocket;
        private int a;
        private int b;

        public Socket getmySocket()
        {
            return this.mySocket;
        }

        public void Start()
        {
            mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint point = new IPEndPoint(IPAddress.Any, 8192);
            FileStream file = new FileStream("receive.log", FileMode.Append, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file);
            mySocket.Bind(point);
            mySocket.Listen(1);
            writer.WriteLine(DateTime.Now.ToString() + " - Server Start!");

            while (true)
            {
                SocketReceive receive = new SocketReceive();

                receive.setMySocket(mySocket.Accept());

                ThreadStart ts = new ThreadStart(receive.file_Receive);
                Thread th = new Thread(ts);

                th.Start();
            }
        }
    }
}
