using DateNight.Core.Exceptions;
using DateNight.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using static DateNight.Api.Data.UserActions;

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
        [ProducesResponseType(typeof(GetUserResponse), StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest, MediaTypeNames.Text.Plain)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound, MediaTypeNames.Text.Plain)]
        public async Task<ActionResult<GetUserResponse>> GetUser(string id)
        {
            bool isValidGuid = Guid.TryParse(id, out Guid guid);

            if (!isValidGuid)
            {
                _logger.LogDebug("Consumer requested id '{id}' but it was not in the correct format.", id);
                return BadRequest("Value 'id' was not in the correct format.");
            }

            var user = await _userService.GetUserByIdAsync(guid);

            if (user is null)
            {
                _logger.LogDebug("Consumer requested user with id '{id}' that does not exist.", id);
                return NotFound($"A user with id '{id}' was not found.");
            }

            var response = new GetUserResponse(user.Name);

            return Ok(response);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(UserLoginResponse), StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest, MediaTypeNames.Text.Plain)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound, MediaTypeNames.Text.Plain)]
        public async Task<ActionResult<UserLoginResponse>> LoginUser(UserLoginRequest user)
        {
            try
            {
                var token = await _userService.LoginUserAsync(user.Email, user.Password);

                var result = new
                {
                    token
                };

                var response = new UserLoginResponse(token);

                return Ok(result);
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
        [ProducesResponseType(typeof(UserRegisterResponse), StatusCodes.Status201Created, MediaTypeNames.Application.Json)]
        public async Task<ActionResult<UserRegisterResponse>> RegisterUser(UserRegisterRequest user)
        {
            var id = await _userService.CreateUserAsync(user.Name, user.Email, user.Password);

            var reponse = new UserRegisterResponse(user.Name, user.Email, user.Password);

            return Created(id, reponse);
        }
    }
}