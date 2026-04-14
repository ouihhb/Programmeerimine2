using KooliProjekt.Application.Features.ToDoItems;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemsController : ApiControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await Mediator.Send(new GetToDoItemQuery { Id = id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] SaveToDoItemCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(id);
        }
    }
}