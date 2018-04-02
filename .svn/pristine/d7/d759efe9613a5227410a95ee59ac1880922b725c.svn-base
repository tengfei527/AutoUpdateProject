using System;
/// <summary>
/// 文件功能描述：获取主机信息的指令。
/// </summary>
namespace AU.Common.Codes
{
    /// <summary>
    /// 主机信息结构
    /// </summary>
    [Serializable]
    public class HostCode : BaseCode
    {
        private string ip;
        private string name;

        /// <summary>
        /// 主机的IP地址
        /// </summary>
        public string IP
        {
            get { return ip; }
            set { ip = value; }
        }

        /// <summary>
        /// 主机名
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// 重载ToString这个方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return IP + "(" + Name + ")";
        }
    }
}
