﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alan.Log.Core
{
    /// <summary>
    /// 日志接口
    /// </summary>
    public interface ILog
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
        void Write(string id,
            DateTime date,
            string level,
            string logger,
            string category,
            string message,
            string note,
            string request,
            string response,
            string position);

    }
}
