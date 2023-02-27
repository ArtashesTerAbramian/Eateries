using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eateries.Application.Features.Ingredient.Commands.CreateIngredient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eateries.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : BaseApiController
    {
        // GET: api/Ingredient
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /*
        // GET: api/Ingredient/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        */

        // POST: api/Ingredient
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateIngredientCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT: api/Ingredient/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Ingredient/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
