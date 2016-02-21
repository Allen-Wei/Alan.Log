
var app = require("express")(),
	server = require("http").Server(app),
	io = require("socket.io")(server),
	amqp = require("amqplib/callback_api");

server.listen(8080, "192.168.121.128");

app.get("/" , function(req, res){
	res.sendfile(__dirname + "/index.html");
});

var sockets = [];
io.on("connection", function(socket){
	socket.on("client", function(data){
		//client sent message
	});
	console.log("in connection.");

	var key = Date.now;
	sockets[key] = socket;

	socket.on("disconnect", function(){
		delete sockets[key];
	});
});

amqp.connect("amqp://test:test@192.168.121.129", function(connerr, connection){
	connection.createChannel(function(channelerr, channel){
		var queueName = "node-log-rabbitmq";
		channel.assertQueue(queueName, {durable:false});
		channel.bindQueue(queueName, "topic-log-test", "#");
		channel.consume(queueName, function(msg){
			if(msg!= null){
				var logMsg = msg.content.toString();
				channel.ack(msg);
				console.log("log message: ", logMsg);
				io.emit("logs", logMsg);
			}
		});
	});
});
