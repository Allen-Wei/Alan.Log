
# Alan.Log

����ģ��, ���õ��ļ���־(��־д�������ļ�), ���ļ���־(��־���ݴ�Сд������ļ�)���ʼ���־(����־���͵�ָ������)ʵ��.
�����ע������־ģ��, �����������������һ����־���ܻ��ж����־ģ����յ���־. �ṩ Fluent ���ĵ���.

## Alan.Log.RabbitMQ

���������RabbitMQ��Ϣ����ʵ�ֵ���־ģ��.

## Install
	
	Install-Package Alan.Log

�����Ҫʹ��RabbitMQ����չ��־ϵͳ, ���� Alan.Log.RabbitMQ ģ��: 

	Install-Package Alan.Log.RabbitMQ

## Example

������ʹ��ʾ��, ʹ�������ܼ�: 
			
	 using Alan.Log.Core;
	 using Alan.Log.LogContainerImplement;

     //�������м�����־, ��¼��������־�ļ���
     LogUtils.Current.InjectLogModule<LogSingleFile>()
         //�������м�����־, ���͵�bovert@163.com����
         .InjectLogModule(new LogEmail("alan.dev@qq.com", "alan.dev@qq.com password", "bovert@163.com", "smtp.qq.com", 587, true))
         //����error������־, ͬʱ���͵�alan.dev@qq.com��alan.wei43@qq.com��������
         .InjectLogModule("error", new LogEmail("bovert@163.com", "alan-overt", "alan.dev@qq.com alan.wei43@qq.com", "smtp.163.com", 25, false))
         //�������м�����־, ��¼���ļ�, ����ļ�����100KB�Զ��ָ��ļ�.
         .InjectLogModule(new LogAutoSeperateFiles(fileMaxSizeBytes: 100 * 1024, fileDirectoryPath: @"E:\Temporary", fileNamePrefix: "multi-log-all"))
         //��������info������־, ��¼���ļ�, ����ļ�����100KB�Զ��ָ��ļ�.
         .InjectLogModule("info", new LogAutoSeperateFiles(100 * 1024, @"E:\Temporary", "multi-log-info"))
         //�����Ҫ Alan.Log.RabbitMQ ģ��
         .InjectLogModule(new Alan.Log.RabbitMQ.LogRabbitMQ("host address", "user name", "password", "exchange name"));


     //д��־, ���� error
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

     //д��־, ���� info
     LogUtils.Current.Log(id: Guid.NewGuid().ToString(), date: DateTime.Now, level: "info", logger: "Alan @ info", message: "info level log message");


��־�ļ�����Ҫ��Ϊ���¼���: 

	critical: Σ�� 
	error: ����/�쳣 
	warning: ���� 
	info: ��Ϣ 
	debug: ���� 
	trace: ����


���µ�ʹ��, �����ļ���InjectLogModule�Ѿ���ʾ��, ��Ҫ�����������͵���־ģ��, һ���ǲ���ĳ�������־, ��һ���ǲ������м������־.

������ `LogSingleFile` ʵ��Ϊ�����ܼ�����Ҫ�÷�:

	//����1: 
	LogUtils.Current.InjectLogModule<LogSingleFile>();

	//����2
	LogUtils.Current.InjectLogModule(new LogSingleFile());

	//����3 
	LogUtils.Current.InjectLogModule(new LogSingleFile(@"E:\Temporary\log.txt"));

	//����4
	LogUtils.Current.InjectLogModuleAppendConfig<LogSingleFile>().Config(@"E:\Temporary\log.txt");

	//����5
	LogUtils.Current.InjectLogModule("error", new LogSingleFile());

	//����6
	LogUtils.Current.InjectLogModule("error", new LogSingleFile(@"E:\Temporary\.log.txt"));
	


����1, 2�����־д�� `Path.Combine(Environment.CurrentDirectory, "LogSingleFile.txt")` ��. ����3, 4�����־д�� `E:\Temporary\log.txt` ��. ����5, 6ֻ����error������־, ��д�������ļ�.


## ��չ

��չ�ܼ�, ����Ҫʵ�� `Alan.Log.Core.ILog` �ӿھͿ�����.
����Բο�Դ����ļ���ʵ��:
	
* [���ļ���־](https://github.com/Allen-Wei/Alan.Log/blob/master/Alan.Log/ILogImplement/LogSingleFile.cs)
* [�Զ��ָ����ļ���־](https://github.com/Allen-Wei/Alan.Log/blob/master/Alan.Log/ILogImplement/LogAutoSeperateFiles.cs)
* [Trace.Write���](https://github.com/Allen-Wei/Alan.Log/blob/master/Alan.Log/ILogImplement/LogTraceWrite.cs)
* [�����ʼ���־](https://github.com/Allen-Wei/Alan.Log/blob/master/Alan.Log/ILogImplement/LogEmail.cs)
* [RabbitMQʵ��](https://github.com/Allen-Wei/Alan.Log/blob/master/Alan.Log.RabbitMQ/LogRabbitMQ.cs)


