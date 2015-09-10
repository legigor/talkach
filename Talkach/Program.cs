using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace Talkach
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("-= Talkach - the chat app for those who respect the Actors Model =-\n\n");

            Console.Write("Your nickname: ");
            var name = Console.ReadLine();

            Console.WriteLine($"Joining the chat as {name} . . .");

            GrainClient.Initialize("ClientConfiguration.xml");
        }
    }
}
