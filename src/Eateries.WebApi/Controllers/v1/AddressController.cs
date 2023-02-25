using Eateries.Application.Features.Addresses.Commands.CreateAddress;
using Eateries.Application.Features.Addresses.Commands.DeleteAddressById;
using Eateries.Application.Features.Addresses.Commands.UpdateAddressCommand;
using Eateries.Application.Features.Addresses.Queries.GetAddressById;
using Eateries.Application.Features.Addresses.Queries.GetAddresses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Eateries.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class AddressController : BaseApiController
    {
        // GET: api/<AddressController>
        [HttpGet]
        public async Task<IActionResult> GetAddressesWithFilter([FromQuery] GetAddressQuery filter)
        {
            return Ok(await Mediator.Send(filter));
        }

        // GET api/<AddressController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddressById(Guid id)
        {
            return Ok(await Mediator.Send(new GetAddressById { Id = id }));
        }

        // POST api/<AddressController>
        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] CreateAddressCommand command)
        {
            var resp = await Mediator.Send(command);
            return CreatedAtAction(nameof(CreateAddress), resp);
        }

        // PUT api/<AddressController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(Guid id, [FromBody] UpdateAddressCommand command)
        {
            if (id != command.Id)
                return BadRequest();
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<AddressController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteAddressByIdCommand { Id = id }));
        }
    }
}