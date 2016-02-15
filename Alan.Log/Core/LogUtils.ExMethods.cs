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
    {   /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="utils">Alan.Log.Core.LogUtils</param>
        /// <param name="log"></param>
        public static LogUtils Log(this LogUtils utils, Models.Log log)
        {
            utils.IteralLogModules(l => l.Log(log));

            return utils;
        }

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
        public static LogUtils Log(this LogUtils self, string id, DateTime date, string level, string logger, string category, string message, string note,
            string position)
        {
            self.IteralLogModules(log => log.Log(id: id, date: date, level: level, logger: logger, category: category, message: message, note: note, position: position));

            return self;
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.LogUtils</param>
        /// <param name="id">编号</param>
        /// <param name="date">日期</param>
        /// <param name="level">级别</param>
        /// <param name="message">消息</param>
        /// <param name="position">输出位置</param>
        public static LogUtils Log(this LogUtils self, string id, DateTime date, string level, string message, string position)
        {
            self.IteralLogModules(log => log.Log(id: id, date: date, level: level, message: message, position: position));
            return self;
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.LogUtils</param>
        /// <param name="date">日期</param>
        /// <param name="level">级别</param>
        /// <param name="message">消息</param>
        public static LogUtils Log(this LogUtils self, DateTime date, string level, string message)
        {
            self.IteralLogModules(log => log.Log(date: date, level: level, message: message));

            return self;
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.LogUtils</param>
        /// <param name="level">级别</param>
        /// <param name="message">消息</param>
        public static LogUtils Log(this LogUtils self, string level, string message)
        {
            self.IteralLogModules(log => log.Log(date: DateTime.Now, level: level, message: message));

            return self;
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.LogUtils</param>
        /// <param name="message">消息</param>
        public static LogUtils Log(this LogUtils self, string message)
        {
            self.IteralLogModules(log => log.Log(message: message));

            return self;
        }


        /// <summary>
        /// 记录危险日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.LogUtils</param>
        /// <param name="message">消息</param>
        /// <param name="note">备注</param>
        public static LogUtils LogCritical(this LogUtils self, string message, string note)
        {
            self.IteralLogModules(log => log.LogCritical(message: message, note: note));

            return self;
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.LogUtils</param>
        /// <param name="message">消息</param>
        /// <param name="note">备注</param>
        public static LogUtils LogError(this LogUtils self, string message, string note)
        {
            self.IteralLogModules(log => log.LogError(message: message, note: note));

            return self;
        }


        /// <summary>
        /// 记录警告日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.LogUtils</param>
        /// <param name="message">消息</param>
        /// <param name="note">备注</param>
        public static LogUtils LogWarning(this LogUtils self, string message, string note)
        {
            self.IteralLogModules(log => log.LogWarning(message: message, note: note));

            return self;
        }


        /// <summary>
        /// 记录信息日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.LogUtils</param>
        /// <param name="message">消息</param>
        /// <param name="note">备注</param>
        public static LogUtils LogInfo(this LogUtils self, string message, string note)
        {
            self.IteralLogModules(log => log.LogInfo(message: message, note: note));

            return self;
        }


        /// <summary>
        /// 记录调试日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.LogUtils</param>
        /// <param name="message">消息</param>
        /// <param name="note">备注</param>
        public static LogUtils LogDebug(this LogUtils self, string message, string note)
        {
            self.IteralLogModules(log => log.LogDebug(message: message, note: note));

            return self;
        }

        /// <summary>
        /// 记录跟踪日志
        /// </summary>
        /// <param name="self">Alan.Log.Core.LogUtils</param>
        /// <param name="message">消息</param>
        /// <param name="note">备注</param>
        public static LogUtils LogTrace(this LogUtils self, string message, string note)
        {
            self.IteralLogModules(log => log.LogTrace(message: message, note: note));
            return self;
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



        ///// <summary>
        ///// 设置日志级别
        ///// </summary>
        ///// <param name="self">Alan.Log.Core.LogUtils</param>
        ///// <param name="ciritical">危险</param>
        ///// <param name="error">错误/异常</param>
        ///// <param name="warning">警告</param>
        ///// <param name="info">信息</param>
        ///// <param name="debug">调试</param>
        ///// <param name="trace">捕获跟踪</param>
        //public static LogUtils SetLogLevel(this LogUtils self, string ciritical, string error, string warning, string info, string debug, string trace)
        //{
        //    self._logLevles["cirtical"] = ciritical;
        //    self._logLevles["error"] = error;
        //    self._logLevles["warning"] = warning;
        //    self._logLevles["info"] = info;
        //    self._logLevles["debug"] = debug;
        //    self._logLevles["trace"] = trace;

        //    return self;
        //}
    }
}
