using AU.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace AuClient
{
    public class AuPublishHelp
    {
        /// <summary>
        /// 更新地址
        /// </summary>
        public string UpdatePath { get; private set; }

        public bool AllowPublish { get; set; }
        /// <summary>
        /// 发布地址
        /// </summary>
        public string PublishAddress { get; set; }
        /// <summary>
        /// 是否UI
        /// </summary>
        public bool AllowUI { get; set; }
        /// <summary>
        /// 执行频率
        /// </summary>
        public int Interval { get; set; }
        /// <summary>
        /// 更新子系统
        /// </summary>
        public string SubSystem { get; set; }
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
            this.Interval = 5000;
            this.AllowPublish = "true".Equals(System.Configuration.ConfigurationManager.AppSettings["AllowPublish"], StringComparison.InvariantCultureIgnoreCase);
            this.AllowUI = "true".Equals(System.Configuration.ConfigurationManager.AppSettings["AllowUI"], StringComparison.InvariantCultureIgnoreCase);
            this.UpdatePath = Application.StartupPath; //+ "\\" + System.Configuration.ConfigurationManager.AppSettings["SubSystem"];
            this.PublishAddress = System.Configuration.ConfigurationManager.AppSettings["PublishAddress"] ?? "";
            this.SubSystem = System.Configuration.ConfigurationManager.AppSettings["SubSystem"] ?? "";
            if (this.AllowPublish)
                nancySelfHost = new Nancy.Hosting.Self.NancyHost(new Uri("http://localhost:54321"), new MyBootstrapper());
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
            //Task publishTask = new Task(() => Publish(cts.Token), cts.Token);
            //publishTask.Start();
            if (this.AllowPublish)
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
            if (this.AllowPublish)
                nancySelfHost.Stop();
        }


        /// <summary>
        /// 处理引擎
        /// </summary>
        private void Publish(System.Threading.CancellationToken ct)
        {

            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
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
                    AppPublish appPublish = new AppPublish(this.SubSystem, this.UpdatePath, this.PublishAddress);
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
                    System.Threading.Thread.Sleep(this.Interval);
                }
                catch (Exception e)
                {
                    //log
                }
            }


        }
    }

    public class MyBootstrapper : Nancy.DefaultNancyBootstrapper
    {

        protected override void ConfigureConventions(Nancy.Conventions.NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);
            nancyConventions.StaticContentsConventions.Add(Nancy.Conventions.StaticContentConventionBuilder.AddDirectory("package"));
        }
    }
}
