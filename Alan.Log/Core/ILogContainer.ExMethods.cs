using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alan.Log.LogContainerImplement;

namespace Alan.Log.Core
{
    /// <summary>
    /// Alan.Log.Core.LogUtils 扩展方法
    /// </summary>
    public static class LogContainerExMethods
    {

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="utils">Alan.Log.Core.LogUtils</param>
        /// <param name="log"></param>
        public static TLogContainer Log<TLogContainer>(this TLogContainer utils, Models.Log log)
            where TLogContainer : ILogContainer
        {
            var level = (log.Level.ToString() ?? "").ToLower();

            utils.Log(
                id: log.Id,
                date: log.Date,
                level: level,
                logger: log.Logger,
                category: log.Category,
                message: log.Message,
                note: log.Note,
                position: log.Position,
                request: log.Request,
                response: log.Response);

            return utils;
        }

        /// <summary>
        /// 危险日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.LogUtils</param>
        /// <param name="id">编号</param>
        /// <param name="date">日期</param>
        /// <param name="logger">标识者</param>
        /// <param name="category">分类</param>
        /// <param name="message">消息</param>
        /// <param name="note">备注</param>
        /// <param name="position">输出位置</param>
        /// <param name="request">请求内容</param>
        /// <param name="response">输出内容</param>
        public static TLogContainer LogCritical<TLogContainer>(this TLogContainer self,
            string id = null,
            DateTime date = default(DateTime),
            string logger = null,
            string category = null,
            string message = null,
            string note = null,
            string position = null,
            string request = null,
            string response = null)
            where TLogContainer : ILogContainer
        {
            self.Log(id: id, date: date, level: LogUtils.Current.GetLogLevel("critical"), logger: logger,
                category: category, message: message, note: note, position: position, request: request,
                response: response);
            return self;
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.LogUtils</param>
        /// <param name="id">编号</param>
        /// <param name="date">日期</param>
        /// <param name="logger">标识者</param>
        /// <param name="category">分类</param>
        /// <param name="message">消息</param>
        /// <param name="note">备注</param>
        /// <param name="position">输出位置</param>
        /// <param name="request">请求内容</param>
        /// <param name="response">输出内容</param>
        public static TLogContainer LogError<TLogContainer>(this TLogContainer self,
            string id = null,
            DateTime date = default(DateTime),
            string logger = null,
            string category = null,
            string message = null,
            string note = null,
            string position = null,
            string request = null,
            string response = null)
            where TLogContainer : ILogContainer
        {
            self.Log(id: id, date: date, level: LogUtils.Current.GetLogLevel("error"), logger: logger,
                category: category, message: message, note: note, position: position, request: request,
                response: response);
            return self;
        }

        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.LogUtils</param>
        /// <param name="id">编号</param>
        /// <param name="date">日期</param>
        /// <param name="logger">标识者</param>
        /// <param name="category">分类</param>
        /// <param name="message">消息</param>
        /// <param name="note">备注</param>
        /// <param name="position">输出位置</param>
        /// <param name="request">请求内容</param>
        /// <param name="response">输出内容</param>
        public static TLogContainer LogWarning<TLogContainer>(this TLogContainer self,
            string id = null,
            DateTime date = default(DateTime),
            string logger = null,
            string category = null,
            string message = null,
            string note = null,
            string position = null,
            string request = null,
            string response = null)
            where TLogContainer : ILogContainer
        {
            self.Log(id: id, date: date, level: LogUtils.Current.GetLogLevel("warning"), logger: logger,
                category: category, message: message, note: note, position: position, request: request,
                response: response);
            return self;
        }


        /// <summary>
        /// 信息日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.LogUtils</param>
        /// <param name="id">编号</param>
        /// <param name="date">日期</param>
        /// <param name="logger">标识者</param>
        /// <param name="category">分类</param>
        /// <param name="message">消息</param>
        /// <param name="note">备注</param>
        /// <param name="position">输出位置</param>
        /// <param name="request">请求内容</param>
        /// <param name="response">输出内容</param>
        public static TLogContainer LogInfo<TLogContainer>(this TLogContainer self,
            string id = null,
            DateTime date = default(DateTime),
            string logger = null,
            string category = null,
            string message = null,
            string note = null,
            string position = null,
            string request = null,
            string response = null)
            where TLogContainer : ILogContainer
        {
            self.Log(id: id, date: date, level: LogUtils.Current.GetLogLevel("info"), logger: logger,
                category: category, message: message, note: note, position: position, request: request,
                response: response);
            return self;
        }


        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.LogUtils</param>
        /// <param name="id">编号</param>
        /// <param name="date">日期</param>
        /// <param name="logger">标识者</param>
        /// <param name="category">分类</param>
        /// <param name="message">消息</param>
        /// <param name="note">备注</param>
        /// <param name="position">输出位置</param>
        /// <param name="request">请求内容</param>
        /// <param name="response">输出内容</param>
        public static TLogContainer LogDebug<TLogContainer>(this TLogContainer self,
            string id = null,
            DateTime date = default(DateTime),
            string logger = null,
            string category = null,
            string message = null,
            string note = null,
            string position = null,
            string request = null,
            string response = null)
            where TLogContainer : ILogContainer
        {
            self.Log(id: id, date: date, level: LogUtils.Current.GetLogLevel("debug"), logger: logger,
                category: category, message: message, note: note, position: position, request: request,
                response: response);
            return self;
        }

        /// <summary>
        /// 捕获日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.LogUtils</param>
        /// <param name="id">编号</param>
        /// <param name="date">日期</param>
        /// <param name="logger">标识者</param>
        /// <param name="category">分类</param>
        /// <param name="message">消息</param>
        /// <param name="note">备注</param>
        /// <param name="position">输出位置</param>
        /// <param name="request">请求内容</param>
        /// <param name="response">输出内容</param>
        public static TLogContainer LogTrace<TLogContainer>(this TLogContainer self,
            string id = null,
            DateTime date = default(DateTime),
            string logger = null,
            string category = null,
            string message = null,
            string note = null,
            string position = null,
            string request = null,
            string response = null)
            where TLogContainer : ILogContainer
        {
            self.Log(id: id, date: date, level: LogUtils.Current.GetLogLevel("trace"), logger: logger,
                category: category, message: message, note: note, position: position, request: request,
                response: response);

            return self;
        }

        #region utils methods


        /// <summary>
        /// 注入指定级别的日志模块
        /// </summary>
        /// <param name="self">LogUtils</param>
        /// <param name="levels">日志级别(同时订阅多个级别日志可以以空格分隔)</param>
        /// <returns></returns>
        public static TLogContainer InjectLogModule<TLogContainer, TLog>(this TLogContainer self, string levels)
            where TLog : ILog, new()
            where TLogContainer : ILogContainer
        {
            self.InjectLogModuleAppendConfig<TLogContainer, TLog>(levels);
            return self;
        }

        /// <summary>
        /// 注入指定级别的日志模块
        /// </summary>
        /// <param name="self">LogUtils</param>
        /// <param name="levels">日志级别(同时订阅多个级别日志可以以空格分隔)</param>
        /// <returns></returns>
        public static TLog InjectLogModuleAppendConfig<TLogContainer, TLog>(this TLogContainer self, string levels)
            where TLog : ILog, new()
            where TLogContainer : ILogContainer
        {
            var log = new TLog();
            self.InjectLogModule(levels, log);
            return log;
        }




        /// <summary>
        /// 注入日志模块
        /// </summary>
        /// <param name="self">Alan.Log.Core.LogUtils</param>
        public static TLogContainer InjectLogModule<TLogContainer, TLog>(this TLogContainer self)
            where TLog : ILog, new()
            where TLogContainer : ILogContainer
        {
            self.InjectLogModuleAppendConfig<TLogContainer, TLog>();
            return self;
        }

        /// <summary>
        /// 注入日志模块 并返回日志模块实例
        /// </summary>
        /// <param name="self">Alan.Log.Core.LogUtils</param>
        public static TLog InjectLogModuleAppendConfig<TLogContainer, TLog>(this TLogContainer self)
            where TLog : ILog, new()
            where TLogContainer : ILogContainer
        {
            var log = new TLog();
            self.InjectLogModule(log);
            return log;
        }

        #endregion


    }
}
