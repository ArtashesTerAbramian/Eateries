using Eateries.Application.Features.Addresses.Queries.GetAddresses;
using Eateries.Application.Features.Eateries.Commands;
using Eateries.Application.Features.Eateries.Queries.GetEateries;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Eateries.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class EateryController : BaseApiController
    {
        // GET: api/<EateryController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetEateriesQuery filter)
        {
            return Ok(await Mediator.Send(filter));
        }

        // GET api/<EateryController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<EateryController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateEateryCommand command)
        {
            var resp = await Mediator.Send(command);
            return CreatedAtAction(nameof(Post), resp);
        }

        // PUT api/<EateryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EateryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
