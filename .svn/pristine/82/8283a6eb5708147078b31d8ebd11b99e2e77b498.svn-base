using System;
using System.Collections.Generic;
using System.Text;

namespace AU.Common
{
    public class AuList
    {
        public AuList()
        {
            this.Files = new List<AuFile>();
            this.Application = new AuApplication();
        }
        /// <summary>
        /// 升级号
        /// </summary>
        public string No { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        ///// <summary>
        ///// 更新URL地址
        ///// </summary>
        //public string Url { get; set; }
        /// <summary>
        /// 发布类型 0=标准 1=非标
        /// </summary>
        public int PublishType { get; set; }
        /// <summary>
        /// 更新类型 -1=允许跳过此版本 0=正常更新 1=强制更新
        /// </summary>
        public int UpdateType { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }
        /// <summary>
        /// 主应用程序
        /// </summary>
        public AuApplication Application { get; set; }
        /// <summary>
        /// 更新文件列表
        /// </summary>
        public List<AuFile> Files { get; set; }
    }


}
