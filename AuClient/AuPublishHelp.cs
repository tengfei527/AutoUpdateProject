using AU.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace AuClient
{
    /// <summary>
    /// 包发布
    /// </summary>
    public class AuPublishHelp
    {
        /// <summary>
        /// 监听发布
        /// </summary>
        Nancy.Hosting.Self.NancyHost nancySelfHost = null;

        public MainForm UI = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        public AuPublishHelp(MainForm ui)
        {
            this.UI = ui;
            if (AppConfig.Current.AllowPublish)
                nancySelfHost = new Nancy.Hosting.Self.NancyHost(new Uri(AppConfig.Current.PublishAddress), new MyBootstrapper());
        }
        /// <summary>
        /// 取消控制变量
        /// </summary>
        private System.Threading.CancellationTokenSource cts;
        /// <summary>
        /// 启动
        /// </summary>
        /// <returns>启动结果</returns>
        public bool Start()
        {
            cts = new System.Threading.CancellationTokenSource();
            Task engineTask = new Task(() => Engine(cts.Token), cts.Token);
            engineTask.Start();
            if (AppConfig.Current.AllowPublish)
                nancySelfHost.Start();
            return true;
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns></returns>
        public void Stop()
        {
            if (cts != null)
                cts.Cancel();
            if (AppConfig.Current.AllowPublish)
                nancySelfHost.Stop();
        }
        /// <summary>
        /// 处理引擎
        /// </summary>
        private void Engine(System.Threading.CancellationToken ct)
        {
            while (true)
            {
                try
                {
                    AppPublish appPublish = new AppPublish(AppConfig.Current.SubSystem, AppConfig.Current.UpdateConfigPath, AppConfig.Current.PublishAddress);
                    AuPublish aup = null;

                    if (appPublish.CheckForUpdate(out aup) > 0 && aup != null)
                    {
                        if (ct.IsCancellationRequested)
                            break;
                        string file = appPublish.DownUpdateFile(aup, true);
                        if (System.IO.File.Exists(file))
                        {
                            this.UI.BeginInvoke((MethodInvoker)delegate ()
                            {
                                this.UI.ShowUpdate(file);
                            });
                        }
                    }
                    if (ct.IsCancellationRequested)
                        break;
                    this.UI.BeginInvoke((MethodInvoker)delegate ()
                    {
                        this.UI.Check();
                    });
                    if (ct.IsCancellationRequested)
                        break;
                    System.Threading.Thread.Sleep(AppConfig.Current.Interval);
                }
                catch (Exception e)
                {
                    //log
                }
            }
        }
    }
}
