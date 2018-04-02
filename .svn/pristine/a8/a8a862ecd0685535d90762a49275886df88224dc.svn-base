using System;
using System.Collections.Generic;
using System.Text;

namespace AU.Common
{
    /// <summary>
    /// 消息内容
    /// </summary>
    public class NotifyMessage : EventArgs
    {
        public NotifyMessage()
        {

        }
        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="notifyType"></param>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public NotifyMessage(NotifyType notifyType, string message, object e = null)
        {
            this.NotifyType = notifyType;
            this.Message = message;
            this.Attachment = e;
        }
        /// <summary>
        /// 消息类别：-1=操作失败 0=正常（默认） 1=操作成功  
        /// </summary>
        public NotifyType NotifyType { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 附件
        /// </summary>
        public object Attachment { get; set; }
    }

}
