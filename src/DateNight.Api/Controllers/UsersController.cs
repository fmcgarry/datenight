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
    [Authorize]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("{id}/partners")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK, MediaTypeNames.Text.Plain)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest, MediaTypeNames.Text.Plain)]
        public async Task<ActionResult<UserRegisterResponse>> AddUserPartner(string id, [FromForm] string code)
        {
            try
            {
                await _userService.AddUserPartnerAsync(id, code);
                return Ok($"Added partner '{code}' to user '{id}'.");
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

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
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

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetUserResponse), StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest, MediaTypeNames.Text.Plain)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound, MediaTypeNames.Text.Plain)]
        public async Task<ActionResult<GetUserResponse>> GetUser(string id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                var response = new GetUserResponse(user.Id, user.Name, user.Email, user.Partners);

                return Ok(response);
            }
            catch (UserDoesNotExistException)
            {
                return NotFound($"A user with id '{id}' was not found.");
            }
            catch (Exception)
            {
                return Problem();
            }
        }

        [HttpGet("{id}/partners")]
        [ProducesResponseType(typeof(UserRegisterResponse), StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound, MediaTypeNames.Text.Plain)]
        public async Task<ActionResult<UserRegisterResponse>> GetUserPartners(string id)
        {
            try
            {
                var partners = await _userService.GetUserPartners(id);
                return Ok(partners);
            }
            catch (UserDoesNotExistException)
            {
                return NotFound($"A user with id '{id}' was not found.");
            }
            catch (Exception)
            {
                return Problem();
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
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
        [AllowAnonymous]
        [ProducesResponseType(typeof(UserRegisterResponse), StatusCodes.Status201Created, MediaTypeNames.Application.Json)]
        public async Task<ActionResult<UserRegisterResponse>> RegisterUser(UserRegisterRequest user)
        {
            try
            {
                var userId = await _userService.CreateUserAsync(user.Name, user.Email, user.Password);
                var response = new UserRegisterResponse(user.Name, user.Email);

                return Created(userId, response);
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

        [HttpDelete("{id}/partners/{partnerId}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK, MediaTypeNames.Text.Plain)]
        public async Task<ActionResult<UserRegisterResponse>> RemoveUserPartner(string id, string partnerId)
        {
            try
            {
                await _userService.RemoveUserPartner(id, partnerId);

                return Ok($"Removed partner '{partnerId}' from user '{id}'.");
            }
            catch (Exception)
            {
                return Problem();
            }
        }

        [HttpPut("{id}")]
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