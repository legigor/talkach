using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;
using Talkach.Client;

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
            Console.Write("Your nickname: ");
            var name = Console.ReadLine();

            Console.WriteLine($"Joining the chat as {name} . . .");
            
            var talker = GrainClient.GrainFactory.GetGrain<ITalker>(name);
            var chat = GrainClient.GrainFactory.GetGrain<IChat>(0);

            try
            {
                await chat.Register(talker);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
