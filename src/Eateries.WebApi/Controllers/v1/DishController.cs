using Eateries.Application.Features.Dishes.Commands.CreateDish;
using Eateries.Application.Features.Dishes.Queries.GetDishById;
using Eateries.Application.Features.Dishes.Queries.GetDishes;
using Microsoft.AspNetCore.Mvc;

namespace Eateries.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : BaseApiController
    {
        // GET: api/Dish
        [HttpGet]
        public async Task<IActionResult> GetDishesByFilters([FromQuery] GetDishesQuery filter)
        {
            return Ok(await Mediator.Send(filter));
        }

        // GET: api/Dish/5
        [HttpGet("{id}", Name = "GetDishById")]
        public async Task<IActionResult> GetDishById(Guid id)
        {
            return Ok(await Mediator.Send(new GetDishByIdQuery{ Id = id}));
        }

        // POST: api/Dish
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateDishCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /*// PUT: api/Dish/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Dish/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
