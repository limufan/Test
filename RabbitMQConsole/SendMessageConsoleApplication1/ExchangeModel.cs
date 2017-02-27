using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMessageConsoleApplication1
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
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    while (true)
                    {
                        string message = Console.ReadLine();
                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(exchange: "logs", routingKey: "", basicProperties: properties, body: body);
                        Console.WriteLine(" [x] Sent {0}", message);
                    }
                }
            }
        }
    }
}
