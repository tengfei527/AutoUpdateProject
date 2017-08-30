using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Model
{
    public class Operator : AggregateRoot
    {
        /// <summary>
        /// 登陆名称
        /// </summary>
        public string LoginId { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string OperName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }

    }
}
