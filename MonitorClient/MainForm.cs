using AU.Monitor.Client;
using AU.Monitor.Client.Extensions;
using SuperSocket.ClientEngine;
using SuperSocket.ProtoBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonitorClient
{
    public partial class MainForm : Form
    {
        private EasyClient easyClient = new EasyClient();
        private readonly Encoding m_Encoding;
        public string cmdSpilts = "\r\n";
        public MainForm()
        {
            InitializeComponent();
            Console.SetOut(new Monitor.Common.ListTextWriter(this.lbLog));
            m_Encoding = System.Text.Encoding.UTF8;
            easyClient.Initialize(new FakeReceiveFilter(m_Encoding), (p =>
            {
                string body = p.Body;
                string key = p.Key;
                string[] par = p.Parameters;
                if (key != body)
                    Console.WriteLine("{0}:{1}", key, body);
                else
                    Console.WriteLine(key);
            }));
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            System.Net.IPAddress ip;
            if (System.Net.IPAddress.TryParse(tbIP.Text.Trim(), out ip))
            {
                Task<bool> result = easyClient.ConnectAsync(new System.Net.IPEndPoint(ip, (int)numericUpDown1.Value));
                Task.WaitAll(result);
                btnConnect.Enabled = !result.Result;
            }
            else
            {
                string msg = "服务器地址为空，请填写服务器地址";
                Console.WriteLine(msg);
                MessageBox.Show(msg);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (easyClient.IsConnected)
            {
                byte[] b = m_Encoding.GetBytes(tbMsg.Text.Trim().Replace("\r\n", "") + cmdSpilts);
                int t = b.Length / 1024;
                byte[] buff;
                for (int i = 0; i <= t; i++)
                {
                    if (i == t)
                    {
                        buff = new byte[b.Length - i * 1024];
                        Array.Copy(b, i * 1024, buff, 0, buff.Length);
                    }
                    else
                    {
                        buff = new byte[1024];
                        Array.Copy(b, i * 1024, buff, 0, 1024);
                    }

                    easyClient.Send(buff);
                }

            }
        }
    }


}
