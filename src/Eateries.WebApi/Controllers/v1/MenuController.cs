﻿using Eateries.Application.Features.Menues.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Eateries.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : BaseApiController
    {
        // GET: api/<MenuController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MenuController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MenuController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateMenuCommand value)
        {
            var resp = await Mediator.Send(value);
            return CreatedAtAction(nameof(Post), resp);
        }

        // PUT api/<MenuController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MenuController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
