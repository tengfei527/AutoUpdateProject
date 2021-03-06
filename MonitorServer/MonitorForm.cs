﻿using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonitorServer
{
    public partial class MainForm : Form
    {
        private IBootstrap bootstrap = BootstrapFactory.CreateBootstrap();
        public MainForm()
        {
            InitializeComponent();
            Console.SetOut(new Monitor.Common.ListTextWriter(this.lbLog));
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                StartResult result = bootstrap.Start();
                Console.WriteLine("Start result: {0}!", result);

                btnStop.Enabled = result != StartResult.Failed;
                btnStart.Enabled = !btnStop.Enabled;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                bootstrap.Stop();
                btnStart.Enabled = true;
                btnStop.Enabled = !btnStart.Enabled;
                Console.WriteLine("The server was stopped!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!bootstrap.Initialize())
            {
                Console.WriteLine("Failed to initialize!");
            }
        }
    }
}
