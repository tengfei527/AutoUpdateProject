using System;
/// <summary>
/// 文件功能描述：涉及到文件管理的指令－基本文件结构类
/// </summary>
namespace AU.Common.Codes
{
    /// <summary>
    /// 标志(文件或文件夹)
    /// </summary>
    public enum FileFlag
    {
        /// <summary>
        /// 文件
        /// </summary>
        File,
        /// <summary>
        /// 文件夹
        /// </summary>
        Directory,
        /// <summary>
        /// 磁盘
        /// </summary>
        Disk,
    }
    /// <summary>
    /// 文件基类结构
    /// </summary>
    [Serializable]
    public abstract class BaseFile
    {
        /// <summary>
        /// 标志(文件,文件夹,磁盘)
        /// </summary>
        public abstract FileFlag Flag
        {
            get;
        }
        /// <summary>
        /// 全名
        /// </summary>
        public abstract string Name
        {
            get;
        }
    }
}
