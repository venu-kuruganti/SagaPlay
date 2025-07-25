using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers
{
    public class ResponseController : Controller
    {
        public IActionResult SendResponse()
        {
                        
            return Ok();
        }
    }
}
