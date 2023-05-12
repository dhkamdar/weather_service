using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace WeatherService.Implementations
{
    public class WeatherForecastImplementation
    {
        internal static async Task<WeatherForecast> GetWeatherForecastAsync(string postalCode)
        {
            WeatherForecast result = new WeatherForecast();
            result.Temprature = new Temprature();

            //todo: retreive current temperature from:
            var weatherURL = "https://www.weatherbit.io/api/swaggerui/weather-api-v2#!/Current32Weather32Data/get_current_postal_code_postal_code";
            var baseAddress = "https://api.weatherbit.io/v2.0/";
            var API_Key = "1824631bbfa74729aac7d2d2f1dfdd76";

            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(baseAddress);
                    var response = await client.GetAsync($"current?postal_code={postalCode}&key={API_Key}");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawWeather = JsonConvert.DeserializeObject<RawWeather>(stringResult);

                    if (rawWeather != null && rawWeather.data.Count > 0)
                    {
                        decimal c = 0;
                        decimal.TryParse(rawWeather.data[0].temp, out c);

                        result.Date = rawWeather.data[0].datetime;
                        result.Temprature.Celcius = c;
                        result.Temprature.Fahrenheit = (c * 9) / 5 + 32;
                    }
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error while getting result from weatherbit API: {httpRequestException.Message}");
                }
                return result;
            }
        }
        private static WeatherForecast BadRequest(string v)
        {
            throw new NotImplementedException();
        }
    }
}
