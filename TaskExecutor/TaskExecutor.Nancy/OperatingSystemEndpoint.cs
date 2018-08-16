using Nancy;
using System;
using HttpStatusCode = Nancy.HttpStatusCode;

namespace TaskExecutor.Nancy
{
    public class OperatingSystemEndpoint : NancyModule
    {
        public OperatingSystemEndpoint(IOperatingSystemProcessor operatingSystemProcessor)
        {

            GetOperatingSystem(operatingSystemProcessor);
        }

        public void GetOperatingSystem(IOperatingSystemProcessor operatingSystemProcessor)
        {
            Get["/os"] = parameters =>
            {
                var operatingSystem = operatingSystemProcessor.GetOperatingSystem();
                var operatingSystemOutput = new MachineInformationResults
                {
                    output = operatingSystem
                };
                return Response.AsJson(operatingSystemOutput);
            };
        }
    }
}
