using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Alan.Log.Core;

namespace Alan.Log.ILogImplement
{
    public class LogSingleFile : ILog
    {
        private static object _lock = new object();
        private string _filePath;

        public string LogFileFullPath
        {
            get { return this._filePath; }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) throw new ArgumentNullException("LogFileFullPath");
                if (!File.Exists(value))
                {
                    lock (_lock)
                    {
                        using (var fs = File.Create(value)) fs.Close();
                    }
                }

                this._filePath = value;
            }
        }

        public LogSingleFile()
        {
            this.LogFileFullPath = Path.Combine(Environment.CurrentDirectory, "LogSingleFile.txt");
        }

        public LogSingleFile(string logFileFullPath)
        {
            this.Config(logFileFullPath);
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="logFileFullPath">日志文件路径</param>
        /// <returns></returns>
        public LogSingleFile Config(string logFileFullPath)
        {

            if (String.IsNullOrWhiteSpace(logFileFullPath)) throw new ArgumentNullException("logFileFullPath");

            this.LogFileFullPath = logFileFullPath;
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
            var logs = new List<string>();
            logs.Add(Environment.NewLine);
            logs.Add(String.Format("{0} Id: {1}, Date: {2}, Level: {3} {0}",
                String.Join("", Enumerable.Repeat("=", 10)), id, date.ToString("yyyy-MM-dd HH:mm:ss"), level));
            logs.Add(String.Format("Logger: {0} Category: {1}", logger, category));

            if (!String.IsNullOrWhiteSpace(message)) logs.Add(String.Format("Message: {0}", message));
            if (!String.IsNullOrWhiteSpace(note)) logs.Add(String.Format("Note: {0}", note));
            if (!String.IsNullOrWhiteSpace(request)) logs.Add(String.Format("Request: {0}", request));
            if (!String.IsNullOrWhiteSpace(response)) logs.Add(String.Format("Response: {0}", response));
            if (!String.IsNullOrWhiteSpace(position)) logs.Add(String.Format("{0} Position: {1} {0}", String.Join("", Enumerable.Repeat("=", 10)), position));

            logs.Add(Environment.NewLine);

            lock (_lock)
            {
                File.AppendAllLines(this.LogFileFullPath, logs, Encoding.UTF8);
            }
        }
    }
}
