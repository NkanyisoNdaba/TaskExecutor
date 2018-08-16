using System.Collections.ObjectModel;
using System.IO;
using System.Management.Automation;
using Nancy;
using Nancy.Extensions;
using Nancy.ModelBinding;
using TaskExecutor.Nancy.Models;

namespace TaskExecutor.Nancy
{
    public class ValidPowershellScriptEndpoint: NancyModule
    {

        PowershellScriptModel powershellScript = new PowershellScriptModel();
       
        public ValidPowershellScriptEndpoint(IScript script)
        {
            GetScriptStatusCode();

            Post["/script"] = parameters =>
            {
                var runScript = this.Bind<PowershellScriptModel>();
            };

           
        }

        public void GetScriptStatusCode()
        {
            Post["/script"] = parameters =>
            {
                return Negotiate.WithStatusCode(HttpStatusCode.OK)
                .WithModel(powershellScript);
            };
            
        }

            

        

    }
}
