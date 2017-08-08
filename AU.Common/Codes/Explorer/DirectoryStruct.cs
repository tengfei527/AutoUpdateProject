using System;
/// <summary>
/// 文件功能描述：涉及到文件管理的指令－文件夹结构类。
/// </summary>
namespace AU.Common.Codes
{
    /// <summary>
    /// 文件夹结构(作为序列化指令在网络上传输)
    /// </summary>
    [Serializable]
    public class DirectoryStruct : FileStruct
    {
        /// <summary>
        /// 文件夹标志
        /// </summary>
        public override FileFlag Flag
        {
            get { return FileFlag.Directory; }
        }
        public DirectoryStruct(string name) : base(name) { }

        public override string Size
        {
            get
            {
                return "";
            }
        }
    }
}
