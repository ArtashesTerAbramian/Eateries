using Eateries.Application.Features.Menues.Commands.CreateMenu;
using Eateries.Application.Features.Menues.Queries.GetMenuById;
using Eateries.Application.Features.Menues.Queries.GetMenus;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Eateries.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetMenuesWuthFilter([FromQuery] GetMenuQuery query)
        {
            var resp = await Mediator.Send(query);
            return Ok(resp);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuById(Guid id)
        {
            return Ok(await Mediator.Send(new GetMenuByIdQuery{ Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> AddMenu([FromBody] CreateMenuCommand value)
        {
            var resp = await Mediator.Send(value);
            return CreatedAtAction(nameof(AddMenu), resp);
        }

        /*
        [HttpPut("{id}")]
        public void UpdateMenu(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void DeleteMenu(int id)
        {
        }*/
    }
}
