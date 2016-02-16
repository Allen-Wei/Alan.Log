using Alan.Log.Core;

namespace Alan.Log.LogContainerImplement
{
    /// <summary>
    /// Alan.Log.Core.LogUtils 扩展方法
    /// </summary>
    public static class LogUtilsExMethods
    {

        /// <summary>
        /// 注入指定级别的日志模块
        /// </summary>
        /// <param name="self">LogUtils</param>
        /// <param name="levels">日志级别(同时订阅多个级别日志可以以空格分隔)</param>
        /// <returns></returns>
        public static LogUtils InjectLogModule<TLog>(this LogUtils self, string levels)
            where TLog : ILog, new()
        {
            self.InjectLogModuleAppendConfig<LogUtils, TLog>(levels);
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
            var log = new TLog();
            self.InjectLogModule(levels, log);
            return log;
        }




        /// <summary>
        /// 注入日志模块
        /// </summary>
        /// <param name="self">Alan.Log.Core.LogUtils</param>
        public static LogUtils InjectLogModule< TLog>(this LogUtils self)
            where TLog : ILog, new()
        {
            self.InjectLogModuleAppendConfig<LogUtils, TLog>();
            return self;
        }

        /// <summary>
        /// 注入日志模块 并返回日志模块实例
        /// </summary>
        /// <param name="self">Alan.Log.Core.LogUtils</param>
        public static TLog InjectLogModuleAppendConfig< TLog>(this LogUtils self)
            where TLog : ILog, new()
        {
            var log = new TLog();
            self.InjectLogModule(log);
            return log;
        }

    }
}
