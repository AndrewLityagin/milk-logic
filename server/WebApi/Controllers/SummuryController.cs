
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application;

namespace WebApi;

[ApiController]
[Route("api/sensors/[controller]")]
public class SummuryController(IMediator mediator) : ControllerBase
{  
    [HttpGet]
    public async Task<ActionResult> Get(DateTime start, DateTime end)
    {
        var result = await mediator.Send(new GetSummuryQuery(start.ToUniversalTime(),end.ToUniversalTime()));
        return Ok(result);
    }

}