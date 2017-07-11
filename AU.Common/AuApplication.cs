using System;
using System.Collections.Generic;
using System.Text;

namespace AU.Common
{
    /// <summary>
    /// 主应用详情
    /// </summary>
    public class AuApplication
    {
        /// <summary>
        /// 更新完成启动方式： 0=不启动 1=启动
        /// </summary>
        public int StartType { get; set; }
        /// <summary>
        /// 启动参数
        /// </summary>
        public string StartArgs { get; set; }
        /// <summary>
        /// 关闭类型 0=进城关闭 1=参数关闭
        /// </summary>
        public int CloseType { get; set; }
        /// <summary>
        /// 关闭参数
        /// </summary>
        public string CloseArgs { get; set; }
        /// <summary>
        /// 应用编号
        /// </summary>
        public string ApplicationId { get; set; }
        /// <summary>
        /// 入口点
        /// </summary>
        public string EntryPoint { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 主版本
        /// </summary>
        public string Version { get; set; }
    }
}
