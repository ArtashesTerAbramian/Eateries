using Eateries.Application.Features.Eateries.Commands.CreateEatery;
using Eateries.Application.Features.Eateries.Queries.GetEateries;
using Eateries.Application.Features.Eateries.Queries.GetEateryById;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Eateries.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class EateryController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetEateriesWithFilter([FromQuery] GetEateriesQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEateryById(Guid id)
        {
            return Ok(await Mediator.Send(new GetEateryByQueryId { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> AddEatery([FromBody] CreateEateryCommand command)
        {
            var resp = await Mediator.Send(command);
            return CreatedAtAction(nameof(AddEatery), resp);
        }

        [HttpPut("{id}")]
        public void UpdateEatery(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void DeleteEatery(int id)
        {
        }
    }
}
