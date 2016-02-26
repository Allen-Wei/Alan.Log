using cn.bmob.io;

namespace Alan.Log.Bmob.Utils
{
    /// <summary>
    /// 日志模型
    /// </summary>
    public class LogModel : BmobTable
    {

        /// <summary>
        /// 编号/标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public BmobDate Date { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public string Level { get; set; }
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






        private string _tableName;

        /// <summary>
        /// 实例化 LogModel 表名默认为 BmobLogs
        /// </summary>
        public LogModel() : this("BmobLogs") { }
        /// <summary>
        /// 实例化 LogModel
        /// </summary>
        /// <param name="tableName">表名</param>
        public LogModel(string tableName)
        {
            this._tableName = tableName;
        }
        public override string table
        {
            get { return this._tableName; }
        }

        //读字段信息
        public override void readFields(BmobInput input)
        {
            base.readFields(input);

            this.Id = input.getString(nameof(this.Id));
            this.Date = input.getDate(nameof(this.Date));
            this.Level = input.getString(nameof(this.Level));
            this.Logger = input.getString(nameof(this.Logger));
            this.Category = input.getString(nameof(this.Category));
            this.Message = input.getString(nameof(this.Message));
            this.Note = input.getString(nameof(this.Note));
            this.Request = input.getString(nameof(this.Request));
            this.Response = input.getString(nameof(this.Response));
            this.Position = input.getString(nameof(this.Position));

        }

        //写字段信息
        public override void write(BmobOutput output, bool all)
        {
            base.write(output, all);

            output.Put(nameof(this.Id), this.Id);
            output.Put(nameof(this.Date), this.Date);
            output.Put(nameof(this.Level), this.Level);
            output.Put(nameof(this.Logger), this.Logger);
            output.Put(nameof(this.Category), this.Category);
            output.Put(nameof(this.Message), this.Message);
            output.Put(nameof(this.Note), this.Note);
            output.Put(nameof(this.Request), this.Request);
            output.Put(nameof(this.Response), this.Response);
            output.Put(nameof(this.Position), this.Position);
        }
    }
}
