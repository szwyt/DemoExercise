﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQCustom
{
    internal class Program
    {
        private static void Main()
        {
            // 建立RabbitMQ连接和通道
            var connectionFactory = new ConnectionFactory
            {
                HostName = "192.168.31.100",
                Port = 5672,
                UserName = "rbmqszw",
                Password = "123456",
            };

            using (var connection = connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("kibaQueue", false, false, false, null);

                    /* 这里定义了一个消费者，用于消费服务器接受的消息
                     * C#开发需要注意下这里，在一些非面向对象和面向对象比较差的语言中，是非常重视这种设计模式的。
                     * 比如RabbitMQ使用了生产者与消费者模式，然后很多相关的使用文章都在拿这个生产者和消费者来表述。
                     * 但是，在C#里，生产者与消费者对我们而言，根本算不上一种设计模式，他就是一种最基础的代码编写规则。
                     * 所以，大家不要复杂的名词吓到，其实，并没那么复杂。
                     * 这里，其实就是定义一个EventingBasicConsumer类型的对象，然后该对象有个Received事件，
                     * 该事件会在服务接收到数据时触发。
                     */
                    var consumer = new EventingBasicConsumer(channel);//消费者
                    channel.BasicConsume("kibaQueue", true, consumer);//消费消息
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                    };
                }
            }
        }
    }
}