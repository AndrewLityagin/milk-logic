
using Microsoft.AspNetCore.Mvc;

namespace WebApi;

[ApiController]
[Route("api/[controller]")]
public class DataController : ControllerBase
{  
    [HttpGet]
    public  IActionResult Get()
    {
        return Ok("Get work");
    }

    [HttpPost]
    public IActionResult Post()
    {
        return Ok("Post work");
    }

}