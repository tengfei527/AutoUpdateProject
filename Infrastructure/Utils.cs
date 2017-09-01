﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Infrastructure
{
    /// <summary>
    /// 表示用于整个Byteart Retail系统的工具类。
    /// </summary>
    public static class Utils
    {
        #region Private Fields
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("Au.Logger");
        #endregion

        #region Public Static Methods
        /// <summary>
        /// 将指定的字符串信息写入日志。
        /// </summary>
        /// <param name="message">需要写入日志的字符串信息。</param>
        public static void Log(string message)
        {
            log.Info(message);
        }
        /// <summary>
        /// 将指定的<see cref="Exception"/>实例详细信息写入日志。
        /// </summary>
        /// <param name="ex">需要将详细信息写入日志的<see cref="Exception"/>实例。</param>
        public static void Log(Exception ex)
        {
            log.Error("Exception caught", ex);
        }
        /// <summary>
        /// 向指定的邮件地址发送邮件。
        /// </summary>
        /// <param name="sender">发送邮件者</param>
        /// <param name="to">需要发送邮件的邮件地址。</param>
        /// <param name="subject">邮件主题。</param>
        /// <param name="content">邮件内容。</param>
        public static void SendEmail(string sender, string to, string subject,string host,int port,string userName, string content,string password, bool enableSsl)
        {
            MailMessage msg = new MailMessage(sender,
                to,
                subject,
                content);
            SmtpClient smtpClient = new SmtpClient(host);
            smtpClient.Port = port;
            smtpClient.Credentials = new NetworkCredential(userName, password);
            smtpClient.EnableSsl = enableSsl;
            smtpClient.Send(msg);
        }
        #endregion
    }
}