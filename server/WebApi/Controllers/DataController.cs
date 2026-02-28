
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application;

namespace WebApi;

[ApiController]
[Route("api/[controller]")]
public class DataController(IMediator mediator) : ControllerBase
{  
    [HttpGet]
    public async Task<ActionResult> Get(int sensorId, DateTime start, DateTime end)
    {
        var result = await mediator.Send(new GetSensorDataListQuery(sensorId, start.ToUniversalTime(), end.ToUniversalTime()));
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateSensorDataRequest request)
    {
       var dto = new SensorDataDto
        {
            SensorId = request.SensorId,
            Timestamp = DateTime.UtcNow,
            Value = request.Value
        };
        
        var result = await mediator.Send(new CreateSensorDataCommand(dto));
        return Ok(result);
    }

    [HttpPost("xml")]
    public async Task<ActionResult> PostXml([FromBody] CreateSensorDataRequest request)
    {
       var dto = new SensorDataDto
        {
            SensorId = request.SensorId,
            Timestamp = DateTime.UtcNow,
            Value = request.Value
        };
        
        var result = await mediator.Send(new CreateSensorDataCommand(dto));
        return Ok(result);
    }
}