using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Alan.Log.Core;

namespace Alan.Log.ILogImplement
{
    /// <summary>
    /// 单文件日志
    /// </summary>
    public class LogSingleFile : ILog
    {
        private static object _lock = new object();


        /// <summary>
        /// 是否在写日志的时候添加注释
        /// </summary>
        public bool AppendCommentName { get; set; }

        /// <summary>
        /// 日志文件路径
        /// </summary>
        public string LogFileFullPath { get; set; }

        /// <summary>
        /// 实例化日志模块
        /// </summary>
        public LogSingleFile()
        {
            this.LogFileFullPath = Path.Combine(Environment.CurrentDirectory, "LogSingleFileClear.txt");
            this.AppendCommentName = true;
        }

        /// <summary>
        /// 实例化日志模块
        /// </summary>
        /// <param name="logFileFullPath">日志绝对路径</param>
        public LogSingleFile(string logFileFullPath)
        {
            this.LogFileFullPath = logFileFullPath;
        }


        /// <summary>
        /// 实例化日志模块
        /// </summary>
        /// <param name="logFileFullPath">日志绝对路径</param>
        /// <param name="appenComment">是否添加注释前缀</param>
        public LogSingleFile(string logFileFullPath, bool appenComment)
        {
            this.LogFileFullPath = logFileFullPath;
            this.AppendCommentName = appenComment;
        }
        public LogSingleFile Config(string fullPath)
        {
            this.LogFileFullPath = fullPath;
            return this;
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
            var lines = new List<string>();

            if (this.AppendCommentName)
            {
                if (!String.IsNullOrWhiteSpace(id)) lines.Add(String.Format("{0,-10}: {1}", "Id", id));
                if (date != default(DateTime)) lines.Add(String.Format("{0,-10}: {1}", "Date", date.ToString("yyyy-MM-dd HH:mm:ss")));
                if (!String.IsNullOrWhiteSpace(level)) lines.Add(String.Format("{0,-10}: {1}", "Level", level));
                if (!String.IsNullOrWhiteSpace(logger)) lines.Add(String.Format("{0,-10}: {1}", "Logger", logger));
                if (!String.IsNullOrWhiteSpace(category)) lines.Add(String.Format("{0,-10}: {1}", "Category", category));
                if (!String.IsNullOrWhiteSpace(message)) lines.Add(String.Format("{0,-10}: {1}", "Message", message));
                if (!String.IsNullOrWhiteSpace(note)) lines.Add(String.Format("{0,-10}: {1}", "Note", note));
                if (!String.IsNullOrWhiteSpace(request)) lines.Add(String.Format("{0,-10}: {1}", "Request", request));
                if (!String.IsNullOrWhiteSpace(response)) lines.Add(String.Format("{0,-10}: {1}", "Response", response));
                if (!String.IsNullOrWhiteSpace(position)) lines.Add(String.Format("{0,-10}: {1}", "Position", position));
            }
            else
            {
                if (!String.IsNullOrWhiteSpace(id)) lines.Add(id);
                if (date != default(DateTime)) lines.Add(date.ToString("yyyy-MM-dd HH:mm:ss"));
                if (!String.IsNullOrWhiteSpace(level)) lines.Add(level);
                if (!String.IsNullOrWhiteSpace(logger)) lines.Add(logger);
                if (!String.IsNullOrWhiteSpace(category)) lines.Add(category);
                if (!String.IsNullOrWhiteSpace(message)) lines.Add(message);
                if (!String.IsNullOrWhiteSpace(note)) lines.Add(note);
                if (!String.IsNullOrWhiteSpace(request)) lines.Add(request);
                if (!String.IsNullOrWhiteSpace(response)) lines.Add(response);
                if (!String.IsNullOrWhiteSpace(position)) lines.Add(position);
            }

            var text = String.Join(Environment.NewLine, lines) + Environment.NewLine;

            var dir = System.IO.Path.GetDirectoryName(this.LogFileFullPath);

            lock (_lock)
            {
                if (!System.IO.Directory.Exists(dir)) System.IO.Directory.CreateDirectory(dir);

                System.IO.File.AppendAllText(this.LogFileFullPath, text, System.Text.Encoding.UTF8);
            }
        }
    }
}
