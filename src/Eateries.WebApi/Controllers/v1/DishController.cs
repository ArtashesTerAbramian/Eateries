using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eateries.Application.Features.Dishes.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eateries.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : BaseApiController
    {
        // GET: api/Dish
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /*// GET: api/Dish/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }*/

        // POST: api/Dish
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateDishCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT: api/Dish/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Dish/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
