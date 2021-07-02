using Microsoft.AspNetCore.Mvc;

namespace EdTech.Api.Controllers
{
    public class HomeController : ControllerBaseApi
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Hello, world!";
        }

        [HttpGet("[action]")]
        public ActionResult<string> Privacy()
        {
            return "Your privacy!";
        }
    }
}
