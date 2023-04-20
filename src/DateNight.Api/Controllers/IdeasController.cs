using DateNight.Api.Data;
using DateNight.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static DateNight.Api.Data.IdeaActions;

namespace DateNight.Api.Controllers
{
    [ApiController, Authorize]
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
        public async Task<IActionResult> AddIdea(AddIdeaRequest idea)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
            {
                return BadRequest();
            }

            var ideaModel = new Core.Entities.IdeaAggregate.Idea()
            {
                Title = idea.Title,
                Description = idea.Description,
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow
            };

            await _ideaService.AddIdeaAsync(ideaModel);

            return Created(ideaModel.Id, idea);
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
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var idea = await _ideaService.GetUserActiveIdeaAsync(userId);

                var ideaModel = new Idea()
                {
                    Id = idea.Id,
                    CreatedOn = idea.CreatedOn,
                    Description = idea.Description,
                    Title = idea.Title,
                    CreatedBy = idea.CreatedBy
                };

                return Ok(ideaModel);
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
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
                CreatedBy = idea.CreatedBy
            };

            return Ok(ideaModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetIdeas()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var ideas = await _ideaService.GetAllUserIdeasAsync(userId);

                var returnedIdeas = new List<Idea>();

                foreach (var idea in ideas)
                {
                    var returnedIdea = new Idea()
                    {
                        Id = idea.Id,
                        Description = idea.Description,
                        Title = idea.Title,
                        CreatedOn = idea.CreatedOn,
                        CreatedBy = idea.CreatedBy
                    };

                    returnedIdeas.Add(returnedIdea);
                }

                return Ok(returnedIdeas);
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
        }

        [HttpGet("random")]
        public async Task<IActionResult> GetRandomIdea()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var idea = await _ideaService.GetRandomUserIdeaAsync(userId);

                var ideaModel = new Idea()
                {
                    Id = idea.Id,
                    CreatedOn = idea.CreatedOn,
                    Description = idea.Description,
                    Title = idea.Title,
                    CreatedBy = idea.CreatedBy
                };

                return Ok(ideaModel);
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
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