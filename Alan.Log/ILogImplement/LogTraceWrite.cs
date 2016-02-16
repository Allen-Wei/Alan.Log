using System;
using System.Diagnostics;
using Alan.Log.Core;
using Alan.Log.LogContainerImplement;

namespace Alan.Log.ILogImplement
{
    /// <summary>
    /// System.Diagnostics.Trace 实现
    /// </summary>
    public class LogTraceWrite : ILog
    {

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="date">日期</param>
        /// <param name="level">级别</param>
        /// <param name="logger">标识者</param>
        /// <param name="category">分类(比如 注册/订单/支付/添加好友)</param>
        /// <param name="message">消息</param>
        /// <param name="note">备注</param>
        /// <param name="request">请求内容</param>
        /// <param name="response">响应内容</param>
        /// <param name="position">输出位置</param>
        public void Write(string id, DateTime date, string level, string logger, string category, string message, string note,
            string request, string response, string position)
        {
            var output = String.Format("Id: {0}, Date: {1}, Level: {2}, Logger: {3}, Category: {4}, Message: {5}, Note: {6}, Request: {7}, Response: {8}, Position: {9}", 
                id, date, level, logger, category, message, note, request, response, position);

            if (level == LogUtils.Current.GetLogLevel("error") || level == LogUtils.Current.GetLogLevel("critical"))
            {
                Trace.TraceError(output);
            }
            else if (level == LogUtils.Current.GetLogLevel("info"))
            {
                Trace.TraceWarning(output);
            }
            else if (level == LogUtils.Current.GetLogLevel("info") || level == LogUtils.Current.GetLogLevel("debug"))
            {
                Trace.TraceInformation(output);
            }
            else
            {
                Trace.WriteLine(output);
            }
        }
    }
}
