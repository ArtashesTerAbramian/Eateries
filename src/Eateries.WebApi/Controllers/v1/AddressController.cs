using Eateries.Application.Features.Addresses.Commands;
using Eateries.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Eateries.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : BaseApiController
    {
        private readonly IAddressRepositoryAsync _addressRepository;

        public AddressController(IAddressRepositoryAsync addressRepository)
        {
            this._addressRepository = addressRepository;
        }
        // GET: api/<AddressController>
/*        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var result = await _addressRepository.GetAllAsync();
            return Ok("");
        }*/

        // GET api/<AddressController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
