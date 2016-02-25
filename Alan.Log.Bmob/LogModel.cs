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


        public override string table
        {
            get { return "Logs"; }
        }

        //读字段信息
        public override void readFields(BmobInput input)
        {
            base.readFields(input);

            this.score = input.getInt("score");
            this.cheatMode = input.getBoolean("cheatMode");
            this.playerName = input.getString("playerName");
        }

        //写字段信息
        public override void write(BmobOutput output, bool all)
        {
            base.write(output, all);

            output.Put("score", this.score);
            output.Put("cheatMode", this.cheatMode);
            output.Put("playerName", this.playerName);
        }
    }
}
}
