using System.Threading.Tasks;
using KooliProjekt.Application.Features.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetOrderQuery { Id = id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] SaveOrderCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteOrderCommand { Id = id });
            return Ok();
        }
    }
}