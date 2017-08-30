using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Model
{
    /// <summary>
    /// 项目状态
    /// </summary>
    public class MonitorStatus : AggregateRoot
    {
        /// <summary>
        /// 监控终端Session
        /// </summary>
        public string MonitorId { get; set; }
        /// <summary>
        /// 终端名称
        /// </summary>
        public string MonitorName { get; set; }
        /// <summary>
        /// 位置 IP:PORT
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 当前状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 状态详细标识
        /// </summary>
        public int Flag { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 监控类型
        /// </summary>
        public int MonitorType { get; set; }
        /// <summary>
        /// 监控类别名称
        /// </summary>
        public string MonitorTypeName { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime? LastUpdateTime { get; set; } = DateTime.Now.Date;
        /// <summary>
        /// 监控终端版本号
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 对应项目编号
        /// </summary>
        public string ProjectNo { get; set; }
    }
}
