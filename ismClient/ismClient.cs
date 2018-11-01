using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ismClient
{
    public partial class ismClient : Form
    {

        private String file_path = null;

        public ismClient()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.Filter = "prt 파일(*.prt) | *.prt|모든파일(*.*)|*.*";
            openFileDialog1.FileName = null;

            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file_path = openFileDialog1.FileName;

                textBox1.Text = file_path.Split('\\')[file_path.Split('\\').Length-1];
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SocketSender send = new SocketSender();

            send.Sender(file_path,progressBar1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}
