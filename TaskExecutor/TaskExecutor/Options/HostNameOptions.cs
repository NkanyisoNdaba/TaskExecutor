using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace TaskExecutor
{
     [Verb("hostname", HelpText = "Gets the computer name of current machine.")]
    public class HostNameOptions
    {

        [Option( 'f',"fully-qualified", Default = false, HelpText = "Should Return Fully Qualified Name")]
        public bool fullyQualified { get; set; }

        [Usage(ApplicationAlias = "tasakexecutor")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Returns the computer name of current machine.", new HostNameOptions() { fullyQualified = true });
               
            }
        }
    }
}
