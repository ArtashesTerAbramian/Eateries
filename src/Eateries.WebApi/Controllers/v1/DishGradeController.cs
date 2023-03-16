using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eateries.Application.Features.DishGrade.Commands.CreateDishGrade;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eateries.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishGradeController : BaseApiController
    {
        /*// GET: api/DishGrade
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/DishGrade/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/DishGrade
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }*/

        // PUT: api/DishGrade/5
        [HttpPut]
        public async Task<IActionResult> UpdateDishGrade([FromBody] UpdateDishGradeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /*
        // DELETE: api/DishGrade/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
