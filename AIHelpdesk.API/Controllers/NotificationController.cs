using AIHelpdesk.API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AIHelpdesk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        public readonly Func<string, INotification> _factory;

        public NotificationController(Func<string, INotification> factory)
        {
            _factory = factory;
        }

        [HttpPost("Send/{type}")]
        public IActionResult SendNotification(string type)
        {
            INotification notification = _factory(type);

            string sentMsg = notification.Send();

            return Ok(sentMsg);
        }

    }
}
