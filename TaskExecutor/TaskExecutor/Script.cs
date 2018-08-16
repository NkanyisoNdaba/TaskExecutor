using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Management.Automation;
using TaskExecutorBoundry;

namespace TaskExecutor
{
    public class Script : IScript
    {
        public string RunScript(string path)
        {
            using (PowerShell PowerShellInstance = PowerShell.Create())
            {
                PowerShellInstance.AddScript(File.ReadAllText(path));

                Collection<PSObject> PSOutput = PowerShellInstance.Invoke();
                if (PowerShellInstance.Streams.Error.Count > 0)
                {
                   
                    Console.WriteLine("Error");
                     return "Error";
                }

                foreach (PSObject outputItem in PSOutput)
                {
                    if (outputItem != null)
                    {
                        var text = outputItem.ToString();
                        Console.WriteLine(text);
                        return $"{text}";
                       
                    }
                }

                return string.Empty;
            }
        }
    }
}
