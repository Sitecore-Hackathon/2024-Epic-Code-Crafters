using System.Web.Mvc;
using System.Web.Routing;
using Sitecore.Pipelines;


namespace ECCHackaton24.Feature.ImageFinder.Pipelines
{
    public class RegisterWebApiRoutes
    {
        public void Process(PipelineArgs args)
        {
            RouteTable.Routes.MapRoute("imagefinder", "api/{controller}/{action}", new { controller = "Imagefinder", action = "Index" });
        }
    }
}