using Alan.Log.Core;

namespace Alan.Log.LogContainerImplement
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

    }
}
