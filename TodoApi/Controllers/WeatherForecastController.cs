using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WeatherForecastController : ControllerBase
{
    private WeatherForecast[] _wheatherForecast = new WeatherForecast[]
    {
        new WeatherForecast(1, DateOnly.FromDateTime(DateTime.Now), 25, "Sunny"),
        new WeatherForecast(2, DateOnly.FromDateTime(DateTime.Now.AddDays(1)), 30, "Cloudy"),
        new WeatherForecast(3, DateOnly.FromDateTime(DateTime.Now.AddDays(2)), 20, "Rainy")
    };

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        return _wheatherForecast;
    }

    [HttpGet("{id}")]
    public ActionResult<WeatherForecast> GetById(int id)
    {
        var weatherForecast = _wheatherForecast.FirstOrDefault(a => a.Id == id);
        if (weatherForecast == null)
        {
            return NotFound($"No forecast found for id {id}");
        }
        else
        {
            return weatherForecast;
        }
    }

    [HttpPost]
    public ActionResult<WeatherForecast> Post([FromBody] WeatherForecast weatherForecast)
    {
        if (weatherForecast == null)
        {
            return BadRequest("Weather forecast cannot be null");
        }

        // Simulate adding to a database by generating a new Id
        var newId = _wheatherForecast.Max(w => w.Id) + 1;
        var newWeatherForecast = new WeatherForecast(newId, weatherForecast.Date, weatherForecast.TemperatureC, weatherForecast.Summary);

        // Add the new forecast to the array (in a real application, you would add it to a database)
        _wheatherForecast = _wheatherForecast.Append(newWeatherForecast).ToArray();
        return CreatedAtAction(nameof(GetById), new { id = newId }, newWeatherForecast);
    }

    //TODO:delete weatherForecast + update weatherForecast
}
