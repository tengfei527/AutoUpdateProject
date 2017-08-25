using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    /// <summary>
    /// 项目表
    /// </summary>
    public class Project : AggregateRoot
    {
        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectNo { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 区域编号
        /// </summary>
        public string ZoneId { get; set; }
        /// <summary>
        /// 项目类别
        /// </summary>
        public int ProjectType { get; set; }
        /// <summary>
        /// 默认网关
        /// </summary>
        public string GateWay { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// X坐标
        /// </summary>
        public decimal PositionX { get; set; }
        /// <summary>
        /// Y坐标
        /// </summary>
        public decimal PositionY { get; set; }
        /// <summary>
        /// 标识 0=标准 1=非标
        /// </summary>
        public int Flag { get; set; }
        /// <summary>
        /// 设定版本
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 升级状态
        /// </summary>
        public int UpStatus { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastUpdate { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string Gid { get; set; }
        /// <summary>
        /// 记录标识
        /// </summary>
        public string Rid { get; set; }
    }
}
