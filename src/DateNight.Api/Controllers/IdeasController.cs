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
        public async Task<IActionResult> AddIdea(Data.Idea idea)
        {
            var ideaModel = new Core.Entities.IdeaAggregate.Idea()
            {
                Title = idea.Title,
                Description = idea.Description
            };

            await _ideaService.AddIdeaAsync(ideaModel);

            return Created(ideaModel.Id!, idea);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIdea(string id)
        {
            var ideaModel = await _ideaService.GetIdeaByIdAsync(id);

            if (ideaModel is null)
            {
                return NotFound();
            }

            return Ok(ideaModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetIdeas()
        {
            var ideas = await _ideaService.GetAllIdeasAsync();

            var returnedIdeas = new List<Data.Idea>();

            foreach (var idea in ideas)
            {
                var returnedIdea = new Data.Idea()
                {
                    Description = idea.Description,
                    Title = idea.Title
                };

                returnedIdeas.Add(returnedIdea);
            }

            return Ok(returnedIdeas);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIdea(string id, Data.Idea idea)
        {
            var ideaModel = await _ideaService.GetIdeaByIdAsync(id);

            if (ideaModel is null)
            {
                return NotFound();
            }

            ideaModel.Title = idea.Title;
            ideaModel.Description = idea.Description;

            await _ideaService.UpdateIdeaAsync(ideaModel);
            return NoContent();
        }
    }
}