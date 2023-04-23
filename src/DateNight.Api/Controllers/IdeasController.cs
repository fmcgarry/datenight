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

        public IdeasController(IIdeaService ideaService)
        {
            _ideaService = ideaService;
        }

        [HttpPost]
        public async Task<IActionResult> AddIdea(IdeaRequest idea)
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

                var ideaResponse = new IdeaResponse(idea.Id, idea.Title, idea.Description, idea.CreatedBy, idea.CreatedOn);

                return Ok(ideaResponse);
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

            var ideaResponse = new IdeaResponse(idea.Id, idea.Title, idea.Description, idea.CreatedBy, idea.CreatedOn);

            return Ok(ideaResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetIdeas(bool includePartnerIdeas)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var ideas = await _ideaService.GetAllUserIdeasAsync(userId, includePartnerIdeas);

                var ideaResponses = new List<IdeaResponse>();

                foreach (var idea in ideas)
                {
                    var ideaResponse = new IdeaResponse(idea.Id, idea.Title, idea.Description, idea.CreatedBy, idea.CreatedOn);
                    ideaResponses.Add(ideaResponse);
                }

                return Ok(ideaResponses);
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
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var idea = await _ideaService.GetRandomUserIdeaAsync(userId);

                if (idea is null)
                {
                    return NotFound("No ideas were found.");
                }

                var ideaResponse = new IdeaResponse(idea.Id, idea.Title, idea.Description, idea.CreatedBy, idea.CreatedOn);

                return Ok(ideaResponse);
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
        public async Task<IActionResult> UpdateIdea(string id, IdeaRequest idea)
        {
            try
            {
                var ideaModel = await _ideaService.GetIdeaByIdAsync(id);

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