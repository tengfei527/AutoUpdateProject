using System;
/// <summary>
/// 文件功能描述：涉及到文件管理的指令－磁盘结构类。
/// </summary>
namespace AU.Common.Codes
{
    /// <summary>
    /// 磁盘结构(作为序列化指令在网络上传输)
    /// </summary>
    [Serializable]
    public class DiskStruct : FileStruct
    {
        /// <summary>
        /// 磁盘标志
        /// </summary>
        public override FileFlag Flag
        {
            get { return FileFlag.Disk; }
        }
        public DiskStruct(string name) : base(name) { }
    }
}
