﻿using SuperSocket.ClientEngine;
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
            Console.SetOut(new ListTextWriter(this.lbLog));
            m_Encoding = System.Text.Encoding.Default;
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
                Task<bool> result = easyClient.ConnectAsync(new System.Net.IPEndPoint(ip, 5275));
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
                easyClient.Send(m_Encoding.GetBytes(tbMsg.Text.Trim() + cmdSpilts));
            }
        }
    }

    public class FakeReceiveFilter : TerminatorReceiveFilter<StringPackageInfo>
    {
        public System.Text.Encoding MessageEncoding { get; private set; }
        public FakeReceiveFilter(System.Text.Encoding m_Encoding, string terminator = "\r\n")
            : base(m_Encoding.GetBytes(terminator))
        {
            this.MessageEncoding = m_Encoding;
        }

        public override StringPackageInfo ResolvePackage(IBufferStream bufferStream)
        {
            var buffer = bufferStream.Read();
            string msg = MessageEncoding.GetString(buffer);
            return new StringPackageInfo(msg.Substring(0, msg.Length - 2), new BasicStringParser(":", ","));
        }
    }
    public static class IBufferStreamExtensions
    {
        public static byte[] Read(this IBufferStream bufferStream)
        {
            var buffer = new byte[bufferStream.Length];

            bufferStream.Read(buffer, 0, (int)bufferStream.Length);
            return buffer;
        }
    }
}