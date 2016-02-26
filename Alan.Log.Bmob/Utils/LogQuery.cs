using System;
using System.Threading.Tasks;
using cn.bmob.api;
using cn.bmob.exception;
using cn.bmob.io;
using cn.bmob.response;

namespace Alan.Log.Bmob.Utils
{
    /// <summary>
    /// 日志查询
    /// </summary>
    public class LogQuery
    {
        private BmobWindows _bmob;
        private string _tableName;

        /// <summary>
        /// 实例化日志查询
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="appKey">Application Key</param>
        /// <param name="restKey">REST Key</param>
        public LogQuery(string tableName, string appKey, string restKey)
        {
            this._tableName = tableName;
            this._bmob = new BmobWindows();
            this._bmob.initialize(appKey, restKey);
        }

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="query"></param>
        /// <param name="callback"></param>
        public void Query(BmobQuery query, Action<QueryCallbackData<LogModel>, BmobException> callback)
        {
            this._bmob.Find<LogModel>(this._tableName, query, (response, ex) =>
            {
                callback(response, ex);
            });
        }

        /// <summary>
        /// 执行异步查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<QueryCallbackData<LogModel>> QueryAsync(BmobQuery query)
        {
            var res = await this._bmob.FindTaskAsync<LogModel>(this._tableName, query);
            return res;
        }

        /// <summary>
        /// 分页查询记录
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="limit"></param>
        /// <param name="callback"></param>
        public void Query(int skip, int limit, Action<QueryCallbackData<LogModel>, BmobException> callback)
        {
            BmobQuery query = new BmobQuery();
            query.Skip(skip).Limit(limit);

            this.Query(query, callback);
        }
    }
}
