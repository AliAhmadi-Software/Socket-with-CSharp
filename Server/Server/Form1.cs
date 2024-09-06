using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections;


namespace Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        TcpListener tc;
        Socket s;
        Queue customer = new Queue(2000);
        int numberCustomer = 0;
        int numberEmployee = 0;
        int tim, count, num;
        List<string> customers = new List<string>();
        List<string> employee = new List<string>();
        private void init()
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            tc = new TcpListener(ip,8086);
            tc.Start();
            while(true)
            {
                s = tc.AcceptSocket();
                Thread t = new Thread(new ThreadStart(reply));
                t.IsBackground = true;
                t.Start();
            }
        }
        private void reply()
        {
            Socket sc = s;
            NetworkStream ns = new NetworkStream(sc);
            StreamReader reader = new StreamReader(ns);
            StreamWriter writer = new StreamWriter(ns);

            string str = "";
            string response = "";
            try { str = reader.ReadLine(); }
            catch { str = "error"; }
            if (str == "request")
            {
                if(employee.Count>0)
                {
                    addQueue();
                    response = tim.ToString() + "," + count.ToString() + "," + num.ToString();
                }
                else
                {
                    response = "noemployee, , ";
                }
            }
            else if(str=="employee_register")
            {
                numberEmployee++;
                employee.Add(" کارمند شماره"+numberEmployee.ToString());
                string nc = "";
                if (customer.Count > 0)
                {
                    nc = customer.Dequeue().ToString();
                    customers.Remove("مشتری شماره "+nc.ToString());

                }
                response = "registered,"+numberEmployee+ToString()+","+nc;
            }
            else if (str=="employee_next")
            {
                string nc = "";
                if(customer.Count>0)
                {
                    nc = customer.Dequeue().ToString();
                    customers.Remove("مشتری شماره " + nc.ToString());
                }
                response = nc;
            }
            writer.WriteLine(response);
            writer.Flush();

            ns.Close();
            sc.Close();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(customers.Count!=listBox2.Items.Count)
            {
                listBox2.Items.Clear();
                foreach( string item in customers)
                {
                    listBox2.Items.Add(item);
                }
            }
            if(employee.Count != listBox1.Items.Count)
            {
                listBox1.Items.Clear();
                foreach(string item in employee )
                {
                    listBox1.Items.Add(item);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Trim()!="")
            {
                textBox1.BackColor = Color.White;
                Thread t = new Thread(new ThreadStart(init));
                t.IsBackground = true;
                t.Start();

                MessageBox.Show("سرور شروع به کار کرده و آماده انجام درخواست ها می باشد.");

                button1.Enabled = false;
                textBox1.Enabled = false;
            }
            else
            {
                textBox1.BackColor = Color.Red;
            }
        }

        public void addQueue()
        {
            numberCustomer += 1;
            customer.Enqueue(numberCustomer);
            tim = ((int.Parse(customer.Count.ToString())) * int.Parse(textBox1.Text) )/ employee.Count;
            count = int.Parse(customer.Count.ToString());
            num = numberCustomer;
            customers.Add("مشتری شماره "+numberCustomer.ToString());

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
