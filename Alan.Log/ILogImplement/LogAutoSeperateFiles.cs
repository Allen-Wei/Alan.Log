using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Alan.Log.Core;

namespace Alan.Log.ILogImplement
{
    /// <summary>
    /// 根据文件大小自动将日志记录到不同的文件里
    /// </summary>
    public class LogAutoSeperateFiles : ILog
    {
        private static object _lock = new object();

        /// <summary>
        /// 文件最大尺寸
        /// </summary>
        private int _fileMaxSizeBytes;

        /// <summary>
        /// 文件目录
        /// </summary>
        private string _fileDirectory;
        /// <summary>
        /// 文件名前缀
        /// </summary>
        private string _fileNamePrefix;

        /// <summary>
        /// 日志文件后缀名
        /// </summary>
        private string _fileExtentionName;

        /// <summary>
        /// 获取文件绝对路径
        /// (directory, fileNamePrefix, maxSize) 
        /// </summary>
        private Func<string, string, int, string> _getFileFullPath;

        /// <summary>
        /// 实例化 默认大小 100*1024, 默认执行环境目录, 文件名LogAutoSeperateFiles
        /// </summary>
        public LogAutoSeperateFiles()
        {
            this.Config(100 * 1024, Environment.CurrentDirectory, "LogAutoSeperateFiles", "txt");
        }

        /// <summary>
        /// 自动将日志记录到不同的文件里
        /// </summary>
        /// <param name="fileMaxSizeBytes">单个文件最大尺寸</param>
        /// <param name="fileFullPath">日志文件绝对路径(比如 E:\SitePath\LogName.log, 目录是 E:\SitePath, 日志名前缀 LogName, 日志扩展名 log)</param>
        public LogAutoSeperateFiles(int fileMaxSizeBytes, string fileFullPath)
        {
            if (String.IsNullOrWhiteSpace(fileFullPath)) throw new ArgumentNullException("fileFullPath");

            var directory = Path.GetDirectoryName(fileFullPath);
            var prefixName = Path.GetFileNameWithoutExtension(fileFullPath);
            var extName = (Path.GetExtension(fileFullPath) ?? "").Replace(".", "");

            this.Config(fileMaxSizeBytes, directory, prefixName, extName);
        }


        /// <summary>
        /// 自动将日志记录到不同的文件里
        /// </summary>
        /// <param name="fileMaxSizeBytes">单个文件最大尺寸</param>
        /// <param name="fileDirectoryPath">文件所在目录</param>
        /// <param name="fileNamePrefix">文件名前缀</param>
        public LogAutoSeperateFiles(int fileMaxSizeBytes, string fileDirectoryPath, string fileNamePrefix)
        {
            this.Config(fileMaxSizeBytes, fileDirectoryPath, fileNamePrefix, "txt");
        }

        /// <summary>
        /// 自动将日志记录到不同的文件里
        /// </summary>
        /// <param name="fileMaxSizeBytes">单个文件最大尺寸</param>
        /// <param name="fileDirectoryPath">文件所在目录</param>
        /// <param name="fileNamePrefix">文件名前缀</param>
        /// <param name="getFileFullPath">(directory, fileNamePrefix, maxSize) => fileFullpath</param>
        public LogAutoSeperateFiles(int fileMaxSizeBytes, string fileDirectoryPath, string fileNamePrefix, Func<string, string, int, string> getFileFullPath)
        {
            this.Config(fileMaxSizeBytes, fileDirectoryPath, fileNamePrefix, "txt", getFileFullPath);
        }


        /// <summary>
        /// 配置
        /// </summary> 
        /// <param name="fileMaxSizeBytes">单个文件最大尺寸</param>
        /// <param name="fileDirectoryPath">文件所在目录</param>
        /// <param name="fileNamePrefix">文件名前缀</param>
        /// <param name="fileExtName">日志文件后缀名</param>
        /// <returns></returns>
        public LogAutoSeperateFiles Config(int fileMaxSizeBytes, string fileDirectoryPath, string fileNamePrefix, string fileExtName)
        {
            if (fileMaxSizeBytes < 1) throw new ArgumentOutOfRangeException(message: "fileMaxSizeBytes 文件尺寸不能小于1", innerException: null);
            if (String.IsNullOrWhiteSpace(fileDirectoryPath)) throw new ArgumentNullException("fileDirectoryPath");
            if (String.IsNullOrWhiteSpace(fileNamePrefix)) throw new ArgumentNullException("fileNamePrefix");

            this._fileMaxSizeBytes = fileMaxSizeBytes;
            this._fileDirectory = fileDirectoryPath;
            this._fileNamePrefix = fileNamePrefix;
            this._fileExtentionName = fileExtName;


            this._getFileFullPath = (direc, fnPrefix, maxSize) =>
            {
                var directory = new DirectoryInfo(direc);
                var files = directory.GetFiles(String.Format("{0}*.{1}", fnPrefix, this._fileExtentionName)).OrderByDescending(f => f.CreationTime);

                var number = 0;
                var firstFile = files.FirstOrDefault();

                if (firstFile != null)
                {
                    if (firstFile.Length < maxSize) return firstFile.FullName;

                    var firstFileName = firstFile.Name;
                    number = int.Parse(firstFileName.Split('.')[0].Split('-').Last());
                    ++number;
                }

                var fileFullPath = Path.Combine(direc, String.Format("{0}-{1}-{2}.{3}", fnPrefix, DateTime.Now.ToString("yyyyMMdd"), number, this._fileExtentionName));


                return fileFullPath;
            };

            return this;
        }


        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="fileMaxSizeBytes">单个文件最大尺寸</param>
        /// <param name="fileDirectoryPath">文件所在目录</param>
        /// <param name="fileNamePrefix">文件名前缀</param>
        /// <param name="extName">日志文件扩展名</param>
        /// <param name="getFileFullPath">(directory, fileNamePrefix, maxSize) => fileFullpath</param>
        /// <returns></returns>
        public LogAutoSeperateFiles Config(int fileMaxSizeBytes, string fileDirectoryPath, string fileNamePrefix, string extName, Func<string, string, int, string> getFileFullPath)
        {
            this.Config(fileMaxSizeBytes, fileDirectoryPath, fileNamePrefix, extName);

            if (getFileFullPath == null) throw new ArgumentNullException("getFileFullPath");

            this._getFileFullPath = getFileFullPath;

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
            logs.Add(String.Format("{0} Id: {1}, Date: {2}, Level: {3} {0}", String.Join("", Enumerable.Repeat("=", 10)), id, date.ToString("yyyy-MM-dd HH:mm:ss"), level));
            logs.Add(String.Format("Logger: {0} Category: {1}", logger, category));

            if (!String.IsNullOrWhiteSpace(message)) logs.Add(String.Format("Message: {0}", message));
            if (!String.IsNullOrWhiteSpace(note)) logs.Add(String.Format("Note: {0}", note));
            if (!String.IsNullOrWhiteSpace(request)) logs.Add(String.Format("Request: {0}", request));
            if (!String.IsNullOrWhiteSpace(response)) logs.Add(String.Format("Response: {0}", response));
            if (!String.IsNullOrWhiteSpace(position)) logs.Add(String.Format("{0} Position: {1} {0}", String.Join("", Enumerable.Repeat("=", 10)), position));
            logs.Add(Environment.NewLine);

            var fileFullPath = this._getFileFullPath(this._fileDirectory, this._fileNamePrefix, this._fileMaxSizeBytes);

            lock (_lock)
            {
                if (!File.Exists(fileFullPath))
                {
                    using (var fs = File.Create(fileFullPath))
                    {
                        var txt = Encoding.UTF8.GetBytes(String.Join(Environment.NewLine, logs));
                        fs.Write(txt, 0, txt.Length);
                        fs.Close();
                    }
                }
                else
                {
                    File.AppendAllLines(fileFullPath, logs, Encoding.UTF8);
                }
            }
        }
    }
}
