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
    public sealed class LogUtils : LogContainer, ILogContainer
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


        private LogUtils() : base()
        {
        }



        ///// <summary>
        ///// 注入日志级别模块
        ///// </summary>
        ///// <param name="levels">日志级别(同时订阅多个级别日志可以以空格分隔)</param>
        ///// <param name="log">日志模块</param>
        //internal LogUtils AddLogModule(string levels, ILog log)
        //{
        //    base.InjectLogModule(levels, log);
        //    return this;
        //}

        ///// <summary>
        ///// 注入日志级别模块
        ///// </summary>
        ///// <param name="levels">日志级别(同时订阅多个级别日志可以以空格分隔)</param>
        //internal TLog AddLogModule<TLog>(string levels)
        //    where TLog : ILog, new()
        //{
        //    return base.InjectLogModule<TLog>(levels);
        //}

        ///// <summary>
        ///// 迭代日志模块
        ///// </summary>
        ///// <param name="level"></param>
        ///// <param name="iteral"></param>
        ///// <returns></returns>
        //internal LogUtils IteralLogModules(string level, Action<ILog> iteral)
        //{
        //    base.EachLogModules(level, iteral);
        //    return this;

        //}

        ///// <summary>
        ///// 注入日志模块
        ///// </summary>
        ///// <param name="log">日志模块</param>
        //internal LogUtils AddLogModule(ILog log)
        //{
        //    base.InjectLogModule(log);
        //    return this;

        //}


        ///// <summary>
        ///// 注入日志模块 并返回日志模块实例
        ///// </summary>
        //internal TLog AddLogModule<TLog>()
        //    where TLog : ILog, new()
        //{
        //    return base.InjectLogModule<TLog>();

        //}

        ///// <summary>
        ///// 遍历所有日志模块
        ///// </summary>
        ///// <param name="iteral">迭代函数</param>
        //public LogUtils IteralLogModules(Action<ILog> iteral)
        //{
        //    base.EachLogModules(iteral);
        //    return this;
        //}

    }
}
