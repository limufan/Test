using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiverConsoleApplication
{
    class ExchangeModel
    {
        public void Declare()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: "logs", type: "fanout");

                    channel.QueueDeclare(queue: "word", durable: false, exclusive: false, autoDelete: false, arguments: null);
                    channel.QueueBind(queue: "word",
                              exchange: "logs",
                              routingKey: "");

                    Console.WriteLine(" [*] Waiting for messages.");

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] Received {0}", message);
                    };

                    channel.BasicConsume(queue: "word", noAck: true, consumer: consumer);

                    Console.WriteLine(" word queue");
                    Console.ReadLine();
                }
            }
        }
    }
}
