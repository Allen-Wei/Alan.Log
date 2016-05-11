using Alan.Log.Core;
using Alan.Log.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alan.Log.Models.Ex;

namespace Alan.Log.ILogImplement
{
    /// <summary>
    /// 根据日期自动分割文件
    /// </summary>
    public class LogAutoSeperateFilesByDate : ILog
    {
        private static object _lock = new object();
        /// <summary>
        /// 日志存放目录
        /// </summary>
        private string Directory { get; set; }
        /// <summary>
        /// 日志文件名前缀
        /// </summary>
        private string PrefixName { get; set; }
        /// <summary>
        /// 日志文件扩展名
        /// </summary>
        private string ExtName { get; set; }

        /// <summary>
        /// 序列化日志
        /// </summary>
        public Func<Alan.Log.Models.Log, string> Generate { get; set; }
        /// <summary>
        /// 在默认的生成日志内容方法里(Generate)是否将属性名写入
        /// </summary>
        public bool AppendPropertyNameInDefaultGenerate { get; set; }

        /// <summary>
        /// 实例化
        /// </summary>
        public LogAutoSeperateFilesByDate()
        {
            this.Directory = Environment.CurrentDirectory;
            this.PrefixName = "LogAutoSeperateFilesByDate";
            this.ExtName = ".txt";
            this.AppendPropertyNameInDefaultGenerate = true;
            this.Generate = l =>
            {
                List<string> lines = new List<string>();
                if (this.AppendPropertyNameInDefaultGenerate)
                {
                    if (!String.IsNullOrWhiteSpace(l.Id)) lines.Add(String.Format("{0,-10}: {1}", "Id", l.Id));
                    if (l.Date != default(DateTime)) lines.Add(String.Format("{0,-10}: {1}", "Date", l.Date.ToString("yyyy-MM-dd HH:mm:ss")));
                    if (l.Level != Models.Log.LogLevel.None) lines.Add(String.Format("{0,-10}: {1}", "Level", l.Level));
                    if (!String.IsNullOrWhiteSpace(l.Logger)) lines.Add(String.Format("{0,-10}: {1}", "Logger", l.Logger));
                    if (!String.IsNullOrWhiteSpace(l.Category)) lines.Add(String.Format("{0,-10}: {1}", "Category", l.Category));
                    if (!String.IsNullOrWhiteSpace(l.Message)) lines.Add(String.Format("{0,-10}: {1}", "Message", l.Message));
                    if (!String.IsNullOrWhiteSpace(l.Note)) lines.Add(String.Format("{0,-10}: {1}", "Note", l.Note));
                    if (!String.IsNullOrWhiteSpace(l.Request)) lines.Add(String.Format("{0,-10}: {1}", "Request", l.Request));
                    if (!String.IsNullOrWhiteSpace(l.Response)) lines.Add(String.Format("{0,-10}: {1}", "Response", l.Response));
                    if (!String.IsNullOrWhiteSpace(l.Position)) lines.Add(String.Format("{0,-10}: {1}", "Position", l.Position));
                }
                else
                {
                    if (!String.IsNullOrWhiteSpace(l.Id)) lines.Add(l.Id);
                    if (l.Date != default(DateTime)) lines.Add(l.Date.ToString("yyyy-MM-dd HH:mm:ss"));
                    if (l.Level != Models.Log.LogLevel.None) lines.Add(l.Level.ToString());
                    if (!String.IsNullOrWhiteSpace(l.Logger)) lines.Add(l.Logger);
                    if (!String.IsNullOrWhiteSpace(l.Category)) lines.Add(l.Category);
                    if (!String.IsNullOrWhiteSpace(l.Message)) lines.Add(l.Message);
                    if (!String.IsNullOrWhiteSpace(l.Note)) lines.Add(l.Note);
                    if (!String.IsNullOrWhiteSpace(l.Request)) lines.Add(l.Request);
                    if (!String.IsNullOrWhiteSpace(l.Response)) lines.Add(l.Response);
                    if (!String.IsNullOrWhiteSpace(l.Position)) lines.Add(l.Position);
                }

                return String.Join(Environment.NewLine, lines) + Environment.NewLine;
            };
        }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="fileFullPath">日志文件绝对路径, 从这个路径里获取日志目录, 日志名前缀, 日志扩展名. 比如 E:\path1\filename.ext, 目录为 E:\path1, 日志文件名前缀为 filename, 日志文件扩展名为.ext</param>
        public LogAutoSeperateFilesByDate(string fileFullPath) : this()
        {
            this.Directory = System.IO.Path.GetDirectoryName(fileFullPath);
            this.PrefixName = System.IO.Path.GetFileNameWithoutExtension(fileFullPath);
            this.ExtName = System.IO.Path.GetExtension(fileFullPath);
        }


        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="fileFullPath">日志文件绝对路径, 从这个路径里获取日志目录, 日志名前缀, 日志扩展名. 比如 E:\path1\filename.ext, 目录为 E:\path1, 日志文件名前缀为 filename, 日志文件扩展名为.ext</param>
        /// <param name="generate">序列化字符串</param>
        public LogAutoSeperateFilesByDate(string fileFullPath, Func<Alan.Log.Models.Log, string> generate) : this()
        {
            this.Directory = System.IO.Path.GetDirectoryName(fileFullPath);
            this.PrefixName = System.IO.Path.GetFileNameWithoutExtension(fileFullPath);
            this.ExtName = System.IO.Path.GetExtension(fileFullPath);
            this.Generate = generate;
        }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="directory">日志目录</param>
        /// <param name="fileName">日志文件名(包含扩展名)</param>
        public LogAutoSeperateFilesByDate(string directory, string fileName) : this()
        {
            this.PrefixName = System.IO.Path.GetFileNameWithoutExtension(fileName);
            this.ExtName = System.IO.Path.GetExtension(fileName);
            this.Directory = directory;
        }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="directory">日志目录</param>
        /// <param name="fileName">日志文件名(包含扩展名)</param>
        /// <param name="generate">序列化日志字符串</param>
        public LogAutoSeperateFilesByDate(string directory, string fileName, Func<Alan.Log.Models.Log, string> generate)
        {
            this.PrefixName = System.IO.Path.GetFileNameWithoutExtension(fileName);
            this.ExtName = System.IO.Path.GetExtension(fileName);
            this.Directory = directory;
            this.Generate = generate;
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
        public void Write(string id,
            DateTime date,
            string level,
            string logger,
            string category,
            string message,
            string note,
            string request,
            string response,
            string position)
        {

            var fileName = String.Format("{0}-{1}-{2}{3}", this.PrefixName, level ?? "none", DateTime.Now.ToString("yyyyMMdd"), this.ExtName);
            var filePath = System.IO.Path.Combine(this.Directory, fileName);

            var logTxt = this.Generate(new Alan.Log.Models.Log
            {
                Id = id,
                Level = level.ToLogLevel(),
                Logger = logger,
                Category = category,
                Date = date,
                Message = message,
                Note = note,
                Position = position,
                Request = request,
                Response = response
            });


            lock (_lock)
            {
                if (!System.IO.Directory.Exists(this.Directory)) System.IO.Directory.CreateDirectory(this.Directory);

                System.IO.File.AppendAllText(filePath, logTxt, Encoding.UTF8);
            }
        }
    }
}
