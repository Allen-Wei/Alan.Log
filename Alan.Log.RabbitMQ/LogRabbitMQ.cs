using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Alan.Log.Core;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Alan.Log.RabbitMQ
{
    public class LogRabbitMQ : ILog
    {

        /// <summary>
        /// RabbitMQ Broker 服务地址
        /// </summary>
        private string _rabbitMqHost;
        /// <summary>
        /// RabbitMQ Broker 端口号
        /// </summary>
        private int _rabbitMqPort;


        /// <summary>
        /// RabbitMQ 用户名
        /// </summary>
        private string _rabbitMqUserName;
        /// <summary>
        /// RabbitMQ 密码
        /// </summary>
        private string _rabbitMqPassword;
        /// <summary>
        /// RabbitMQ 交换器名称
        /// </summary>
        private string _rabbitMqExchange;

        /// <summary>
        /// RabbitMQ 虚机主机名
        /// </summary>
        private string _rabbitMqVirtualHost;

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="host">RabbitMQ Broker 地址</param>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <param name="exchange">交换器名</param>
        public LogRabbitMQ(string host, string userName, string passWord, string exchange)
            : this(host, userName, passWord, exchange, "/", 5672)
        {
        }
   


        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="host">RabbitMQ Broker 地址</param>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <param name="exchange">交换器名</param>
        /// <param name="vhost">虚拟主机名</param>
        public LogRabbitMQ(string host, string userName, string passWord, string exchange, string vhost) :
            this(host, userName, passWord, exchange, vhost, 5672)
        {
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="host">RabbitMQ Broker 地址</param>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <param name="exchange">交换器名</param>
        /// <param name="vhost">虚拟主机名</param>
        /// <param name="port">RabbitMQ Broker端口号</param>
        public LogRabbitMQ(string host, string userName, string passWord, string exchange, string vhost, int port)
        {
            this._rabbitMqHost = host;
            this._rabbitMqUserName = userName;
            this._rabbitMqPassword = passWord;
            this._rabbitMqExchange = exchange;

            this._rabbitMqVirtualHost = vhost;
            this._rabbitMqPort = port;

            var channel = Channel;
            channel.ExchangeDeclare(exchange: this._rabbitMqExchange, type: "topic", durable: false, autoDelete: false, arguments: null);
        }


        private IModel _internalChannel;
        private IModel Channel
        {
            get
            {
                if (this._internalChannel != null && this._internalChannel.IsOpen) return this._internalChannel;

                var factory = new ConnectionFactory()
                {
                    HostName = this._rabbitMqHost,
                    UserName = this._rabbitMqUserName,
                    Password = this._rabbitMqPassword,
                    VirtualHost = this._rabbitMqVirtualHost
                };

                var connection = factory.CreateConnection();
                this._internalChannel = connection.CreateModel();

                return this._internalChannel;
            }
        }


        /// <summary>
        /// 输入日志
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="date">日期</param>
        /// <param name="level">级别</param>
        /// <param name="logger">发布者</param>
        /// <param name="category">分类(分类必须为字母, 且不能为空)</param>
        /// <param name="message">消息内容</param>
        /// <param name="note">备注</param>
        /// <param name="request">请求内容</param>
        /// <param name="response">响应内容</param>
        /// <param name="position">位置</param>
        public void Write(string id,
            DateTime date,
            string level,
            string logger,
            string category,
            string message,
            string note,
            string request,
            string response,
            string position)
        {
            if (String.IsNullOrWhiteSpace(category) || !Regex.IsMatch(category, @"(\w|\-)+")) category = "all";
            if (date == default(DateTime)) date = DateTime.Now;


            var json = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                Id = id,
                Date = date,
                Level = level,
                Logger = logger,
                Category = category,
                Message = message,
                Note = note,
                Request = request,
                Response = response,
                Position = position
            });

            this.Channel.BasicPublish(exchange: this._rabbitMqExchange,
                routingKey: String.Format("{0}.{1}", category, level),
                basicProperties: null,
                body: Encoding.UTF8.GetBytes(json));

        }
    }
}
