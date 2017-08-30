using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Model
{
    /// <summary>
    /// 项目版本设定
    /// </summary>
    public class ProjectVerSet : AggregateRoot
    {
        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectNo { get; set; }
        /// <summary>
        /// 发布类别 对应子系统
        /// </summary>
        public int PublishType { get; set; }
        /// <summary>
        ///设定版本
        /// </summary>
        public string SetVerNo { get; set; }
        /// <summary>
        /// 当前版本
        /// </summary>
        public string NowVerNo { get; set; }       
        /// <summary>
        /// 是否有效 1=有效 0=无效
        /// </summary>
        public int Flag { get; set; }
        /// <summary>
        /// 创建时间时间
        /// </summary>
        public DateTime? LastSetTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime? LastUpdateTime { get; set; } = DateTime.Now.Date;
    }
}
