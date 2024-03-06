
using MassTransit;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_PublisherQueue
{

    public class Message : IMessage
    {
        public string Text { get; set; }
    }
    public interface IMessagePublisher<T>
    {
        Task PublishMessageAsync(T message);
    }


    public class RabbitMqMessagePublisher<T> : IMessagePublisher<T> where T : class
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public RabbitMqMessagePublisher(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }
        public async Task PublishMessageAsync(T message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            await _publishEndpoint.Publish<T>(message);
        }
    }
    public class Publisher
    {
        private readonly IMessagePublisher<IMessage> _messagePublisher;

        public Publisher(IMessagePublisher<IMessage> messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }

        public async Task RunPublisherAsync()
        {
            while (true)
            {
                Console.WriteLine("Ödeme günü gelmiştir, ödemenizi yapınız.");
                Message message = new Message
                {
                    Text = "Ödeme günü gelmiştir, ödemenizi yapınız."
                };

                await _messagePublisher.PublishMessageAsync(message);

                Console.WriteLine("Ödeme yapıldı mı? (E/H)");
                string response = Console.ReadLine()?.ToUpper();
                if (response != "E")
                    continue;

                
                break;
            }
        }

    }


        public class Program
        {
            static async Task Main(string[] args)
            {
                string rabbitMqUri = "amqp://stajers:123456@10.10.10.18:5672/";
                string UserName = "stajers";
                string password = "123456";

                var bus = Bus.Factory.CreateUsingRabbitMq(factory =>
                {
                    factory.Host(rabbitMqUri, configurator =>
                    {
                        configurator.Username(UserName);
                        configurator.Password(password);
                    });
                });

                var messagePublisher = new RabbitMqMessagePublisher<IMessage>(bus);
                var publisher = new Publisher(messagePublisher);

                await publisher.RunPublisherAsync();
            }
        }
}
