using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/products?pageNumber=1&pageSize=10
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetProductsQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}

[HttpGet("{id}")]
public async Task<IActionResult> Get(int id)
{
    var result = await Mediator.Send(new GetProductQuery { Id = id });
    return Ok(result);
}

[HttpPost]
public async Task<IActionResult> Save([FromBody] SaveProductCommand command)
{
    var id = await Mediator.Send(command);
    return Ok(id);
}

[HttpDelete("{id}")]
public async Task<IActionResult> Delete(int id)
{
    await Mediator.Send(new DeleteProductCommand { Id = id });
    return Ok();
}
