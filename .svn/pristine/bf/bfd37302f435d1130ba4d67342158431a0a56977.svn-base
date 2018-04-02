using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Model
{
    /// <summary>
    /// 版本库
    /// </summary>
    public class VerRepository : AggregateRoot
    {
        /// <summary>
        /// 发布编号
        /// </summary>
        public string No { get; set; }
        /// <summary>
        /// 发布类别 对应子系统
        /// </summary>
        public int PublishType { get; set; }
        /// <summary>
        /// 描述信息
        /// </summary>
        public string Descripthion { get; set; }
        /// <summary>
        /// 版本类别 0=标准 1=非标
        /// </summary>
        public string VerType { get; set; }
        /// <summary>
        /// 包路径
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 下载路径
        /// </summary>
        public string DownPath { get; set; }
        /// <summary>
        /// 更新类型 0=正常
        /// </summary>
        public int UpdateType { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime? LastUpdateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// SHA256校验值
        /// </summary>
        public string SHA256 { get; set; }
        /// <summary>
        /// 包清单
        /// </summary>
        public string AuPackage { get; set; }
    }
}
