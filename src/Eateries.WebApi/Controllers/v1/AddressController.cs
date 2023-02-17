using Eateries.Application.Features.Addresses.Commands;
using Eateries.Application.Features.Addresses.Queries.GetAddressById;
using Eateries.Application.Features.Addresses.Queries.GetAddresses;
using Eateries.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Eateries.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : BaseApiController
    {
        // GET: api/<AddressController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAddressQuery filter)
        {
            return Ok(await Mediator.Send(filter));
        }

        // GET api/<AddressController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetAddressByIdQuery{Id = id}));
        }

        // POST api/<AddressController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateAddressCommand command)
        {
            var resp = await Mediator.Send(command);
            return CreatedAtAction(nameof(Post), resp);
        }

        // PUT api/<AddressController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AddressController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
