using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace ismServer
{
    public partial class ismServer : Form
    {
        private ServerStart s_Start;
        private ThreadStart ts;
        private Thread th;


        public ismServer()
        {
            InitializeComponent();
            s_Start = new ServerStart();
            ts = new ThreadStart(s_Start.Start);
            th = new Thread(ts);

            button1.Text = "서버 시작";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (button1.Text.Equals("서버 종료"))
            {
                s_Start.getmySocket().Close();
                th.Abort();
                button1.Text = "서버 시작";
            }
            else
            {
                th.Start();
                button1.Text = "서버 종료";
            }

        }
    }
}
