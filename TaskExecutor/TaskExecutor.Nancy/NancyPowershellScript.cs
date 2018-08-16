using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Management.Automation;
using TaskExecutorBoundry;

namespace TaskExecutor.Nancy
{
    public class NancyPowershellScript :IPowershellScript
    {
        public int RunAndReturnScriptOutput(ScriptOptions opts)
        {
            var command = ReadScript(opts.PowershellScriptOutput);
            Console.WriteLine(ExecuteScript(command));
            return Environment.ExitCode;
        }

        public string ExecuteScript(string command)
        {
            string results = string.Empty;
            using (PowerShell PowerShellInstance = PowerShell.Create())
            {
                PowerShellInstance.AddScript(command);
                Collection<PSObject> PSOutput = PowerShellInstance.Invoke();
                var errorChecker = CheckErrors(PowerShellInstance);
                if (errorChecker != null && !errorChecker.Equals(""))
                {
                    return errorChecker;
                }
                results = GetScriptBody(results, PSOutput);
            }

            return results;
        }

        public string ReadScript(string path)
        {
            return File.ReadAllText(path);
        }

        private static string CheckErrors(PowerShell PowerShellInstance)
        {
            var errorResult = string.Empty;
            Collection<ErrorRecord> errorList = PowerShellInstance.Streams.Error.ReadAll();
            if (errorList != null && errorList.Count > 0)
            {
                foreach (ErrorRecord error in errorList)
                {
                    errorResult += error.Exception.Message;
                }
            }

            return errorResult;
        }

        private static string GetScriptBody(string results, Collection<PSObject> PSOutput)
        {
            foreach (PSObject outputItem in PSOutput)
            {
                results = AddItemsToList(results, outputItem);
            }
            return results;
        }

        private static string AddItemsToList(string results, PSObject outputItem)
        {
            if (outputItem != null)
            {
                results = outputItem.ToString();
            }
            return results;
        }

    }

}
