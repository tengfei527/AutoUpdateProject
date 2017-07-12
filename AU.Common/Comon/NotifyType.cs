using System;
using System.Collections.Generic;
using System.Text;

namespace AU.Common
{
    /// <summary>
    /// 通知类别
    /// </summary>
    public enum NotifyType
    {
        /// <summary>
        /// 异常
        /// </summary>
        Error = -1,
        /// <summary>
        /// 正常通知
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 运行中
        /// </summary>
        Running = 1,
        /// <summary>
        /// 停止
        /// </summary>
        Stop = 2,

        /// <summary>
        /// 开始下载
        /// </summary>
        StartDown = 3,
        /// <summary>
        /// 进度
        /// </summary>
        Process = 4,
        /// <summary>
        /// 更新
        /// </summary>
        UpProcess=5,
        /// <summary>
        /// 停止更新
        /// </summary>
        StopDown = 6,


        /// <summary>
        /// 执行成功
        /// </summary>
        RunSucess = 10,
        /// <summary>
        /// 执行错误
        /// </summary>
        RunError = 20,
        /// <summary>
        /// 其他
        /// </summary>
        Other = 30,
    }
}
