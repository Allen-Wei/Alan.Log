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
        /// <param name="level">日志级别</param>
        /// <returns></returns>
        public static LogUtils InjectLogModule<TLog>(this LogUtils self, string level)
            where TLog : ILog, new()
        {
            self.AddLogModule<TLog>(level);
            return self;
        }

        /// <summary>
        /// 注入指定级别的日志模块
        /// </summary>
        /// <param name="self">LogUtils</param>
        /// <param name="level">日志级别</param>
        /// <returns></returns>
        public static TLog InjectLogModuleAppendConfig<TLog>(this LogUtils self, string level)
            where TLog : ILog, new()
        {
            return self.AddLogModule<TLog>(level);
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
