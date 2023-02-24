using Eateries.Application.Features.Cuisines.Queries.GetCuisines;
using Microsoft.AspNetCore.Mvc;

namespace Eateries.WebApi.Controllers.v1
{
    [Route("api/Cuisine")]
    [ApiController]
    public class CuisineController : BaseApiController
    {
        // GET: api/Cuisine
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetCuisinesQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        /*// GET: api/Cuisine/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }*/

        // POST: api/Cuisine
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Cuisine/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Cuisine/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
