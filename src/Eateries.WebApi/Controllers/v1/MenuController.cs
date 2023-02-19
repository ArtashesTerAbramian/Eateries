using Eateries.Application.Features.Menues.Commands;
using Eateries.Application.Features.Menues.Commands.CreateMenu;
using Eateries.Application.Features.Menues.Queries;
using Eateries.Application.Features.Menues.Queries.GetMenuById;
using Eateries.Application.Features.Menues.Queries.GetMenus;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Eateries.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : BaseApiController
    {
        // GET: api/<MenuController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetMenuQuery query)
        {
            var resp = await Mediator.Send(query);
            return Ok(resp);
        }

        // GET api/<MenuController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetMenuByIdQuery{ Id = id }));
        }

        // POST api/<MenuController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateMenuCommand value)
        {
            var resp = await Mediator.Send(value);
            return CreatedAtAction(nameof(Post), resp);
        }

        // PUT api/<MenuController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MenuController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
