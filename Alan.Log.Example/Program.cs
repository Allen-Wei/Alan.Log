using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Alan.Log.Core;
using Alan.Log.Implement;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net.Mail;

namespace Alan.Log.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            LogUtils.Current.InjectLogModule<LogTraceWrite>()
                .InjectLogModule<LogSingleFile>()
                .InjectLogModule(new LogEmail("bovert@163.com", "alan.overt", "alan.dev@qq.com", "smtp.163.com", 25, false))
                .InjectLogModuleAppendConfig<LogSingleFile>().Config(@"E:\Temporary\SingleFile.txt");
            LogUtils.Current.InjectLogModuleAppendConfig<LogAutoSeperateFiles>().Config(10 * 1024, @"E:\Temporary", "MultiFile");

            //LogUtils.Current.InjectLogModule(new Alan.Log.RabbitMQ.LogRabbitMQ("106.39.123.229", "android", "android@2016", "demo.log"));


            LogUtils.Current.Log(new Models.Log
            {
                Id = Guid.NewGuid().ToString(),
                Level = Models.Log.LogLevel.Debug,
                Date = DateTime.Now,
                Category = "order",
                Message = "I'm message.",
                Note = "I'm note",
                Logger = "Alan Wei"
            });

            Console.ReadKey();


        }
    }
}
