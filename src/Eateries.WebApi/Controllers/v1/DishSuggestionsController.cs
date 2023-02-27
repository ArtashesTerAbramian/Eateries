using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eateries.Application.Features.DishSuggestion.GetDishSuggestion;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eateries.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishSuggestionsController : BaseApiController
    {
        // GET: api/DishSuggestions
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetDishSuggestion command)
        {
            return Ok(await Mediator.Send(command));
        }

        /*// GET: api/DishSuggestions/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }*/

        // POST: api/DishSuggestions
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/DishSuggestions/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/DishSuggestions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
