using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eateries.Application.Features.Orders.Commands.CreateOrder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eateries.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseApiController
    {
        // GET: api/Order
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /*
        // GET: api/Order/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        */

        // POST: api/Order
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT: api/Order/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
