using DateNight.Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace DateNight.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdeasController : ControllerBase
    {
        private readonly ILogger<IdeasController> _logger;

        public IdeasController(ILogger<IdeasController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "all")]
        public IEnumerable<IdeaDto> GetIdeas()
        {
            return Enumerable.Empty<IdeaDto>();
        }
    }
}