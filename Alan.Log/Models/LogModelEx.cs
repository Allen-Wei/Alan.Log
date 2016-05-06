using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alan.Log.Models.Ex
{

    /// <summary>
    /// LogModel的扩展方法
    /// </summary>
    public static class LogModelEx
    {
        /// <summary>
        /// 字符串转换成枚举LogLevel
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static Log.LogLevel ToLogLevel(this string level)
        {
            if (String.IsNullOrWhiteSpace(level)) return Log.LogLevel.Trace;

            level = level.ToLower();
            switch (level)
            {
                case "critical": return Log.LogLevel.Critical;
                case "error": return Log.LogLevel.Error;
                case "warning": return Log.LogLevel.Warning;
                case "info": return Log.LogLevel.Info;
                case "debug": return Log.LogLevel.Debug;
                case "trace": return Log.LogLevel.Trace;
                default: return Log.LogLevel.Trace;
            }
        }
    }

}
