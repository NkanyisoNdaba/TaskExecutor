using System.Net;

namespace TaskExecutor
{
    public class HostNameProcessor : IHostNameProcessor
    {
        public string GetComputerName(bool fullyqulified)
        {
            if (fullyqulified)
            {
                return GetFullyQualifiedHostName();
            }
            return GetHostName();
        }
        public string GetHostName()
        {

            return Dns.GetHostName();
        }

        public string GetFullyQualifiedHostName()
        {
            var hostName = GetHostName();
            var hostEntry = Dns.GetHostEntry(hostName);
            return hostEntry.HostName;
        }
    }
}
