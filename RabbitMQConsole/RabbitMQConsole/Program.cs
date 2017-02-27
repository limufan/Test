using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //var factory = new ConnectionFactory() { HostName = "localhost" };
            //using (var connection = factory.CreateConnection())
            //{
            //    connection.CallbackException += connection_CallbackException;
            //    using (var channel = connection.CreateModel())
            //    {
            //        channel.CallbackException += channel_CallbackException;
                    
            //        channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

            //        Console.WriteLine(" [*] Waiting for messages.");

            //        var consumer = new EventingBasicConsumer(channel);
            //        consumer.Received += (model, ea) =>
            //        {
            //            var body = ea.Body;
            //            var message = Encoding.UTF8.GetString(body);
            //            Console.WriteLine(" [x] Received {0}", message);
            //        };
            //        channel.BasicConsume(queue: "hello", noAck: true, consumer: consumer);

            //        Console.WriteLine(" Press [enter] to exit.");
            //        Console.ReadLine();
            //    }
            //}


            ExchangeModel exchangeModel = new ExchangeModel();
            exchangeModel.Declare();

            Console.ReadLine();
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
