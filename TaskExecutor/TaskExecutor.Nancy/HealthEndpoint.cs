using Nancy;

namespace TaskExecutor.Nancy
{
    public class HealthEndpoint : NancyModule
    { 
        public HealthEndpoint()
        {
            CheckHealth();
        }
        public void CheckHealth()
        {
            Get["/health"] = _ =>
            {
                return Negotiate.WithStatusCode(HttpStatusCode.OK);
            };

        }
    }
}
