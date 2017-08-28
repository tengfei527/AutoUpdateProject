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
        /// 编号
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 标识 1=项目 0
        /// </summary>
        public int Flag { get; set; }
        /// <summary>
        /// 监控地址
        /// </summary>
        public string IP { get; set; }
        
        public string Version { get; set; }

        public string Name { get; set; }
    }
}
