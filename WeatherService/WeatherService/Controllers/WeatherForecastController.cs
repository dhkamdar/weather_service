using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace WeatherService.Controllers
{
    [ApiController]
    [Route("weather")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;

        private static readonly Dictionary<int, string> Summaries = new Dictionary<int, string>(){{0,
            "Freezing" }, {4,"Bracing" }, {10,"Chilly" } , {16,"Cool" }, {21,"Mild" }, {27,"Warm" }, {32,"Hot" }, {38,"Sweltering" }, {43,"Scorching" }
        };

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetForecast")]
        public async Task<WeatherForecast> Get(string postalcode)
        {
            WeatherForecast forecast = await WeatherService.Implementations.WeatherForecastImplementation.GetWeatherForecastAsync(postalcode);
            
            //todo: set Summary value on forecast response using Summaries data dictionary
            if(forecast != null && forecast.Temprature != null)
            {
                switch (forecast.Temprature.Celcius)
                {
                    case decimal n when (n <4):
                        forecast.Summary = Summaries[0];
                        break;
                    case decimal n when (n >= 4 && n < 10):
                        forecast.Summary = Summaries[4];
                        break;
                    case decimal n when (n >= 10 && n < 16):
                        forecast.Summary = Summaries[10];
                        break;
                    case decimal n when (n >= 16 && n < 21):
                        forecast.Summary = Summaries[16];
                        break;
                    case decimal n when (n >= 21 && n < 27):
                        forecast.Summary = Summaries[21];
                        break;
                    case decimal n when (n >= 27 && n < 32):
                        forecast.Summary = Summaries[27];
                        break;
                    case decimal n when (n >= 32 && n < 38):
                        forecast.Summary = Summaries[32];
                        break;
                    case decimal n when (n >= 38 && n < 43):
                        forecast.Summary = Summaries[38];
                        break;
                    case decimal n when (n >= 43):
                        forecast.Summary = Summaries[43];
                        break;
                    default:
                        // code block
                        break;
                }
            }

            return forecast;
        }
    }
}