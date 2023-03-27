using Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSVReader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        ICsvService _service;
        ILogger<UsersController> _logger;

        public UsersController(ICsvService service, ILogger<UsersController> logger)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        [EnableCors("Policy1")]
        [Route("check")]
        public ActionResult Check([FromForm] string? userId)
        {
            _logger.LogInformation($"Received a new request to verify the user userId={userId}");
            var user = _service.GetOrCreateUser(userId);
            return Ok(user);
        }
    }
}
