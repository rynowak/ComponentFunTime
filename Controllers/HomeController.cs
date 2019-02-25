using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ComponentFunTime.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult About([FromServices] EndpointDataSource dataSource)
        {
            var endpoints = dataSource.Endpoints;
            return Content("Hey there");
        }
    }
}
