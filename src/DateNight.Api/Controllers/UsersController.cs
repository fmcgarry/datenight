using DateNight.Core.Exceptions;
using DateNight.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Security.Claims;
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

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        [Consumes(MediaTypeNames.Text.Plain)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK, MediaTypeNames.Text.Plain)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest, MediaTypeNames.Text.Plain)]
        public async Task<ActionResult<UserRegisterResponse>> DeleteUser(string id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);

                return Ok($"Deleted user '{id}'.");
            }
            catch (UserDoesNotExistException)
            {
                return NotFound("User does not exist.");
            }
        }

        [HttpGet("{id}"), Authorize]
        [ProducesResponseType(typeof(GetUserResponse), StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest, MediaTypeNames.Text.Plain)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound, MediaTypeNames.Text.Plain)]
        public async Task<ActionResult<GetUserResponse>> GetUser(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user is null)
            {
                _logger.LogDebug("Consumer requested user with id '{id}' that does not exist.", id);
                return NotFound($"A user with id '{id}' was not found.");
            }

            var response = new GetUserResponse(user.Name, user.Email);

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

                var response = new UserLoginResponse(token);

                return Ok(response);
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
            try
            {
                var id = await _userService.CreateUserAsync(user.Name, user.Email, user.Password);
                var response = new UserRegisterResponse(user.Name, user.Email, user.Password);

                return Created(id, response);
            }
            catch (UserCreationFailedException)
            {
                return BadRequest("Failed to register user. Please try again.");
            }
            catch (Exception)
            {
                return Problem();
            }
        }

        [HttpPut("{id}"), Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK, MediaTypeNames.Text.Plain)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound, MediaTypeNames.Text.Plain)]
        public async Task<IActionResult> UpdateUser(string id, UserRegisterRequest user)
        {
            try
            {
                string accessingUserId = User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

                if (accessingUserId != id)
                {
                    return Unauthorized("You may only update your own information.");
                }

                await _userService.UpdateUserAsync(id, user.Name, user.Email, user.Password);

                return Ok($"Updated user '{id}'");
            }
            catch (UserDoesNotExistException)
            {
                return NotFound("User does not exist.");
            }
        }
    }
}