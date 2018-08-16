using Nancy;
using System;
using HttpStatusCode = Nancy.HttpStatusCode;

namespace TaskExecutor.Nancy
{
    public class IpEndpoint : NancyModule
    {
        public IpEndpoint(IIpAddressProcessor ipAddressProcessor)
        {
            GetIPEndpoint(ipAddressProcessor);
        }

        public void GetIPEndpoint(IIpAddressProcessor ipAddressProcessor)
        {
            Get["/ip"] = parameters =>
            {
                var ipAddress = ipAddressProcessor.GetIPAddress();
                var ipAdressOutput = new MachineInformationResults
                {
                    output = ipAddress
                };
                return Response.AsJson(ipAdressOutput);

            };
        }
    }
}
