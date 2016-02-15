using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using Alan.Log.Core;

namespace Alan.Log.Implement
{
    /// <summary>
    /// 发送E-mail日志
    /// 网易邮箱 SSL: false, Port: 25 测试通过
    /// QQ邮箱 SSL: true, Port: 587 测试通过
    /// </summary>
    public class LogEmail : ILog
    {
        /// <summary>
        /// Sender UserName
        /// </summary>
        private string _userName;
        /// <summary>
        /// Sender Password
        /// </summary>
        private string _passWord;
        /// <summary>
        /// SMTP Server Address
        /// </summary>
        private string _smtpServer;
        /// <summary>
        /// SMTP Server Port
        /// </summary>
        private int _smtpPort;

        /// <summary>
        /// SMTP enabled SSL
        /// </summary>
        private bool _enableSsl;
        /// <summary>
        /// Sender e-mail address
        /// </summary>
        private string _sender;
        /// <summary>
        /// Receiver e-mail address
        /// </summary>
        private string _receiver;

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="userName">sender用户名</param>
        /// <param name="passWord">sender密码</param>
        /// <param name="smtpServer">SMTP Server Address</param>
        /// <param name="receiver">接收人邮箱地址</param>
        public LogEmail(string userName, string passWord, string receiver, string smtpServer)
            : this(userName, passWord, smtpServer, receiver, 587, true, userName)
        {

        }
        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="userName">sender用户名</param>
        /// <param name="passWord">sender密码</param>
        /// <param name="smtpServer">SMTP Server Address</param>
        /// <param name="port">SMTP Port</param>
        /// <param name="receiver">接收人邮箱地址</param>
        public LogEmail(string userName, string passWord,
            string receiver, string smtpServer, int port)
            : this(userName, passWord, receiver, smtpServer, port, true, userName)
        {
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="userName">sender用户名</param>
        /// <param name="passWord">sender密码</param>
        /// <param name="smtpServer">SMTP Server Address</param>
        /// <param name="port">SMTP Port</param>
        /// <param name="enableSsl">SMTP是否开启SSL</param>
        /// <param name="receiver">接收人邮箱地址</param>
        public LogEmail(string userName, string passWord,
            string receiver, string smtpServer, int port, bool enableSsl)
            : this(userName, passWord, receiver, smtpServer, port, enableSsl, userName)
        {
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="userName">sender用户名</param>
        /// <param name="passWord">sender密码</param>
        /// <param name="smtpServer">SMTP Server Address</param>
        /// <param name="port">SMTP Port</param>
        /// <param name="enableSsl">SMTP是否开启SSL</param>
        /// <param name="sender">发送人邮箱地址</param>
        /// <param name="receiver">接收人邮箱地址</param>
        public LogEmail(string userName, string passWord,
            string receiver, string smtpServer, int port, bool enableSsl, string sender)
        {
            this._userName = userName;
            this._passWord = passWord;
            this._smtpServer = smtpServer;
            this._smtpPort = port;
            this._enableSsl = enableSsl;
            this._sender = sender;
            this._receiver = receiver;
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="date">日期</param>
        /// <param name="level">级别</param>
        /// <param name="logger">标识者</param>
        /// <param name="category">分类(比如 注册/订单/支付/添加好友)</param>
        /// <param name="message">消息</param>
        /// <param name="note">备注</param>
        /// <param name="request">请求内容</param>
        /// <param name="response">响应内容</param>
        /// <param name="position">输出位置</param>
        public void Write(string id, DateTime date, string level, string logger, string category, string message, string note,
            string request, string response, string position)
        {

            NetworkCredential credential = new NetworkCredential(this._userName, this._passWord);
            SmtpClient smtp = new SmtpClient()
            {
                EnableSsl = this._enableSsl,
                Port = this._smtpPort,
                Host = this._smtpServer,
                UseDefaultCredentials = true,
                Credentials = credential
            };

            var logs = new List<string>();
            logs.Add(Environment.NewLine);
            logs.Add(String.Format("{0} Id: {1}, Date: {2}, Level: {3} {0}", String.Join("", Enumerable.Repeat("=", 10)), id, date.ToString("yyyy-MM-dd HH:mm:ss"), level));
            if (!String.IsNullOrWhiteSpace(logger)) logs.Add(String.Format("Logger: {0}", logger));
            if (!String.IsNullOrWhiteSpace(category)) logs.Add(String.Format("Category: {0}", category));

            if (!String.IsNullOrWhiteSpace(message)) logs.Add(String.Format("Message: {0}", message));
            if (!String.IsNullOrWhiteSpace(note)) logs.Add(String.Format("Note: {0}", note));
            if (!String.IsNullOrWhiteSpace(request)) logs.Add(String.Format("Request: {0}", request));
            if (!String.IsNullOrWhiteSpace(response)) logs.Add(String.Format("Response: {0}", response));
            if (!String.IsNullOrWhiteSpace(position)) logs.Add(String.Format("Position: {0}", position));
            logs.Add(String.Format("{0} End {1} {0}", String.Join("", Enumerable.Repeat("=", 10)), id));
            logs.Add(Environment.NewLine);


            var mail = new MailMessage(new MailAddress(this._sender, "Log Sender"), new MailAddress(this._receiver));

            mail.Subject = String.Format("Id: {0} Date: {1} Level: {2}", id, date.ToString("yyyy-MM-dd HH:mm:ss"), level);
            mail.SubjectEncoding = System.Text.Encoding.UTF8;

            mail.Body = String.Join(Environment.NewLine, logs);
            mail.BodyEncoding = System.Text.Encoding.UTF8;

            smtp.Send(mail);
        }
    }
}
