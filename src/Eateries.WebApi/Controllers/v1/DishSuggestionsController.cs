using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eateries.Application.Features.DishSuggestion.Queries.GetDishSuggestion;
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
    }
}
