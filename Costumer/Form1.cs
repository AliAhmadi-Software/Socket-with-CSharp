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

namespace Costumer
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
            string tim, count, num;
            try
            {
                tcp = new TcpClient(textBox1.Text,int.Parse(textBox2.Text));
                tcp.ReceiveBufferSize = 25000;
                tcp.NoDelay = true;
                ns = tcp.GetStream();
                reader = new StreamReader(ns);
                writer = new StreamWriter(ns);
                writer.WriteLine("request");
                writer.Flush();
                str = reader.ReadLine();

                string[] strsplit = null;
                strsplit = str.Split(',');
                if(strsplit[0] != "noemployee")
                {
                    tim = strsplit[0];
                    count = strsplit[1];
                    num = strsplit[2];

                    label1.Text = "نوبت شما حدود " + tim.ToString() + " دقیقه دیگر";
                    label2.Text = "تعداد افراد موجود در صف " + count.ToString() + " نفر";
                    label3.Text = "شماره شما  " + num.ToString();

                }
                else
                {
                    MessageBox.Show("کارمندی برای پاسخگویی در دسترس نیست.");
                }
            }
            catch
            {
                MessageBox.Show("خطا در اتصال به سرور. لطفا سرور را روشن نمایید و یا پورت اتصال به سرور را تغییر دهید.");
                return;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
