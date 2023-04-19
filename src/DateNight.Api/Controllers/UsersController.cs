using DateNight.Api.Data;
using DateNight.Api.Mappers;
using DateNight.Core.Exceptions;
using DateNight.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DateNight.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IAppLogger<UsersController> _logger;
        private readonly IUserService _userService;

        public UsersController(IAppLogger<UsersController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            bool isValidGuid = Guid.TryParse(id, out Guid guid);

            if (!isValidGuid)
            {
                _logger.LogDebug("Consumer requested id '{id}' but it was not in the correct format.", id);
                return BadRequest("Value 'id' was not in the correct format.");
            }

            var user = await _userService.GetUserAsync(guid);

            if (user is null)
            {
                _logger.LogDebug("Consumer requested user with id '{id}' that does not exist.", id);
                return NotFound($"A user with id '{id}' was not found.");
            }

            var userDTO = user.MapToDTO();

            return userDTO;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest, "text/plain")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound, "text/plain")]
        [ProducesResponseType(typeof(JsonResult), StatusCodes.Status200OK, "application/json")]
        public async Task<IActionResult> LoginUser(User user)
        {
            try
            {
                var token = await _userService.LoginUserAsync(user.Email, user.Password);

                var result = new
                {
                    token
                };

                return new JsonResult(result);
            }
            catch (UserDoesNotExistException)
            {
                return NotFound("User does not exist.");
            }
            catch (InvalidPasswordException)
            {
                return BadRequest("Invalid password.");
            }
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<User>> RegisterUser(User user)
        {
            var id = await _userService.CreateUserAsync(user.Name, user.Email, user.Password);

            return Created(id, user);
        }
    }
}