using System;
using System.Net;
using Orleans.Runtime.Host;

namespace Talkach.Server
{
    class Program
    {
        static SiloHost _siloHost;

        static void Main()
        {
            // Orleans should run in its own AppDomain, we set it up like this
            var hostDomain = AppDomain.CreateDomain("OrleansHost", null, new AppDomainSetup
            {
                AppDomainInitializer = InitSilo
            });

            Console.WriteLine("Orleans Silo is running.\nPress Enter to terminate...");
            Console.ReadLine();

            // We do a clean shutdown in the other AppDomain
            hostDomain.DoCallBack(ShutdownSilo);
        }

        static void InitSilo(string[] args)
        {
            _siloHost = new SiloHost(Dns.GetHostName())
            {
                ConfigFileName = "ServerConfiguration.xml"
            };

            _siloHost.InitializeOrleansSilo();


            var startedOk = _siloHost.StartOrleansSilo();

            if (!startedOk)
                throw new SystemException($"Failed to start Orleans silo '{_siloHost.Name}' as a {_siloHost.Type} node");
        }

        static void ShutdownSilo()
        {
            if (_siloHost == null)
                return;

            _siloHost.Dispose();
            GC.SuppressFinalize(_siloHost);
            _siloHost = null;
        }
    }
}
