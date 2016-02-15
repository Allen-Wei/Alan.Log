using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alan.Log.Core
{
    /// <summary>
    /// 日志模块扩展方法
    /// </summary>
    public static class ILogExMethods
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.ILog</param>
        /// <param name="log"></param>
        public static ILog Log(this ILog self, Models.Log log)
        {
            var level = (log.Level.ToString() ?? "").ToLower();
            self.Write(log.Id, log.Date, LogUtils.Current.GetLogLevel(level), log.Logger, log.Category, log.Message, log.Note, log.Request,
                log.Response, log.Position);

            return self;
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.ILog</param>
        /// <param name="id">编号</param>
        /// <param name="date">日期</param>
        /// <param name="level">级别</param>
        /// <param name="logger">标识者</param>
        /// <param name="category">分类</param>
        /// <param name="message">消息</param>
        /// <param name="note">备注</param>
        /// <param name="position">输出位置</param>
        public static ILog Log(this ILog self, string id, DateTime date, string level, string logger, string category, string message, string note,
            string position)
        {
            self.Write(id: id, date: date, level: level, logger: logger, category: category, message: message, note: note,
                request: null, response: null, position: position);
            return self;
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.ILog</param>
        /// <param name="id">编号</param>
        /// <param name="date">日期</param>
        /// <param name="level">级别</param>
        /// <param name="message">消息</param>
        /// <param name="position">输出位置</param>
        public static ILog Log(this ILog self, string id, DateTime date, string level, string message, string position)
        {
            self.Write(id: id, date: date, level: level, logger: null, category: null, message: message, note: null,
                request: null, response: null, position: position);

            return self;
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.ILog</param>
        /// <param name="date">日期</param>
        /// <param name="level">级别</param>
        /// <param name="message">消息</param>
        public static ILog Log(this ILog self, DateTime date, string level, string message)
        {
            self.Write(id: null, date: date, level: level, logger: null, category: null, message: message, note: null,
                request: null, response: null, position: null);

            return self;
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.ILog</param>
        /// <param name="level">级别</param>
        /// <param name="message">消息</param>
        public static ILog Log(this ILog self, string level, string message)
        {
            self.Write(id: null, date: DateTime.Now, level: level, logger: null, category: null, message: message, note: null,
                request: null, response: null, position: null);

            return self;
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.ILog</param>
        /// <param name="message">消息</param>
        public static ILog Log(this ILog self, string message)
        {
            self.Write(id: null, date: DateTime.Now, level: null, logger: null, category: null, message: message, note: null,
                request: null, response: null, position: null);

            return self;
        }


        /// <summary>
        /// 记录危险日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.ILog</param>
        /// <param name="message">消息</param>
        /// <param name="note">备注</param>
        public static ILog LogCritical(this ILog self, string message, string note)
        {
            self.Write(id: null, date: DateTime.Now, level: LogUtils.Current.GetLogLevel("critical"), logger: null, category: null, message: message, note: note,
                request: null, response: null, position: null);

            return self;
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.ILog</param>
        /// <param name="message">消息</param>
        /// <param name="note">备注</param>
        public static ILog LogError(this ILog self, string message, string note)
        {
            self.Write(id: null, date: DateTime.Now, level: LogUtils.Current.GetLogLevel("error"), logger: null, category: null, message: message, note: note,
                request: null, response: null, position: null);

            return self;
        }


        /// <summary>
        /// 记录警告日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.ILog</param>
        /// <param name="message">消息</param>
        /// <param name="note">备注</param>
        public static ILog LogWarning(this ILog self, string message, string note)
        {
            self.Write(id: null, date: DateTime.Now, level: LogUtils.Current.GetLogLevel("warning"), logger: null, category: null, message: message, note: note,
                request: null, response: null, position: null);

            return self;
        }


        /// <summary>
        /// 记录信息日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.ILog</param>
        /// <param name="message">消息</param>
        /// <param name="note">备注</param>
        public static ILog LogInfo(this ILog self, string message, string note)
        {
            self.Write(id: null, date: DateTime.Now, level: LogUtils.Current.GetLogLevel("info"), logger: null, category: null, message: message, note: note,
                request: null, response: null, position: null);

            return self;
        }


        /// <summary>
        /// 记录调试日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.ILog</param>
        /// <param name="message">消息</param>
        /// <param name="note">备注</param>
        public static ILog LogDebug(this ILog self, string message, string note)
        {
            self.Write(id: null, date: DateTime.Now, level: LogUtils.Current.GetLogLevel("debug"), logger: null, category: null, message: message, note: note,
                request: null, response: null, position: null);

            return self;
        }

        /// <summary>
        /// 记录跟踪日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.ILog</param>
        /// <param name="message">消息</param>
        /// <param name="note">备注</param>
        public static ILog LogTrace(this ILog self, string message, string note)
        {
            self.Write(id: null, date: DateTime.Now, level: LogUtils.Current.GetLogLevel("trace"), logger: null, category: null, message: message, note: note,
                request: null, response: null, position: null);

            return self;
        }

        /// <summary>
        /// 将自己添加到日志模块列表
        /// </summary>
        /// <param name="self"></param>
        public static ILog InjectThis(this ILog self)
        {
            LogUtils.Current.InjectLogModule(self);

            return self;
        }

        /// <summary>
        /// 只注入自己模块
        /// </summary>
        /// <param name="self"></param>
        public static ILog InjectJustThis(this ILog self)
        {
            LogUtils.Current.ClearLogModules();
            LogUtils.Current.InjectLogModule(self);

            return self;
        }
    }
}
