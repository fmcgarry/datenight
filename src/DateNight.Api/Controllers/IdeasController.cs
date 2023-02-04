using DateNight.Api.Data;
using DateNight.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DateNight.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdeasController : ControllerBase
    {
        private readonly IAppLogger<IdeasController> _logger;

        public IdeasController(IAppLogger<IdeasController> logger)
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