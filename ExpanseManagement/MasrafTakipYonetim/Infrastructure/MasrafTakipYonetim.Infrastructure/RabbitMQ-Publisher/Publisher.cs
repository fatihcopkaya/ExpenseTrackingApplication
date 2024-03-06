//using MasrafTakipYonetim.Application.Shared.Shared;
//using MassTransit;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MasrafTakipYonetim.Infrastructure.RabbitMQ_Publisher
//{
//    public class Publisher
//    {
//        public class Message : IMessage
//        {
//            public string Text { get; set; }
//        }
        
//           public static async Task AddQueue()
//            {
//                string rabbitMqUri = "amqp://guest:guest@localhost:5672/";
//                string queue = "test-queue";
//                string UserName = "guest";
//                string password = "guest";

//                var bus = Bus.Factory.CreateUsingRabbitMq(factory =>
//                {
//                    factory.Host(rabbitMqUri, configurator =>
//                    {
//                        configurator.Username(UserName);
//                        configurator.Password(password);
//                    });
//                });

//                //var sendToUri = new Uri($"{rabbitMqUri}/{queue}");
//                //var endPoint = await bus.GetSendEndpoint(sendToUri);

//                await Task.Run(async () =>
//                {
//                    while (true)
//                    {
//                        //Console.Write("Mesaj yaz : ");
//                        Message message = new Message
//                        {
//                            Text = "Ödeme Günü gelmiştir.Lütfen ödemenizi yapınız!"
//                        };
                       
//                        await bus.Publish<IMessage>(message);
//                        //Console.WriteLine("");
//                    }
//                });

//            }
        
//    }
//}
