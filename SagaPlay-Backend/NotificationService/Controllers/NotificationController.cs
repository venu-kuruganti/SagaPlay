using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SagaPlay.Shared.Contracts;
using NotificationService.Utilities;

namespace NotificationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        //The Catalog Service calls this service to send notifications.

        private readonly INotificationDispatcher _dispatcher;

        public NotificationController(INotificationDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] NotificationDTO? message)
        {
            if (message==null)
            {
                return BadRequest("Message cannot be null!");
            }

            await _dispatcher.SendAsync(message);
            return Ok("Notification Sent to all subscribers.");
        }
    }
}
