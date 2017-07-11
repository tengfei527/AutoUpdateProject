using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuClient
{
    /// <summary>
    /// 升级消息
    /// </summary>
    public class UpgradeMessage
    {
        /// <summary>
        /// 子系统
        /// </summary>
        public string SubSystem { get; set; }
        /// <summary>
        /// 升级包
        /// </summary>
        public string UpdatePackFile { get; set; }
        /// <summary>
        /// 升级系统路径
        /// </summary>
        public string UpgradePath { get; set; }
    }
}
