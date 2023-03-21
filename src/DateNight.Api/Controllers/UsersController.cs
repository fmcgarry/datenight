using DateNight.Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace DateNight.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = new User()
            {
                Name = ""
            };

            return Ok(user);
        }
    }
}
