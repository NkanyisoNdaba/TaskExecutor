using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace TaskExecutor
{
    [Verb("ip", HelpText = "Gets IP Adddress of the current computer.")]
    public class IpOptions
    {
        [Usage(ApplicationAlias = "taskexecutor")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Returns  IP Adddress of the current computer.", new IpOptions() { });

            }
        }
    }
}