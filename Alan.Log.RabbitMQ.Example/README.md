
# Alan.Log.RabbitMQ
下面以日志模块作为例子介绍一下如何利用RabbitMQ来分发日志. 传统的日志模块都是把日志写到网站所在的服务器上的文件里或者数据库里, 利用RabbitMQ可以开发一个低耦合的分布式日志系统, 可以很简单高效地将日志分发到其他服务器上.

## Install & Introduction
你可以直接把 [ALan.Log](https://github.com/Allen-Wei/Alan.Log) 下载到本地, 然后切换到 [Alan.Log.RabbitMQ.Example](https://github.com/Allen-Wei/Alan.Log/tree/master/Alan.Log.RabbitMQ.Example) 目录. 

目录里有一个 Visual Studio的Web项目文件 **Alan.Log.RabbitMQ.Example.sln**, 这个就是你的网站了, 网站注册了一个 `IHttpModule`类 `LogModule`, 在 `LogModule` 里把每次Web请求和网站异常发布到日志. 在 **Global.asax** 应用启动时注册了两个日志模块, 单文件日志模块和RabbitMQ日志分发模块. 

目录NodeJsExample里有一个nodejs项目, 利用socket.io将VS(Visual Studio)的Web网站分发的日志实时显示在网页上.

VS项目, 你可以直接F5运行测试, 或者发布测试. NodeJsExample 你需要先运行 `npm install` 把所需模块安装了。 

## Configuration
这里主要配置RabbitMQ Server的服务器地址, 认证信息和交换器名字. 
VS项目里的 **Global.asax** 注册RabbitMQ日志模块时, 通过参数指定这些信息. 
NodeJS项目里, 在 **app.js** 里 `amqp.connect("amqp://username:password@IpAddress:port", callback)` 里指定.

## Test
配置完成就可以测试了. 为了安全你最好先启动VS项目, 因为交换器声明是在VS项目里. 如果你先启动NodeJS项目, 而交换器未声明的话会抛异常.
启动好VS项目之后, 就可以启动NodeJS项目了, NodeJS项目你直接访问首页(默认监听的是8080端口).
两个项目启动完成后, 你就可以打开VS项目启动后的网站了, 你访问VS网站 */Home/Index*, NodeJS网站就会立即打印出日志消息, 显示你访问了 */Home/Index*. 如果你访问 */Home/ThrowException?msg=exmsg*, NodeJS网站会立即打印出一个错误日志.

## End
这里图解一下职责规划.
截图里有两个虚拟机，第一个是RabbitMQ服务器(`192.168.121.129`)，第二个运行的是NodeJS站点(`192.168.121.128`)，NodeJS站点订阅RabbitMQ队列，接收日志消息，利用socket.io将日志消息实时显示在网页上。
`http://localhost:60679`是一个ASP.Net网站，在Http Module是捕获了网站异常，并发布到日志，在`Begin_Request`里发布是每次请求日志。其中访问 `/Home/ThrowException` 会手动抛出一个异常消息。
而访问`192.168.121.128:8080`就可以反问NodeJS搭建的网站，然后实时显示 `http:localhost:60679` 发布的日志消息。
你可以通过routingKey来过滤不同级别的日志.

![illustrator](https://raw.githubusercontent.com/Allen-Wei/Alan.Log/master/Alan.Log.RabbitMQ.Example/rabbitmq-log.png)
