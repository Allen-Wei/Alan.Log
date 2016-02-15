using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alan.Log.Core;
using Alan.Log.Implement;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Alan.Log.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            LogUtils.Current.InjectLogModule<LogTraceWrite>()
                .InjectLogModule<LogSingleFile>()
                .InjectLogModuleAppendConfig<LogSingleFile>().Config(@"E:\Temporary\SingleFile.txt");

            LogUtils.Current.InjectLogModuleAppendConfig<LogAutoSeperateFiles>().Config(10 * 1024, @"E:\Temporary", "MultiFile");

            var factory = new ConnectionFactory()
            {
                HostName = "219.143.6.72",
                UserName = "android",
                Password = "android@2016"
            };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var queue = channel.QueueDeclare(queue: "log-queue", durable: false, exclusive: false,
                        autoDelete: false, arguments: null);

                    channel.QueueBind(queue: queue.QueueName, exchange: "amq.rabbitmq.log", routingKey: "*");
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var text = Encoding.UTF8.GetString(body);



                        LogUtils.Current.Log(new Models.Log
                        {
                            Level = Models.Log.LogLevel.Info,
                            Date = DateTime.Now.AddDays(50),
                            Message = ea.RoutingKey,
                            Note = text
                        });



                    };


                    channel.BasicConsume(queue: queue.QueueName, noAck: false, consumer: consumer);
                    Console.ReadKey();
                }
            }

        }
    }
}
