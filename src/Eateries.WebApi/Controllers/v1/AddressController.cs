using Eateries.Application.Features.Addresses.Commands.CreateAddress;
using Eateries.Application.Features.Addresses.Queries.GetAddresses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Eateries.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public string Get(Guid id)
        {
            return "value";
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
        public void UpdateAddress(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AddressController>/5
        [HttpDelete("{id}")]
        public void DeleteAddress(int id)
        {
        }
    }
}
