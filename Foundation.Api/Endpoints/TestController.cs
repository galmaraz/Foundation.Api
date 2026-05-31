using Microsoft.AspNetCore.Mvc;

namespace Fundation.Api.Endpoints;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { mensaje = "Fundación Jabes API funcionando ✅", fecha = DateTime.Now });
    }
}