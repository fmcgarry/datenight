using DateNight.Api.Data;
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
        public async Task<IActionResult> AddIdea(Idea idea)
        {
            var ideaModel = new Core.Entities.IdeaAggregate.Idea()
            {
                Title = idea.Title,
                Description = idea.Description
            };

            await _ideaService.AddIdeaAsync(ideaModel);

            return Created(ideaModel.Id!, idea);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIdea(string id)
        {
            try
            {
                await _ideaService.DeleteIdeaAsync(id);
                return NoContent();
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveIdea()
        {
            try
            {
                var idea = await _ideaService.GetActiveIdeaAsync();

                var ideaModel = new Idea()
                {
                    Id = idea.Id,
                    CreatedOn = idea.CreatedOn,
                    Description = idea.Description,
                    Title = idea.Title,
                };

                return Ok(ideaModel);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIdea(string id)
        {
            var idea = await _ideaService.GetIdeaByIdAsync(id);

            if (idea is null)
            {
                return NotFound();
            }

            var ideaModel = new Idea()
            {
                CreatedOn = idea.CreatedOn,
                Description = idea.Description,
                Title = idea.Title,
            };

            return Ok(ideaModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetIdeas()
        {
            var ideas = await _ideaService.GetAllIdeasAsync();

            var returnedIdeas = new List<Idea>();

            foreach (var idea in ideas)
            {
                var returnedIdea = new Idea()
                {
                    Id = idea.Id,
                    Description = idea.Description,
                    Title = idea.Title,
                    CreatedOn = idea.CreatedOn
                };

                returnedIdeas.Add(returnedIdea);
            }

            return Ok(returnedIdeas);
        }

        [HttpGet("random")]
        public async Task<IActionResult> GetRandomIdea()
        {
            var idea = await _ideaService.GetRandomIdeaAsync();

            var ideaModel = new Idea()
            {
                Id = idea.Id,
                CreatedOn = idea.CreatedOn,
                Description = idea.Description,
                Title = idea.Title,
            };

            return Ok(ideaModel);
        }

        [HttpPost("active")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> SetActiveIdea([FromForm] string id)
        {
            try
            {
                await _ideaService.ActivateIdea(id);
                return NoContent();
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIdea(string id, Idea idea)
        {
            try
            {
                var ideaModel = await _ideaService.GetIdeaByIdAsync(id);

                ideaModel.Id = idea.Id;
                ideaModel.CreatedOn = idea.CreatedOn;
                ideaModel.Title = idea.Title;
                ideaModel.Description = idea.Description;

                await _ideaService.UpdateIdeaAsync(ideaModel);

                return NoContent();
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
        }
    }
}