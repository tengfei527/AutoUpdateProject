using System;
using System.Collections.Generic;
using System.Text;

namespace AU.Common
{
    public class CommandType
    {
        /// <summary>
        /// 服务器更新通知
        /// </summary>
        public static readonly string AUVERSION = "AUVERSION";

        /// <summary>
        /// 中转命令
        /// </summary>
        public static readonly string TRANSFER = "TRANSFER";

        /// <summary>
        /// 指定中转命令
        /// </summary>
        public static readonly string TRANSFERONE = "TRANSFERONE";

        /// <summary>
        /// 终端指令
        /// </summary>
        public static readonly string TERMINAL = "TERMINAL";

        /// <summary>
        /// 资源指令
        /// </summary>
        public static readonly string RESOURCE = "RESOURCE";

        /// <summary>
        /// 脚本指令
        /// </summary>
        public static readonly string SCRIPT = "SCRIPT";
    }
}
