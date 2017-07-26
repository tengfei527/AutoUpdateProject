using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AuClient
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public sealed class AppConfig
    {
        /// <summary>
        /// 私有对象
        /// </summary>
        private static AppConfig appConfig = new AppConfig();
        /// <summary>
        /// 当前配置
        /// </summary>
        public static AppConfig Current
        {
            get { return appConfig; }
        }
        /// <summary>
        /// 检测更新频率
        /// </summary>
        public int Interval { get; set; }
        /// <summary>
        /// 是否允许发布
        /// </summary>
        public bool AllowPublish { get; set; }
        /// <summary>
        /// 是否显示UI
        /// </summary>
        public bool AllowUI { get; set; }
        /// <summary>
        /// 发布端口
        /// </summary>
        public int PublishPort { get; set; }
        /// <summary>
        /// 发布地址
        /// </summary>
        public string PublishAddress { get; set; }
        /// <summary>
        /// 更新配置路径
        /// </summary>
        public string UpdateConfigPath { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string LinkUrl { get; set; }
        /// <summary>
        /// Socket服务器地址
        /// </summary>
        public string SocketServer { get; set; }
        /// <summary>
        ///初始化系统参数 
        /// </summary>
        private AppConfig()
        {
            string temp = System.Configuration.ConfigurationManager.AppSettings["AllowPublish"];
            try
            {
                this.Interval = string.IsNullOrWhiteSpace(temp) ? 5000 : Convert.ToInt32(temp);
            }
            catch
            {
                this.Interval = 5000;
            }

            this.AllowPublish = "true".Equals(System.Configuration.ConfigurationManager.AppSettings["AllowPublish"], StringComparison.InvariantCultureIgnoreCase);

            this.AllowUI = "true".Equals(System.Configuration.ConfigurationManager.AppSettings["AllowUI"], StringComparison.InvariantCultureIgnoreCase);
            //PublishAddress
            try
            {
                this.PublishPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PublishPort"]);
            }
            catch
            {
                this.PublishPort = 54321;
            }
            try
            {
                this.PublishAddress = string.Format("http://{0}:{1}/", AU.Common.Utility.IpHelp.GetLocalIP(), this.PublishPort);
            }
            catch
            {
                this.PublishAddress = string.Format("http://{0}:{1}/", "0.0.0.0", this.PublishPort);
            }
            this.UpdateConfigPath = Application.StartupPath;
            this.LinkUrl = System.Configuration.ConfigurationManager.AppSettings["LinkUrl"] ?? "";
            try
            {
                this.SocketServer = System.Configuration.ConfigurationManager.AppSettings
                    ["SocketServer"] ?? "";
                var ips = this.SocketServer.Split(':');
                System.Net.IPAddress.Parse(ips[0]);
                int port = Convert.ToInt32(ips[1]);
            }
            catch (Exception e)
            {
                MessageBox.Show("服务器地址配置错误，详情：" + e.Message);
            }
        }

        public static string GetUpdateTempPath(string subsystem)
        {
            return appConfig.UpdateConfigPath + "\\autemp\\" + subsystem;
        }
        public static string GetAuBackupPath(string subsystem)
        {
            return appConfig.UpdateConfigPath + "\\aubackup\\" + subsystem + "\\backup\\";
        }


        //+ "\\" + System.Configuration.ConfigurationManager.AppSettings["SubSystem"];

    }
}
