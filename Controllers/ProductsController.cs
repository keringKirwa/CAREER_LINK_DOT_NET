using CareerLinkServer.products;
using Microsoft.AspNetCore.Mvc;

namespace CareerLinkServer.Controllers;

/**
 * Note that , the list DataSet<T>  from the  database can be converted to an array using .ToArray() method ,
 * or to a  list using the ToList() method.
 * public WeatherForecastController(ILogger<WeatherForecastController> logger)
 */

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<Product> Get()
    {
        return Enumerable.Range(1, 5).Selec(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
