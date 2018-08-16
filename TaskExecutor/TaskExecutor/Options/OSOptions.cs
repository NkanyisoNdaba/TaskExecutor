using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace TaskExecutor
{
    [Verb("os", HelpText = "Gets The operating system of the current computer")]
    public class OsOptions
    {
        [Usage(ApplicationAlias = "taskexecutor")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Returns The operating system of the current computer", new OsOptions() { });

            }
        }
    }
}