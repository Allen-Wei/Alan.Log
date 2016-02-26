using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alan.Log.Bmob.Utils;
using Alan.Log.Core;

namespace Alan.Log.Bmob
{
    /// <summary>
    /// Bmob日志实现
    /// </summary>
    public class LogBmob : ILog
    {
        private string _tableName;
        private readonly cn.bmob.api.BmobWindows _bmob;

        /// <summary>
        /// 实例化 LogBmob
        /// </summary>
        /// <param name="tableName">表明</param>
        /// <param name="appKey">Application Key</param>
        /// <param name="restKey">REST Key</param>
        public LogBmob(string tableName, string appKey, string restKey)
        {
            this._tableName = tableName;
            _bmob = new cn.bmob.api.BmobWindows();
            _bmob.initialize(appKey, restKey);
        }
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="id"></param>
        /// <param name="date"></param>
        /// <param name="level"></param>
        /// <param name="logger"></param>
        /// <param name="category"></param>
        /// <param name="message"></param>
        /// <param name="note"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="position"></param>
        public void Write(string id, DateTime date, string level, string logger, string category, string message, string note,
            string request, string response, string position)
        {
            if (date == default(DateTime)) date = DateTime.Now;

            var model = new LogModel(this._tableName)
            {
                Id = id,
                Date = new cn.bmob.io.BmobDate()
                {
                    iso = date.ToString("yyyy-MM-dd HH:mm:ss")
                },
                Level = level,
                Logger = logger,
                Category = category,
                Message = message,
                Note = note,
                Request = request,
                Response = response,
                Position = position
            };
            _bmob.CreateTaskAsync(model);
        }
    }
}
