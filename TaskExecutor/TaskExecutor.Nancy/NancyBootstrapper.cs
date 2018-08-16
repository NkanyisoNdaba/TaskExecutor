using Nancy;
using Nancy.Bootstrapper;
using System;
using System.Text;

namespace TaskExecutor.Nancy
{
    public class NancyBootstrapper : IApplicationStartup
    {
        public void Initialize(IPipelines pipelines)
        {
            pipelines.OnError += PipelinesOnError();
        }

        private Func<NancyContext, Exception, dynamic> PipelinesOnError()
        {
            return (ctx, ex) =>
            {
                return new Response
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Contents = stream => stream.Write(Encoding.UTF8.GetBytes(ex.Message), 0,
                        ex.Message.Length)
                };
            };
        }

    }
}
