using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alan.Log.Core
{
    /// <summary>
    /// 日志模块配置实用类
    /// </summary>
    public sealed class LogUtils
    {
        static LogUtils()
        {
            _current = new LogUtils();
        }

        /// <summary>
        /// 当前LogUtils实例
        /// </summary>
        private static LogUtils _current;

        /// <summary>
        /// 获取当前LogUtils实例
        /// </summary>
        public static LogUtils Current { get { return _current; } }


        private LogUtils()
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
            this._lastLogModule = new Implement.LogEmpty();
        }

        /// <summary>
        /// 日志模块
        /// </summary>
        private readonly List<ILog> _logModules;

        /// <summary>
        /// 日志级别
        /// </summary>
        private readonly Dictionary<string, string> _logLevles;

        /// <summary>
        /// 不同级别的日志模块
        /// </summary>
        private Dictionary<string, List<ILog>> _logLevelModules;

        /// <summary>
        /// 当前最后一个日志模块
        /// </summary>
        private ILog _lastLogModule;

        /// <summary>
        /// 当前最后一个日志模块
        /// </summary>
        public ILog LastLogModule { get { return _lastLogModule; } }


        /// <summary>
        /// 注入日志级别模块
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="log">日志模块</param>
        internal LogUtils AddLogModule(string level, ILog log)
        {
            if (String.IsNullOrWhiteSpace(level)) throw new ArgumentNullException("level");
            if (log == null) throw new ArgumentNullException("log");

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

            return this;
        }

        /// <summary>
        /// 注入日志级别模块
        /// </summary>
        /// <param name="level">日志级别</param>
        internal TLog AddLogModule<TLog>(string level)
            where TLog : ILog, new()
        {
            var log = new TLog();
            this.AddLogModule(level, log);
            return log;
        }

        /// <summary>
        /// 迭代日志模块
        /// </summary>
        /// <param name="level"></param>
        /// <param name="iteral"></param>
        /// <returns></returns>
        internal LogUtils IteralLogModules(string level, Action<ILog> iteral)
        {
            if (!this._logLevelModules.ContainsKey(level))
                return this;

            var logModules = this._logLevelModules[level];
            if (logModules == null) return this;
            logModules.ForEach(iteral);
            return this;
        }

        /// <summary>
        /// 注入日志模块
        /// </summary>
        /// <param name="log">日志模块</param>
        internal LogUtils AddLogModule(ILog log)
        {
            if (log == null) throw new ArgumentNullException("log");

            this._logModules.Add(log);
            this._lastLogModule = log;

            return this;
        }


        /// <summary>
        /// 注入日志模块 并返回日志模块实例
        /// </summary>
        internal TLog AddLogModule<TLog>()
            where TLog : ILog, new()
        {
            var log = new TLog();
            this.InjectLogModule(log);

            return log;
        }


        /// <summary>
        /// 清空已注入的日志模块
        /// </summary>
        public LogUtils ClearLogModules()
        {
            this._logModules.Clear();

            return this;
        }

        /// <summary>
        /// 遍历所有日志模块
        /// </summary>
        /// <param name="iteral">迭代函数</param>
        public LogUtils IteralLogModules(Action<ILog> iteral)
        {
            this._logModules.ForEach(iteral);

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
