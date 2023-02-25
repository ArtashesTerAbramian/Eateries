using Eateries.Application.Features.User.Commands.CreateUser;
using Eateries.Application.Features.User.Commands.UpdateUser;
using Eateries.Application.Features.User.Queries.GetUserByIdQuery;
using Eateries.Application.Features.User.Queries.GetUsers;
using Microsoft.AspNetCore.Mvc;

namespace Eateries.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetUsersWithFilter([FromQuery] GetUsersQuery query)
        {
            var res = await Mediator.Send(query);
            return Ok(res);
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            return Ok(await Mediator.Send(new GetUserByIdQuery { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] CreateUserCommand command)
        {
            var resp = await Mediator.Send(command);
            return CreatedAtAction(nameof(AddUser), resp);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserCommand command)
        {
            if (id != command.Id)
                return BadRequest();
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public void DeleteUser(int id)
        {
        }
    }
}
