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
            return self.Log(id: log.Id, level: level, logger: log.Logger, category: log.Category, message: log.Message,
                note: log.Note, position: log.Position, request: log.Request, response: log.Response);
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
        /// <param name="request">请求内容</param>
        /// <param name="response">输出内容</param>
        public static ILog Log(this ILog self,
            string id = null,
            DateTime date = default(DateTime),
            string level = null,
            string logger = null,
            string category = null,
            string message = null,
            string note = null,
            string position = null,
            string request = null,
            string response = null)
        {
            self.Write(id: id, date: date, level: level, logger: logger, category: category, message: message, note: note, request: request, response: response, position: position);
            return self;
        }


        /// <summary>
        /// 记录危险日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.ILog</param>
        /// <param name="id">编号</param>
        /// <param name="date">日期</param>
        /// <param name="logger">标识者</param>
        /// <param name="category">分类</param>
        /// <param name="message">消息</param>
        /// <param name="note">备注</param>
        /// <param name="position">输出位置</param>
        /// <param name="request">请求内容</param>
        /// <param name="response">输出内容</param>
        public static ILog LogCritical(this ILog self, 
            string message,
            string id = null,
            DateTime date = default(DateTime),
            string logger = null,
            string category = null,
            string note = null,
            string position = null,
            string request = null,
            string response = null)
        {
            return self.Log(id: id, date: date, level: LogUtils.Current.GetLogLevel("critical"), logger: logger, category: category, message: message, note: note, position: position, request: request, response: response);
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.ILog</param>
        /// <param name="id">编号</param>
        /// <param name="date">日期</param>
        /// <param name="logger">标识者</param>
        /// <param name="category">分类</param>
        /// <param name="message">消息</param>
        /// <param name="note">备注</param>
        /// <param name="position">输出位置</param>
        /// <param name="request">请求内容</param>
        /// <param name="response">输出内容</param>
        public static ILog LogError(this ILog self,
            string message,
            string id = null,
            DateTime date = default(DateTime),
            string logger = null,
            string category = null,
            string note = null,
            string position = null,
            string request = null,
            string response = null)
        {
            return self.Log(id: id, date: date, level: LogUtils.Current.GetLogLevel("error"), logger: logger, category: category, message: message, note: note, position: position, request: request, response: response);

        }


        /// <summary>
        /// 记录警告日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.ILog</param>
        /// <param name="id">编号</param>
        /// <param name="date">日期</param>
        /// <param name="logger">标识者</param>
        /// <param name="category">分类</param>
        /// <param name="message">消息</param>
        /// <param name="note">备注</param>
        /// <param name="position">输出位置</param>
        /// <param name="request">请求内容</param>
        /// <param name="response">输出内容</param>
        public static ILog LogWarning(this ILog self, 
            string message,
            string id = null,
            DateTime date = default(DateTime),
            string logger = null,
            string category = null,
            string note = null,
            string position = null,
            string request = null,
            string response = null)
        {
            return self.Log(id: id, date: date, level: LogUtils.Current.GetLogLevel("warning"), logger: logger, category: category, message: message, note: note, position: position, request: request, response: response);
        }


        /// <summary>
        /// 记录信息日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.ILog</param>
        /// <param name="id">编号</param>
        /// <param name="date">日期</param>
        /// <param name="logger">标识者</param>
        /// <param name="category">分类</param>
        /// <param name="message">消息</param>
        /// <param name="note">备注</param>
        /// <param name="position">输出位置</param>
        /// <param name="request">请求内容</param>
        /// <param name="response">输出内容</param>
        public static ILog LogInfo(this ILog self,
            string message,
            string id = null,
            DateTime date = default(DateTime),
            string logger = null,
            string category = null,
            string note = null,
            string position = null,
            string request = null,
            string response = null)
        {
            return self.Log(id: id, date: date, level: LogUtils.Current.GetLogLevel("info"), logger: logger, category: category, message: message, note: note, position: position, request: request, response: response);

        }


        /// <summary>
        /// 记录调试日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.ILog</param>
        /// <param name="id">编号</param>
        /// <param name="date">日期</param>
        /// <param name="logger">标识者</param>
        /// <param name="category">分类</param>
        /// <param name="message">消息</param>
        /// <param name="note">备注</param>
        /// <param name="position">输出位置</param>
        /// <param name="request">请求内容</param>
        /// <param name="response">输出内容</param>
        public static ILog LogDebug(this ILog self,
            string message,
            string id = null,
            DateTime date = default(DateTime),
            string logger = null,
            string category = null,
            string note = null,
            string position = null,
            string request = null,
            string response = null)
        {
            return self.Log(id: id, date: date, level: LogUtils.Current.GetLogLevel("debug"), logger: logger, category: category, message: message, note: note, position: position, request: request, response: response);

        }

        /// <summary>
        /// 记录跟踪日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.ILog</param>
        /// <param name="id">编号</param>
        /// <param name="date">日期</param>
        /// <param name="logger">标识者</param>
        /// <param name="category">分类</param>
        /// <param name="message">消息</param>
        /// <param name="note">备注</param>
        /// <param name="position">输出位置</param>
        /// <param name="request">请求内容</param>
        /// <param name="response">输出内容</param>
        public static ILog LogTrace(this ILog self,
            string message,
            string id = null,
            DateTime date = default(DateTime),
            string logger = null,
            string category = null,
            string note = null,
            string position = null,
            string request = null,
            string response = null)
        {
            return self.Log(id: id, date: date, level: LogUtils.Current.GetLogLevel("trace"), logger: logger, category: category, message: message, note: note, position: position, request: request, response: response);

        }

    }
}
