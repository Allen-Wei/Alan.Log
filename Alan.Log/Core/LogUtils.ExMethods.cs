using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alan.Log.Core
{
    /// <summary>
    /// Alan.Log.Core.LogUtils 扩展方法
    /// </summary>
    public static class LogUtilsExMethods
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.LogUtils</param>
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
        public static LogUtils Log(this LogUtils self,
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
            //global log modules
            self.IteralLogModules(log => log.Log(
                id: id,
                date: date,
                level: level,
                logger: logger,
                category: category,
                message: message,
                note: note,
                position: position,
                request: request,
                response: response));

            //level log modules
            if (!String.IsNullOrWhiteSpace(level))
                self.IteralLogModules(level, log => log.Log(
                    id: id,
                    date: date,
                    level: level,
                    logger: logger,
                    category: category,
                    message: message,
                    note: note,
                    position: position,
                    request: request,
                    response: response));

            return self;
        }


        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="utils">Alan.Log.Core.LogUtils</param>
        /// <param name="log"></param>
        public static LogUtils Log(this LogUtils utils, Models.Log log)
        {
            var level = (log.Level.ToString() ?? "").ToLower();

            return utils.Log(
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
        public static LogUtils LogCritical(this LogUtils self,
            string id = null,
            DateTime date = default(DateTime),
            string logger = null,
            string category = null,
            string message = null,
            string note = null,
            string position = null,
            string request = null,
            string response = null)
        {
            return self.Log(id: id, date: date, level: LogUtils.Current.GetLogLevel("critical"), logger: logger,
                category: category, message: message, note: note, position: position, request: request,
                response: response);
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
        public static LogUtils LogError(this LogUtils self,
            string id = null,
            DateTime date = default(DateTime),
            string logger = null,
            string category = null,
            string message = null,
            string note = null,
            string position = null,
            string request = null,
            string response = null)
        {
            return self.Log(id: id, date: date, level: LogUtils.Current.GetLogLevel("error"), logger: logger,
                category: category, message: message, note: note, position: position, request: request,
                response: response);
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
        public static LogUtils LogWarning(this LogUtils self,
            string id = null,
            DateTime date = default(DateTime),
            string logger = null,
            string category = null,
            string message = null,
            string note = null,
            string position = null,
            string request = null,
            string response = null)
        {
            return self.Log(id: id, date: date, level: LogUtils.Current.GetLogLevel("warning"), logger: logger,
                category: category, message: message, note: note, position: position, request: request,
                response: response);
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
        public static LogUtils LogInfo(this LogUtils self,
            string id = null,
            DateTime date = default(DateTime),
            string logger = null,
            string category = null,
            string message = null,
            string note = null,
            string position = null,
            string request = null,
            string response = null)
        {
            return self.Log(id: id, date: date, level: LogUtils.Current.GetLogLevel("info"), logger: logger,
                category: category, message: message, note: note, position: position, request: request,
                response: response);
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
        public static LogUtils LogDebug(this LogUtils self,
            string id = null,
            DateTime date = default(DateTime),
            string logger = null,
            string category = null,
            string message = null,
            string note = null,
            string position = null,
            string request = null,
            string response = null)
        {
            return self.Log(id: id, date: date, level: LogUtils.Current.GetLogLevel("debug"), logger: logger,
                category: category, message: message, note: note, position: position, request: request,
                response: response);
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
        public static LogUtils LogTrace(this LogUtils self,
            string id = null,
            DateTime date = default(DateTime),
            string logger = null,
            string category = null,
            string message = null,
            string note = null,
            string position = null,
            string request = null,
            string response = null)
        {
            return self.Log(id: id, date: date, level: LogUtils.Current.GetLogLevel("trace"), logger: logger,
                category: category, message: message, note: note, position: position, request: request,
                response: response);
        }

        #region utils methods

        /// <summary>
        /// 注入指定级别的日志模块
        /// </summary>
        /// <param name="self">LogUtils</param>
        /// <param name="level">日志级别</param>
        /// <param name="log">日志模块</param>
        /// <returns></returns>
        public static LogUtils InjectLogModule(this LogUtils self, string level, ILog log)
        {
            return self.AddLogModule(level, log);
        }

        /// <summary>
        /// 注入指定级别的日志模块
        /// </summary>
        /// <param name="self">LogUtils</param>
        /// <param name="levels">日志级别(同时订阅多个级别日志可以以空格分隔)</param>
        /// <returns></returns>
        public static LogUtils InjectLogModule<TLog>(this LogUtils self, string levels)
            where TLog : ILog, new()
        {
            self.AddLogModule<TLog>(levels);
            return self;
        }

        /// <summary>
        /// 注入指定级别的日志模块
        /// </summary>
        /// <param name="self">LogUtils</param>
        /// <param name="levels">日志级别(同时订阅多个级别日志可以以空格分隔)</param>
        /// <returns></returns>
        public static TLog InjectLogModuleAppendConfig<TLog>(this LogUtils self, string levels)
            where TLog : ILog, new()
        {
            return self.AddLogModule<TLog>(levels);
        }

        /// <summary>
        /// 注入日志模块
        /// </summary>
        /// <param name="self">Alan.Log.Core.LogUtils</param>
        /// <param name="log">日志模块</param>
        public static LogUtils InjectLogModule(this LogUtils self, ILog log)
        {
            return self.AddLogModule(log);

        }

        /// <summary>
        /// 注入日志模块
        /// </summary>
        /// <param name="self">Alan.Log.Core.LogUtils</param>
        /// <param name="log">日志模块</param>
        public static ILog InjectLogModuleAppendConfig(this LogUtils self, ILog log)
        {
            self.AddLogModule(log);
            return log;
        }


        /// <summary>
        /// 注入日志模块
        /// </summary>
        /// <param name="self">Alan.Log.Core.LogUtils</param>
        public static LogUtils InjectLogModule<TLog>(this LogUtils self)
            where TLog : ILog, new()
        {
            self.AddLogModule<TLog>();

            return self;
        }

        /// <summary>
        /// 注入日志模块 并返回日志模块实例
        /// </summary>
        /// <param name="self">Alan.Log.Core.LogUtils</param>
        public static TLog InjectLogModuleAppendConfig<TLog>(this LogUtils self)
            where TLog : ILog, new()
        {
            return self.AddLogModule<TLog>();
        }

        #endregion


    }
}
