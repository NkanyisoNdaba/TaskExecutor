using System;

namespace TaskExecutor
{
    public class OperatingSystemProcessor : IOperatingSystemProcessor
    {
        public string GetOperatingSystem()
        {
            return Environment.OSVersion.ToString();
        }
    }
}
