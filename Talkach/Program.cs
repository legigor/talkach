using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Orleans;
using Talkach.Client;
using static System.Threading.Tasks.Task;

namespace Talkach
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("-= Talkach - the chat app for those who respect the Actors Model =-\n\n");
            Console.WriteLine("Press any key to connect to the server . . .");
            Console.ReadKey(false);

            GrainClient.Initialize("ClientConfiguration.xml");

            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            var chat = GrainClient.GrainFactory.GetGrain<IChat>(0);

            var output = await ChatObserverFactory.CreateObjectReference(new ChatOutput());
            await chat.SubscribeForMessages(output);

            var robin = GrainClient.GrainFactory.GetGrain<ITalker>("Robin");
            var bobin = GrainClient.GrainFactory.GetGrain<ITalker>("Bobin");

            WaitAll(
                robin.JoinChat(chat),
                bobin.JoinChat(chat)
            );

            var dice = new Random();


            var cts = new CancellationTokenSource();
            var _ = Task.Factory.StartNew(() =>
                  {
                      while (cts.Token.IsCancellationRequested == false)
                      {
                          var points = dice.Next(0, 2);
                          if (points == 0)
                              robin.StartTheGame().Wait(cts.Token);
                          else
                              bobin.StartTheGame().Wait(cts.Token);

                          Delay(1000, cts.Token).Wait(cts.Token);
                      }

                  }, cts.Token);


            Console.ReadKey(false);
            cts.Cancel();

            Console.WriteLine("BYE!");
            Console.ReadKey(false);
        }

        public class ChatOutput : IChatObserver
        {
            public void AppendMessage(string name, string msg)
            {
                Console.WriteLine(name + ": " + msg);
            }
        }
    }
}
