using System;
using CommandLine;

namespace TaskExecutor
{
    public class OptionsParser
    {
        private readonly IIpAddressProcessor _ipOptions;
        private readonly IOperatingSystemProcessor _operatingSystemProcessor;
        private readonly IHostNameProcessor _hostNameProcessor;
        private readonly IScript _script;

        public OptionsParser(): this(new IpAddressProcessor(), new OperatingSystemProcessor(), new HostNameProcessor(), new Script())
        {
                
        }

        public OptionsParser(IIpAddressProcessor ipOptions, IOperatingSystemProcessor operatingSystemProcessor, IHostNameProcessor hostNameProcessor, IScript script) 
        {
            _ipOptions = ipOptions;
            _operatingSystemProcessor = operatingSystemProcessor;
            _hostNameProcessor = hostNameProcessor;
            _script = script;
        }
        public   int ParseOptionArguments(string[] args)
        {
            return Parser.Default.ParseArguments<HostNameOptions, IpOptions, OsOptions, ScriptOptions>(args)
                .MapResult(
                    (HostNameOptions opts) => RunHostNameAndReturnExitCode(opts),
                    (IpOptions opts) => RunIPAndReturnExitCode(opts),
                    (OsOptions opts) => RunOSAndReturnExitCode(opts),
                    (ScriptOptions opts) => RunScriptAndReturnExitCode(opts),
                    errs => 1);
        }

        private int RunHostNameAndReturnExitCode(HostNameOptions opts)
        {

            var hostName = _hostNameProcessor.GetComputerName(opts.fullyQualified);
            Console.WriteLine(hostName);
            return 0;
        }

        private  int RunIPAndReturnExitCode(IpOptions opts)
        {
            var ipAddress = _ipOptions.GetIPAddress();
            Console.WriteLine(ipAddress);
            return 0;
        }

        private int RunOSAndReturnExitCode(OsOptions opts)
        {
            var operatingSystem = _operatingSystemProcessor.GetOperatingSystem();
            Console.WriteLine(operatingSystem);
            return 0;
        }

        private int RunScriptAndReturnExitCode(ScriptOptions opts)
        {
            _script.RunScript(opts.PowershellScriptOutput);
            return 1;
        }

    }
}
