using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AU.Monitor.Server
{
    /// <summary>
    /// 中转指令
    /// </summary>
    public class TransferPackage
    {
        /// <summary>
        /// 指令
        /// </summary>
        public string Cmd { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 路由 >
        /// </summary>
        public string[] Route { get; set; }
        /// <summary>
        /// 路由节点
        /// </summary>
        public int RouteIndex { get; set; }
        /// <summary>
        /// 附加信息
        /// </summary>
        public string Attachment { get; set; }
    }
}
