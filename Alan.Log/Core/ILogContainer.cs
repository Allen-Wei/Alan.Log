using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alan.Log.Core
{
    /// <summary>
    /// ILog Container
    /// </summary>
    public interface ILogContainer
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="date">日期</param>
        /// <param name="level">级别</param>
        /// <param name="logger">标识者(比如system, admin)</param>
        /// <param name="category">分类(比如order, pay, comment)</param>
        /// <param name="message">消息</param>
        /// <param name="note">备注</param>
        /// <param name="position">输出位置</param>
        /// <param name="request">请求内容</param>
        /// <param name="response">输出内容</param>
        ILogContainer Log(
            string id = null,
            DateTime date = default(DateTime),
            string level = null,
            string logger = null,
            string category = null,
            string message = null,
            string note = null,
            string position = null,
            string request = null,
            string response = null);


        /// <summary>
        /// 注入日志级别模块
        /// </summary>
        /// <param name="levels">日志级别(同时订阅多个级别日志可以以空格分隔)</param>
        /// <param name="log">日志模块</param>
        ILogContainer InjectLogModule(string levels, ILog log);

        /// <summary>
        /// 注入日志模块
        /// </summary>
        /// <param name="log">日志模块</param>
        ILogContainer InjectLogModule(ILog log);


        /// <summary>
        /// 获取日志级别 
        /// </summary>
        /// <param name="level">{critical: 危险, error: 错误/异常, warning: 警告, info: 信息, debug: 调试, trace: 捕获跟踪}</param>
        /// <returns></returns>
        string GetLogLevel(string level);
    }

}
