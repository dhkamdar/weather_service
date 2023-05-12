namespace WeatherService
{
    public class WeatherForecast
    {
        public string Date { get; set; }
        public Temprature Temprature { get; set; }
        public string Summary { get; set; }
    }

    public class RawWeather
    {
        public int Count { get; set; }
        public List<data> data { get; set; }
    }

    public class data
    {
        public string datetime { get; set; }
        public string temp { get; set; }
        public string app_temp { get; set; }
    }

    public class Temprature
    {
        public decimal Celcius { get; set; }
        public decimal Fahrenheit { get; set; }
    }
}