using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alan.Log.Core
{


    /// <summary>
    /// 日记模块的封装
    /// </summary>
    public class LogContainer : ILogContainer
    {
        /// <summary>
        /// 日记模块的封装
        /// </summary>
        protected LogContainer()
        {
            this._logModules = new List<ILog>();
            this._logLevelModules = new Dictionary<string, List<ILog>>();

            //初始化日志Level
            this._logLevles = new Dictionary<string, string>
            {
                { "critical", "critical"}
                , { "error", "error"}
                , { "warning", "warning"}
                , { "info", "info"}
                , { "debug", "debug"}
                , { "trace", "trace"}
            };
        }

        /// <summary>
        /// 日志级别
        /// </summary>
        protected readonly Dictionary<string, string> _logLevles;



        #region level log

        /// <summary>
        /// 不同级别的日志模块
        /// </summary>
        protected IDictionary<string, List<ILog>> _logLevelModules;


        /// <summary>
        /// 注入日志级别模块
        /// </summary>
        /// <param name="levels">日志级别(同时订阅多个级别日志可以以空格分隔)</param>
        /// <param name="log">日志模块</param>
        public ILogContainer InjectLogModule(string levels, ILog log)
        {
            if (String.IsNullOrWhiteSpace(levels)) throw new ArgumentNullException("levels");
            if (log == null) throw new ArgumentNullException("log");

            levels.Split(' ').ToList().ForEach(level =>
            {
                List<ILog> logModules;
                if (this._logLevelModules.ContainsKey(level))
                {
                    logModules = this._logLevelModules[level];
                    if (logModules == null)
                    {
                        logModules = new List<ILog>();
                        this._logLevelModules[level] = logModules;
                    }
                }
                else
                {
                    logModules = new List<ILog>();
                    this._logLevelModules.Add(level, logModules);
                }

                logModules.Add(log);
            });

            return this;
        }


        /// <summary>
        /// 迭代日志模块
        /// </summary>
        /// <param name="level"></param>
        /// <param name="iteral"></param>
        /// <returns></returns>
        private ILogContainer IteralLogModules(string level, Action<ILog> iteral)
        {
            if (!this._logLevelModules.ContainsKey(level))
                return this;

            var logModules = this._logLevelModules[level];
            if (logModules == null) return this;

            logModules.ForEach(iteral);

            return this;
        }

        #endregion


        #region global log


        /// <summary>
        /// 日志模块
        /// </summary>
        protected readonly List<ILog> _logModules;


        /// <summary>
        /// 注入日志模块
        /// </summary>
        /// <param name="log">日志模块</param>
        public ILogContainer InjectLogModule(ILog log)
        {
            if (log == null) throw new ArgumentNullException("log");

            this._logModules.Add(log);

            return this;
        }


        /// <summary>
        /// 遍历所有日志模块
        /// </summary>
        /// <param name="iteral">迭代函数</param>
        private ILogContainer IteralLogModules(Action<ILog> iteral)
        {
            this._logModules.ForEach(iteral);

            return this;
        }


        #endregion


        /// <summary>
        /// 写日志
        /// </summary>
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
        public ILogContainer Log(
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
            this.IteralLogModules(log => log.Log(
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
                this.IteralLogModules(level, log => log.Log(
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

            return this;
        }






        /// <summary>
        /// 清空已注入的日志模块
        /// </summary>
        public LogContainer ClearLogModules()
        {
            this._logModules.Clear();
            this._logLevelModules = new Dictionary<string, List<ILog>>();

            return this;
        }


        /// <summary>
        /// 获取日志级别 
        /// </summary>
        /// <param name="level">{critical: 危险, error: 错误/异常, warning: 警告, info: 信息, debug: 调试, trace: 捕获跟踪}</param>
        /// <returns></returns>
        public string GetLogLevel(string level)
        {
            if (this._logLevles.ContainsKey(level))
                return this._logLevles[level];

            return String.Empty;
        }

    }
}

