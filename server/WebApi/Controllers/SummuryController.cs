
using Microsoft.AspNetCore.Mvc;

namespace WebApi;

[ApiController]
[Route("api/sensors/[controller]")]
public class SummuryController : ControllerBase
{  
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Get work");
    }

}