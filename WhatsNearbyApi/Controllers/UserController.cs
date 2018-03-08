using WhatsNearbyApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WhatsNearbyApi.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    [Authorize]
    public class UserController : Controller
    {
        private ILogger<UserController> _logger;
        private IUserRepository _userRepository;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpGet]
        [Authorize(Policy = "SuperUsers")]
        public IActionResult Get()
        {
            _logger.LogInformation("Start: Get Users");
            
            var users = _userRepository.GetUsers();

            _logger.LogInformation("End: Get Users");

            return Ok(users);
        }

        [HttpGet]
        [Route("By")]
        public IActionResult GetByUsername(string username)
        {
            _logger.LogInformation("Start: Get User by username");

            var user = _userRepository.GetUserByUsername(username);

            _logger.LogInformation("End: Get User by username");

            return Ok(user);
        }
    }
}