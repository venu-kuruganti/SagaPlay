using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers
{
    public class RequestController : Controller
    {
        public IActionResult GetRequest()
        {
            return Ok();
        }
    }
}
