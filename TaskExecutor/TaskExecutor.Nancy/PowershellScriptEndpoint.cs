using Nancy;
using Nancy.ModelBinding;
using System;
using TaskExecutor.Nancy.Models;
using TaskExecutorBoundry;

namespace TaskExecutor.Nancy
{
    public class PowershellScriptEndpoint : NancyModule
    {
        public PowershellScriptEndpoint(IPowershellScript PowershellScript)
        {
            Post["/script"] = x =>
            {
                try
                {
                    var scriptContent = this.Bind<ScriptModel>();

                    if (scriptContent.text != "")
                    {
                        var command = scriptContent.text;
                        scriptContent.text = PowershellScript.ExecuteScript(command);
                        if (scriptContent.text.Contains("is not recognized"))
                        {
                            return Negotiate.WithStatusCode(HttpStatusCode.BadRequest)
                                .WithModel(scriptContent);
                        }

                        return Negotiate.WithStatusCode(HttpStatusCode.OK)
                            .WithModel(scriptContent);
                    }

                    return Negotiate.WithStatusCode(HttpStatusCode.BadRequest);

                }
                catch(Exception)
                {
                    return Negotiate.WithStatusCode(HttpStatusCode.InternalServerError);
                }

            };
        }
    }
}
