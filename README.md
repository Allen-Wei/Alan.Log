
# Alan.Log

核心模块, 内置单文件日志(日志写进单个文件), 多文件日志(日志根据大小写进多个文件)和邮件日志(将日志发送到指定邮箱)实现.
你可以注册多个日志模块, 所以这里你如果发布一个日志可能会有多个日志模块接收到日志. 提供 Fluent 风格的调用.

## Alan.Log.RabbitMQ

这个是利用RabbitMQ消息队列实现的日志模块.

## Install
	
	Install-Package Alan.Log

如果需要使用RabbitMQ来扩展日志系统, 可以 Alan.Log.RabbitMQ 模块: 

	Install-Package Alan.Log.RabbitMQ

## Example

下面是使用示例, 使用起来很简单: 
			
    using Alan.Log.Core;
    using Alan.Log.ILogImplement;
    using Alan.Log.LogContainerImplement;

    //捕获所有级别日志, 记录到单个日志文件里
    LogUtils.Current.InjectLogModule<LogSingleFile>()
        //捕获所有级别日志, 发送到bovert@163.com邮箱
        .InjectLogModule(new LogEmail("alan.dev@qq.com", "alan.dev@qq.com password", "bovert@163.com", "smtp.qq.com", 587, true))
        //捕获error级别日志, 同时发送到alan.dev@qq.com和alan.wei43@qq.com两个邮箱
        .InjectLogModule("error", new LogEmail("bovert@163.com", "alan-overt", "alan.dev@qq.com alan.wei43@qq.com", "smtp.163.com", 25, false))
        //捕获所有级别日志, 记录到文件, 如果文件大于100KB自动分割文件.
        .InjectLogModule(new LogAutoSeperateFiles(fileMaxSizeBytes: 100 * 1024, fileDirectoryPath: @"E:\Temporary", fileNamePrefix: "multi-log-all"))
        //捕获所有info级别日志, 记录到文件, 如果文件大于100KB自动分割文件.
        .InjectLogModule("info", new LogAutoSeperateFiles(100 * 1024, @"E:\Temporary", "multi-log-info"))
        //这个需要 Alan.Log.RabbitMQ 模块
        .InjectLogModule(new Alan.Log.RabbitMQ.LogRabbitMQ("host address", "user name", "password", "exchange name"));


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
    LogUtils.Current.Log(id: Guid.NewGuid().ToString(), date: DateTime.Now, level: "info", logger: "Alan @ info", message: "info level log message");


日志的级别主要分为以下几种: 

	critical: 危险 
	error: 错误/异常 
	warning: 警告 
	info: 信息 
	debug: 调试 
	trace: 捕获


大致的使用, 上述的几个InjectLogModule已经演示了, 主要分类两种类型的日志模块, 一种是捕获某级别的日志, 另一种是捕获所有级别的日志.

下面以 `LogSingleFile` 单文件日志的简单使用

	//方法1: 
	LogUtils.Current.InjectLogModule<LogSingleFile>();

	//方法2
	LogUtils.Current.InjectLogModule(new LogSingleFile());

	//方法3 
	LogUtils.Current.InjectLogModule(new LogSingleFile(@"E:\Temporary\log.txt"));

	//方法4
	LogUtils.Current.InjectLogModuleAppendConfig<LogSingleFile>().Config(@"E:\Temporary\log.txt");

	//方法5
	LogUtils.Current.InjectLogModule("error", new LogSingleFile());

	//方法6
	LogUtils.Current.InjectLogModule("error info", new LogSingleFile(@"E:\Temporary\.log.txt"));
	


方法1, 2会把日志写到 `Path.Combine(Environment.CurrentDirectory, "LogSingleFile.txt")` 里. 方法3, 4会把日志写到 `E:\Temporary\log.txt` 里. 方法5只捕获error级别日志, 而方法6则会捕获error和info级别的日志.

下面是 `LogEmail` 邮件日志的简单使用:

    //方法1 
    LogUtils.Current.InjectLogModule(new LogEmail("bovert@163.com", "password", "alan@qq.com alan@163.com", "smtp.163.com", 25, false));
    //方法2
    LogUtils.Current.InjectLogModule(new LogEmail("bovert@163.com", "password", "alan@qq.com alan@163.com", "smtp.163.com", 25, false, "bovert@163.com", "sender name"));
    //方法3
    LogUtils.Current.InjectLogModule("error", new LogEmail("bovert@163.com", "password", "alan@qq.com alan@163.com", "smtp.163.com", 25, false));
    //方法4
    LogUtils.Current.InjectLogModule("error debug", new LogEmail("bovert@163.com", "password", "alan@qq.com alan@163.com", "smtp.163.com", 25, false));

邮件日志模块使用的微软提供的SmtpClient库, 网易邮箱 SSL: false, Port: 25 测试通过, QQ邮箱 SSL: true, Port: 587 测试通过. 

其中方法1,2会把所有级别日志使用邮箱bovert@163.com发送到alan@qq.com和alan@163.com两个邮箱地址. 方法3,4会捕获指定级别的日志发送到指定邮箱。

其他日志模块实现的使用方法类似, 就不一一列举, 你直接实例化对象时, 构造函数就会有参数提示.

## Custom Implement

扩展很简单, 你需要实现 `Alan.Log.Core.ILog` 接口就可以了.
你可以参考源码里的几个实现:
	
* [单文件日志](https://github.com/Allen-Wei/Alan.Log/blob/master/Alan.Log/ILogImplement/LogSingleFile.cs)
* [自动分割多个文件日志](https://github.com/Allen-Wei/Alan.Log/blob/master/Alan.Log/ILogImplement/LogAutoSeperateFiles.cs)
* [Trace.Write输出](https://github.com/Allen-Wei/Alan.Log/blob/master/Alan.Log/ILogImplement/LogTraceWrite.cs)
* [发送邮件日志](https://github.com/Allen-Wei/Alan.Log/blob/master/Alan.Log/ILogImplement/LogEmail.cs)
* [RabbitMQ实现](https://github.com/Allen-Wei/Alan.Log/blob/master/Alan.Log.RabbitMQ/LogRabbitMQ.cs)

上述是日志模块的实现.

你还需要有一个日志容器(ILogContainer)来容纳多个日志模块, 然后提供写日志的接口, 写日志的时候遍历注册的日志模块, 并调用日志模块的Write方法写日志. `Alan.Log` 里已经有了一个实现 `Alan.Log.LogContainerImplement.LogContainer.cs`, 然后利用单例模式, 实现了全局的一个唯一日志容器 `LogUtils.Current` .
`LogContainer.cs` 和 `LogUtils.cs` 是我自己的实现, 你也可以实现接口 `ILogContainer.cs` 来根据自己的业务需求实现日志容器.
这个日志模块耦合度还是很低的.
