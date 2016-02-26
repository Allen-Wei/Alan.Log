using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Alan.Log.Core;
using Alan.Log.ILogImplement;
using Alan.Log.LogContainerImplement;

namespace Alan.Log.Example
{
    class Program
    {
        static void Demostrations()
        {
            //捕获所有级别日志, 记录到单个日志文件里
            LogUtils.Current.InjectLogModule<LogSingleFile>()
                //捕获所有级别日志, 发送到bovert@163.com邮箱
                .InjectLogModule(new LogEmail("alan.dev@qq.com", "alan.dev@qq.com password", "bovert@163.com",
                    "smtp.qq.com", 587, true))
                //捕获error级别日志, 发送到alan.dev@qq.com邮箱
                .InjectLogModule("error",
                    new LogEmail("bovert@163.com", "alan.overt", "alan.dev@qq.com", "smtp.163.com", 25, false))
                //捕获所有级别日志, 记录到文件, 如果文件大于100KB自动分割文件.
                .InjectLogModule(new LogAutoSeperateFiles(fileMaxSizeBytes: 100 * 1024, fileDirectoryPath: @"E:\Temporary",
                    fileNamePrefix: "multi-log-all"))
                //捕获所有info级别日志, 记录到文件, 如果文件大于100KB自动分割文件.
                .InjectLogModule("info", new LogAutoSeperateFiles(100 * 1024, @"E:\Temporary", "multi-log-info"));


            LogUtils.Current.InjectLogModule<LogSingleFile>();
            LogUtils.Current.InjectLogModule(new LogSingleFile());
            LogUtils.Current.InjectLogModule(new LogSingleFile(@"E:\Temporary\log.txt"));
            LogUtils.Current.InjectLogModuleAppendConfig<LogSingleFile>().Config(@"E:\Temporary\log.txt");

            LogUtils.Current.InjectLogModule("error", new LogSingleFile());
            LogUtils.Current.InjectLogModule(new Alan.Log.RabbitMQ.LogRabbitMQ(host: "192.168.121.129", userName: "test", passWord: "test", exchange: "topic-log-test"));
            LogUtils.Current.InjectLogModule("error", new LogSingleFile(@"E:\"));

        }
        static void Main(string[] args)
        {
            LogUtils.Current.InjectLogModule(new Alan.Log.Bmob.LogBmob("Logs", "b61048de9001d146307bdd704e87c59b",
                "fa1198d5867bdce5799a813c2933f420"));
            var query = new Alan.Log.Bmob.Utils.LogQuery("Logs", "b61048de9001d146307bdd704e87c59b", "fa1198d5867bdce5799a813c2933f420");

            //写日志, 级别 error
            LogUtils.Current.Log(new Models.Log
            {
                Id = Guid.NewGuid().ToString(),
                Level = Models.Log.LogLevel.Error,
                Date = DateTime.Now,
                Category = "order",
                Message = "order error",
                Note = "I'm note",
                Logger = "Alan Wei @ error"
            });

            //写日志, 级别 info
            LogUtils.Current.Log(id: Guid.NewGuid().ToString(),
                date: DateTime.Now,
                level: "info",
                category: "two",
                logger: "Alan @ info",
                message: "info level log message");

            query.Query(0, 10, (res, ex) =>
            {
                Console.WriteLine(res.results.Count);
                res.results.ToList().ForEach(l =>
                {
                    Console.WriteLine("{0} {1}", l.Id, l.Message);
                });
            });


            Console.ReadKey();
        }

    }
}
