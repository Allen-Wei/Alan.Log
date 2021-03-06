﻿using System;
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


        static void Main(string[] args)
        {
            SeperateFileLogByDate();
            SingleFileLog();
            WriteLog();
        }


        static void WriteLog()
        {
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
            LogUtils.Current.Log(id:
                Guid.NewGuid().ToString(),
                date: DateTime.Now,
                level: "info",
                category: "two",
                logger: "Alan @ info",
                message: "info level log message");


        }
        static void InjectEmailLog()
        {
            //捕获所有级别日志, 发送到bovert@163.com邮箱
            LogUtils.Current.InjectLogModule(new LogEmail("alan.dev@qq.com", "alan.dev@qq.com password", "bovert@163.com", "smtp.qq.com", 587, true))
           //捕获error级别日志, 发送到alan.dev@qq.com邮箱
           .InjectLogModule("error", new LogEmail("bovert@163.com", "alan.overt", "alan.dev@qq.com", "smtp.163.com", 25, false));
        }
        static void SingleFileLog()
        {

            LogUtils.Current.InjectLogModule<LogSingleFile>();
            LogUtils.Current.InjectLogModule(new LogSingleFile());
            LogUtils.Current.InjectLogModule(new LogSingleFile(@"D:\Temporary\logs\log.txt"));
            LogUtils.Current.InjectLogModuleAppendConfig<LogSingleFile>().Config(@"D:\Temporary\logs\log.txt");

            LogUtils.Current.InjectLogModule("error", new LogSingleFile());
        }

        static void SeperateFileLogBySize()
        {
            LogUtils.Current
                //捕获所有级别日志, 记录到文件, 如果文件大于100KB自动分割文件.
                .InjectLogModule(new LogAutoSeperateFiles(fileMaxSizeBytes: 100 * 1024, fileDirectoryPath: @"E:\Temporary", fileNamePrefix: "multi-log-all"))
                //捕获所有info级别日志, 记录到文件, 如果文件大于100KB自动分割文件.
                .InjectLogModule("info", new LogAutoSeperateFiles(100 * 1024, @"E:\Temporary", "multi-log-info"));
        }

        static void SeperateFileLogByDate()
        {
            LogUtils.Current.InjectLogModule(new LogAutoSeperateFilesByDate(@"D:\Temporary\logs\date.log"));
        }

    }
}
