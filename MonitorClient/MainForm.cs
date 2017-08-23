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
        private List<EasyClient> easyClients = new List<SuperSocket.ClientEngine.EasyClient>();
        private readonly Encoding m_Encoding;
        public string cmdSpilts = "\r\n";
        public MainForm()
        {
            InitializeComponent();
            Console.SetOut(new Monitor.Common.ListTextWriter(this.lbLog));
            m_Encoding = System.Text.Encoding.UTF8;

        }
        System.Net.IPAddress ip;
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text == "连接")
            {
                if (System.Net.IPAddress.TryParse(tbIP.Text.Trim(), out ip))
                {
                    btnConnect.Enabled = false;
                    new Task(() =>
                    {
                        for (int i = 0; i < numericUpDown2.Value; i++)
                        {
                            var d = new EasyClient();
                            d.Initialize(new FakeReceiveFilter(m_Encoding), (p =>
                             {
                                 string body = p.Body;
                                 string key = p.Key;
                                 string[] par = p.Parameters;
                                 if (key != body)
                                     Console.WriteLine("{0}:{1}", key, body);
                                 else
                                     Console.WriteLine(key);
                             }));
                            this.Invoke((MethodInvoker)delegate ()
                            {
                                comboBox1.Items.Add(i);
                            });
                            Task<bool> result = d.ConnectAsync(new System.Net.IPEndPoint(ip, (int)numericUpDown1.Value));
                            Task.WaitAll(result);
                            Console.WriteLine("{0}:启动[{1}]", i, result.Result);
                            this.easyClients.Add(d);
                            System.Threading.Thread.Sleep(10);

                        }
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            btnConnect.Text = "断开";
                            btnSend.Enabled = true;
                            btnConnect.Enabled = true;
                        });

                    }).Start();
                }
                else
                {
                    string msg = "服务器地址为空，请填写服务器地址";
                    Console.WriteLine(msg);
                    MessageBox.Show(msg);
                }
            }
            else
            {
                int i = 0;
                foreach (var d in easyClients)
                {
                    bool rs = true;
                    if (d.IsConnected)
                    {
                        Task<bool> result = d.Close();
                        Task.WaitAll(result);
                        System.Threading.Thread.Sleep(10);
                    }
                    Console.WriteLine("{0}:关闭[{1}]", i, rs);
                }
                easyClients.Clear();
                comboBox1.Items.Clear();
                btnSend.Enabled = false;
                btnConnect.Text = "连接";
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("请选择发送终端");
                return;
            }
            var c = easyClients[comboBox1.SelectedIndex];
            if (!c.IsConnected)
            {
                Task<bool> result = c.ConnectAsync(new System.Net.IPEndPoint(ip, (int)numericUpDown1.Value));
                Task.WaitAll(result);
                Console.WriteLine("{0}:启动[{1}]", comboBox1.SelectedIndex, result.Result);
            }
            if (!c.IsConnected)
            {
                MessageBox.Show("终端无法连接服务，请稍后继续");
                return;
            }

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

                c.Send(buff);
            }

        }
    }


}
