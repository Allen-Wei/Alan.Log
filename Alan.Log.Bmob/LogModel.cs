using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cn.bmob.io;

namespace Alan.Log.Bmob
{
    public class LogModel : BmobTable
    {

        public string Id { get; set; }
        public string Category { get; set; }
        public BmobDate Date { get; set; }
        public string Message { get; set; }
        public string Note { get; set; }

        public override string table
        {
            get { return "Logs"; }
        }

        //读字段信息
        public override void readFields(BmobInput input)
        {
            base.readFields(input);

            this.Id = input.getString(nameof(this.Id));
            this.Category = input.getString(nameof(this.Category));
            this.Date = input.getDate(nameof(this.Date));
            this.Note = input.getString(nameof(this.Note));
            this.Message = input.getString(nameof(this.Message));
        }

        //写字段信息
        public override void write(BmobOutput output, bool all)
        {
            base.write(output, all);

            output.Put(nameof(this.Id), this.Id);
            output.Put(nameof(this.Date), this.Date);
            output.Put(nameof(this.Category), this.Category);
            output.Put(nameof(this.Message), this.Message);
            output.Put(nameof(this.Note), this.Note);
        }
    }
}
}
