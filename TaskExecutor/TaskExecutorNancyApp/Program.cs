using System;
using Nancy.Hosting.Self;
using Nancy.TinyIoc;
using TaskExecutor;
using TaskExecutor.Nancy;
using TaskExecutorBoundry;


namespace TaskExecutorNancyApp
{
    class Program
    {
        public static int Main(string[] args)
        {
            HostConfiguration hostConfigs = new HostConfiguration();
            hostConfigs.UrlReservations.CreateAutomatically = true;
            RegisterContainer();

            Uri uri = new Uri("http://localhost/api/");
            var host = new NancyHost(hostConfigs, uri);

            host.Start();
            Console.ReadKey();

            var parser = new OptionsParser();
            return parser.ParseOptionArguments(args);
        }

        private static void RegisterContainer()
        {
            TinyIoCContainer.Current.Register<HealthEndpoint>();
            TinyIoCContainer.Current.Register<HostNameEndpoint>();
            TinyIoCContainer.Current.Register<IpEndpoint>();
            TinyIoCContainer.Current.Register<OperatingSystemEndpoint>();
            TinyIoCContainer.Current.Register<PowershellScriptEndpoint>();
            TinyIoCContainer.Current.Register<IIpAddressProcessor>(new IpAddressProcessor());
            TinyIoCContainer.Current.Register<IHostNameProcessor>(new HostNameProcessor());
            TinyIoCContainer.Current.Register<IOperatingSystemProcessor>(new OperatingSystemProcessor());
            TinyIoCContainer.Current.Register<IScript>(new Script());
            TinyIoCContainer.Current.Register<IPowershellScript>(new NancyPowershellScript());
        }
    }
}
