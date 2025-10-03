using Microsoft.AspNetCore.Mvc;
using LMCalc.Api.Models;

namespace BackendDotnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet]
    public IEnumerable<Weather> Get()
    {
        return new[]
            {
                new Weather { City = "LA", Temp = 27 },
                new Weather { City = "NYC", Temp = 18 },
                new Weather { City = "Chicago", Temp = 15 }
            };
    }
}
