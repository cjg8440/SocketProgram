using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ismServer
{
    class SocketReceive
    {
        private Socket mySocket;

        public void setMySocket(Socket mySocket)
        {
            this.mySocket = mySocket;
        }

        public void file_Receive()
        {
            //파일이름
            byte[] buffer = new Byte[4];
            mySocket.Receive(buffer);
            int nameLength = BitConverter.ToInt32(buffer, 0);
            buffer = new byte[nameLength];
            mySocket.Receive(buffer);

            String fileName = Encoding.UTF8.GetString(buffer);


            //파일전송
            buffer = new Byte[4];
            mySocket.Receive(buffer);
            int fileLength = BitConverter.ToInt32(buffer, 0);

            buffer = new Byte[1024];
            int totalLength = 0;

            FileStream fileStr = new FileStream(Directory.GetCurrentDirectory().ToString() + "\\prt\\" + fileName, FileMode.Create, FileAccess.Write);
            BinaryWriter writer = new BinaryWriter(fileStr);

            while (totalLength < fileLength)
            {
                int receiveLength = mySocket.Receive(buffer);
                writer.Write(buffer, 0, receiveLength);
                totalLength += receiveLength;
            }

            System.Console.WriteLine(DateTime.Now.ToString() + " - " + fileName + "전송 완료.");

            mySocket.Close();
            writer.Close();
            fileStr.Close();
        }

    }
}