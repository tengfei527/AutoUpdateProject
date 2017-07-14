using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AU.Monitor.Server
{
    /// <summary>
    /// 命令包
    /// </summary>
    public class CommandPackage
    {
        public CommandPackage()
        {
            Create = DateTime.Now;
        }
        /// <summary>
        /// 类别
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public string[] Parameters { get; set; }
        /// <summary>
        /// 消息体
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Create { get; set; }
        /// <summary>
        /// 附加内容
        /// </summary>
        public string Attachment { get; set; }
        /// <summary>
        /// 路由 >
        /// </summary>
        public string Route { get; set; }
    }
}
