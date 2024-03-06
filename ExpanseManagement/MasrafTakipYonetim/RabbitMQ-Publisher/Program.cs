using MassTransit;
using Microsoft.Extensions.Configuration;
using RabbitMQ_Publisher.Shared;
 

namespace MasrafTakipYonetim.Infrastructure.RabbitMQ_Publisher
{
    public class Message : IMessage
    {
        public string Text { get; set; }
    }
    public class Program
    {

        static async Task Main(string[] args)
        {
            string rabbitMqUri = "amqp://guest:guest@localhost:5672/";
            string queue = "test-queue";
            string UserName = "guest";
            string password = "guest";

            var bus = Bus.Factory.CreateUsingRabbitMq(factory =>
            {
                factory.Host(rabbitMqUri, configurator =>
                {
                    configurator.Username(UserName);
                    configurator.Password(password);
                });
            });

            //var sendToUri = new Uri($"{rabbitMqUri}/{queue}");
            //var endPoint = await bus.GetSendEndpoint(sendToUri);

            string defaultMessage = "Ödeme Günü gelmiştir.Lütfen ödemenizi yapınız!";
            await Task.Run(async () =>
            {
                while (true)
                {
                    Message message = new Message
                    {
                        Text = defaultMessage
                    };

                    await bus.Publish<IMessage>(message);
                    Console.WriteLine("Varsayılan mesaj gönderildi.");

                    // Belirli bir süre beklemek için
                    await Task.Delay(TimeSpan.FromSeconds(5)); // Örnek olarak 5 saniye bekleniyor, süreyi ihtiyaca göre ayarlayabilirsiniz
                }
            });
        }

    }
}

