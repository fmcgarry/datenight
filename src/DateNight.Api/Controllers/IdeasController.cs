using DateNight.Api.Data;
using DateNight.Core.Entities.IdeaAggregate;
using DateNight.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DateNight.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdeasController : ControllerBase
    {
        private readonly IIdeaService _ideaService;
        private readonly IAppLogger<IdeasController> _logger;

        public IdeasController(IAppLogger<IdeasController> logger, IIdeaService ideaService)
        {
            _logger = logger;
            _ideaService = ideaService;
        }

        [HttpPost]
        public async Task<IActionResult> AddIdea(IdeaDto idea)
        {
            var ideaModel = new Idea()
            {
                Title = idea.Title,
                Description = idea.Description
            };

            await _ideaService.AddIdeaAsync(ideaModel);

            return Created(ideaModel.Id!, idea);
        }

        [HttpGet(Name = "all")]
        public IEnumerable<IdeaDto> GetIdeas()
        {
            return Enumerable.Empty<IdeaDto>();
        }
    }
}