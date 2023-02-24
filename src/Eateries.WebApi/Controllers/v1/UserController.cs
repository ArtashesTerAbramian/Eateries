using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eateries.Application.Features.User.Commands.CreateUser;
using Eateries.Application.Features.User.Queries.GetUsers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eateries.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetUsersQuery query)
        {
            var res = await Mediator.Send(query);
            return Ok(res);
        }

        /*[HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }*/

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
        {
            var resp = await Mediator.Send(command);
            return CreatedAtAction(nameof(Post), resp);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
