using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alan.Log.Models
{
    /// <summary>
    /// 日志模型
    /// </summary>
    public class Log
    {
        /// <summary>
        /// 编号/标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public LogLevel Level { get; set; }

        /// <summary>
        /// 记录者
        /// </summary>
        public string Logger { get; set; }

        /// <summary>
        /// 分类(比如: 注册日志, 订单日志, 支付日志)
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 请求内容
        /// </summary>
        public string Request { get; set; }

        /// <summary>
        /// 输出内容
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        public enum LogLevel
        {
            /// <summary>
            /// 危险
            /// </summary>
            Critical,
            /// <summary>
            /// 错误/异常
            /// </summary>
            Error,
            /// <summary>
            /// 警告
            /// </summary>
            Warning,
            /// <summary>
            /// 信息
            /// </summary>
            Info,
            /// <summary>
            /// 调试
            /// </summary>
            Debug,
            /// <summary>
            /// 捕获/跟踪
            /// </summary>
            Trace,
            /// <summary>
            /// 无
            /// </summary>
            None
        }

    }

}
