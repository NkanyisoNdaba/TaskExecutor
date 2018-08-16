using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace TaskExecutor
{
    public class IpAddressProcessor : IIpAddressProcessor
    {
        public IpAddressProcessor() {
            GetIPAddress();
        }

        public string GetIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            var address = host.AddressList.Where(x => x.AddressFamily == AddressFamily.InterNetwork);
            return address.First().ToString();
        }
    }
}
