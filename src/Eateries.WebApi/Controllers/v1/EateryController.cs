using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Eateries.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class EateryController : ControllerBase
    {
        // GET: api/<EateryController>
      /*  [HttpGet]
        public async IEnumerable<string> Get()
        {
            return Ok(await )
        }*/

        // GET api/<EateryController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<EateryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
