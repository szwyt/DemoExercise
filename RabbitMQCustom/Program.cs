using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQCustom
{
    internal class Program
    {
        private static void Main()
        {
            // 建立RabbitMQ连接和通道
            var connectionFactory = new ConnectionFactory
            {
                HostName = "118.24.60.212",
                Port = 5672,
                UserName = "zhdya",
                Password = "123456",
                Protocol = Protocols.AMQP_0_9_1,
                RequestedFrameMax = UInt32.MaxValue,
                RequestedHeartbeat = UInt16.MaxValue
            };

            using (var connection = connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // 这指示通道不预取超过1个消息
                channel.BasicQos(0, 1, false);

                //创建一个新的，持久的交换区
                channel.ExchangeDeclare("SISOExchange", ExchangeType.Direct, true, false, null);
                //创建一个新的，持久的队列
                channel.QueueDeclare("sample-queue", true, false, false, null);
                //绑定队列到交换区
                channel.QueueBind("SISOqueue", "SISOExchange", "optionalRoutingKey");
                using (var subscription = new Subscription(channel, "SISOqueue", false))
                {
                    Console.WriteLine("等待消息...");
                    var encoding = new UTF8Encoding();
                    while (channel.IsOpen)
                    {
                        BasicDeliverEventArgs eventArgs;
                        var success = subscription.Next(2000, out eventArgs);
                        if (success == false) continue;
                        var msgBytes = eventArgs.Body;
                        var message = encoding.GetString(msgBytes);
                        Console.WriteLine(message);
                        channel.BasicAck(eventArgs.DeliveryTag, false);
                    }
                    Console.ReadKey();
                }
            }
        }
    }

}
