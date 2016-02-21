
# Alan.Log.RabbitMQ
��������־ģ����Ϊ���ӽ���һ���������RabbitMQ���ַ���־. ��ͳ����־ģ�鶼�ǰ���־д����վ���ڵķ������ϵ��ļ���������ݿ���, ����RabbitMQ���Կ���һ������ϵķֲ�ʽ��־ϵͳ, ���Ժܼ򵥸�Ч�ؽ���־�ַ���������������.

## Install & Introduction
�����ֱ�Ӱ� [ALan.Log](https://github.com/Allen-Wei/Alan.Log) ���ص�����, Ȼ���л��� [Alan.Log.RabbitMQ.Example](https://github.com/Allen-Wei/Alan.Log/tree/master/Alan.Log.RabbitMQ.Example) Ŀ¼. 

Ŀ¼����һ�� Visual Studio��Web��Ŀ�ļ� **Alan.Log.RabbitMQ.Example.sln**, ������������վ��, ��վע����һ�� `IHttpModule`�� `LogModule`, �� `LogModule` ���ÿ��Web�������վ�쳣��������־. �� **Global.asax** Ӧ������ʱע����������־ģ��, ���ļ���־ģ���RabbitMQ��־�ַ�ģ��. 

Ŀ¼NodeJsExample����һ��nodejs��Ŀ, ����socket.io��VS(Visual Studio)��Web��վ�ַ�����־ʵʱ��ʾ����ҳ��.

VS��Ŀ, �����ֱ��F5���в���, ���߷�������. NodeJsExample ����Ҫ������ `npm install` ������ģ�鰲װ�ˡ� 

## Configuration
������Ҫ����RabbitMQ Server�ķ�������ַ, ��֤��Ϣ�ͽ���������. 
VS��Ŀ��� **Global.asax** ע��RabbitMQ��־ģ��ʱ, ͨ������ָ����Щ��Ϣ. 
NodeJS��Ŀ��, �� **app.js** �� `amqp.connect("amqp://username:password@IpAddress:port", callback)` ��ָ��.

## Test
������ɾͿ��Բ�����. Ϊ�˰�ȫ�����������VS��Ŀ, ��Ϊ��������������VS��Ŀ��. �����������NodeJS��Ŀ, ��������δ�����Ļ������쳣.
������VS��Ŀ֮��, �Ϳ�������NodeJS��Ŀ��, NodeJS��Ŀ��ֱ�ӷ�����ҳ(Ĭ�ϼ�������8080�˿�).
������Ŀ������ɺ�, ��Ϳ��Դ�VS��Ŀ���������վ��, �����VS��վ */Home/Index*, NodeJS��վ�ͻ�������ӡ����־��Ϣ, ��ʾ������� */Home/Index*. �������� */Home/ThrowException?msg=exmsg*, NodeJS��վ��������ӡ��һ��������־.

## End
�����ͨ��routingKey�����˲�ͬ�������־.

![illustrator](https://raw.githubusercontent.com/Allen-Wei/Alan.Log/master/Alan.Log.RabbitMQ.Example/rabbitmq-log.png)
