using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMessageConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //var factory = new ConnectionFactory() { HostName = "localhost" };
            //using (var connection = factory.CreateConnection())
            //{
            //    connection.ConnectionUnblocked += connection_ConnectionUnblocked;
            //    connection.ConnectionShutdown += connection_ConnectionShutdown;
            //    connection.ConnectionBlocked += connection_ConnectionBlocked;
            //    connection.CallbackException += connection_CallbackException;
            //    while (true)
            //    {
            //        string message = Console.ReadLine();
            //        using (var channel = connection.CreateModel())
            //        {
            //            channel.CallbackException += channel_CallbackException;
            //            channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

            //            var body = Encoding.UTF8.GetBytes(message);

            //            channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);
            //            Console.WriteLine(" [x] Sent {0}", message);
            //        }
            //    }
            //}

            ExchangeModel exchangeModel = new ExchangeModel();
            exchangeModel.Declare();

            Console.ReadLine();
        }

        static void connection_ConnectionUnblocked(object sender, EventArgs e)
        {
            Console.WriteLine("ConnectionShutdown");
        }

        static void connection_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("ConnectionShutdown");
        }

        static void connection_ConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {
            Console.WriteLine("ConnectionBlocked");
        }

        static void channel_CallbackException(object sender, CallbackExceptionEventArgs e)
        {
            Console.WriteLine("channel_CallbackException:" + e.Exception.Message);
        }

        static void connection_CallbackException(object sender, CallbackExceptionEventArgs e)
        {
            Console.WriteLine("connection_CallbackException:" + e.Exception.Message);
        }
    }
}
