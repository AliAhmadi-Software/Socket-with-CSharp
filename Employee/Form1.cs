using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;

namespace Employee
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        TcpClient tcp;
        NetworkStream ns;
        StreamReader reader;
        StreamWriter writer;
        string str = "";
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                tcp = new TcpClient("127.0.0.1",8086);
                tcp.ReceiveBufferSize = 25000;
                tcp.NoDelay = true;
                ns = tcp.GetStream();
                reader = new StreamReader(ns);
                writer = new StreamWriter(ns);
                writer.WriteLine("employee_register");
                writer.Flush();
                str = reader.ReadLine();
                string[] strsplit = null;
                strsplit = str.Split(',');
                if(strsplit[0] == "registered")
                {
                    button1.Visible = false;
                    this.Text = "کارمند شماره " + strsplit[1];
                    label1.Text = "در حال سرویس به مشتری شماره " + strsplit[2];
                }
            }
            catch
            {
                MessageBox.Show("خطا در اتصال به سرور. لطفا سرور را روشن کرده و یا پورت رو تغییر دهید.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                tcp = new TcpClient("127.0.0.1", 8086);
                tcp.ReceiveBufferSize = 25000;
                tcp.NoDelay = true;
                ns = tcp.GetStream();
                reader = new StreamReader(ns);
                writer = new StreamWriter(ns);
                writer.WriteLine("employee_next");
                writer.Flush();
                if (str != "")
                {
                    label1.Text = "در حال سرویس به مشتری " + str;
                }
                else
                {
                    label1.Text = "مشتری وجود ندارد!";
                }
            }
            catch
            {
                MessageBox.Show("خطا در اتصال به سرور. لطفا سرور را روشن کرده و یا پورت رو تغییر دهید.");
            }
        }
    }
}
