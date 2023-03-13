using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace Weather
{
    public class WeatherForecast
    {
        public city city { get; set; }
        public List<list> list { get; set; }
    }
    public class city
    {
        public string name { get; set; }
    }
    public class list
    {
        public double dt { get; set; }
        public double pressure { get; set; }
        public int humidity { get; set; }
        public double speed { get; set; }
        public temp temp { get; set; }
        public List<weather> weather { get; set; }
    }
    public class weather
    {
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }
    public class temp
    {
        public double day { get; set; }
        public double min { get; set; }
        public double max { get; set; }
    }


    internal class DBConnetion : IDBConnection
    {
        private const string token = "9e806ad68210ebdcb7f17fef52a41dfa";
        private const string webApi = "http://api.openweathermap.org/data/2.5/weather?q=";
        private const string webApiv2 = "http://api.openweathermap.org/data/2.5/forecast?q=";
        public const string webApiv3 = "http://api.openweathermap.org/data/2.5/uvi?lat=";

        public WeatherInfo GetData(string city)
        {
            string resultApi;
            WeatherInfo weatherInfo = new WeatherInfo();
            WebClient client = new WebClient();
            try
            {
                resultApi = client.DownloadString($"{webApi}{city}&appid={token}");
                dynamic json = JsonConvert.DeserializeObject(resultApi);

                weatherInfo.Name = json.name;
                weatherInfo.TempMin = json.main["temp_min"] - 273;
                weatherInfo.TempMax = json.main["temp_max"] - 273;
                weatherInfo.WeatherType = json.weather[0]["main"];
                weatherInfo.WeatherDescription = json.weather[0]["description"];
                weatherInfo.WindSpeed = json.wind["speed"];
                weatherInfo.Visibillity = json["visibility"];
                weatherInfo.Humidy = json.main["humidity"];
                weatherInfo.SunRise = json.sys["sunrise"];
                weatherInfo.SunSet = json.sys["sunset"];
                weatherInfo.Icon = json.weather[0]["icon"];
                weatherInfo.Pressure = json.main["pressure"];
                weatherInfo.CountryIndex = json.sys["country"];
                weatherInfo.CloudsAll = json.clouds["all"];
                weatherInfo.Lat = json.coord["lat"];
                weatherInfo.Lon = json.coord["lon"];
            }
            catch
            {
                throw;
            }
            try
            {
                resultApi = client.DownloadString($"{webApiv3}{weatherInfo.Lat}&lon={weatherInfo.Lon}&appid={token}");
                dynamic json = JsonConvert.DeserializeObject(resultApi);

                weatherInfo.UvIndex = json.value;

            }
            catch
            {
                throw;
            }

            return weatherInfo;
        }

        public List<WeatherInfo> GetDataCollection(string city)
        {
            WeatherInfo weatherInfo;
            List<WeatherInfo> result = new List<WeatherInfo>();
            WebClient client = new WebClient();
            try
            {
                string APPID = "542ffd081e67f4512b705f89d2a611b2";
                int day = 5;
                string url = string.Format("http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&units=metric&cnt={1}&APPID={2}", city, day, APPID);
                using (WebClient web = new WebClient())
                {
                    var json = web.DownloadString(url);
                    WeatherForecast forecast = JsonConvert.DeserializeObject<WeatherForecast>(json);
                    DateTime mydate = DateTime.Now;

                    foreach (var one in forecast.list)
                    {
                        weatherInfo = new WeatherInfo();
                        weatherInfo.Date = mydate;
                        weatherInfo.Name = forecast.city.name;
                        weatherInfo.WindSpeed = one.speed;
                        weatherInfo.Pressure = one.pressure.ToString();
                        weatherInfo.WeatherType = one.weather[0].main;
                        weatherInfo.TempMin = Convert.ToInt32(one.temp.min);
                        weatherInfo.TempMax = Convert.ToInt32(one.temp.max);
                        weatherInfo.Icon = one.weather[0].icon;
                        weatherInfo.Humidy = one.humidity;
                        weatherInfo.WeatherDescription = one.weather[0].description;
                        result.Add(weatherInfo);
                        mydate = mydate.AddDays(1);
                    }
                }

            }
            catch
            {
                throw;
            }

            return result;
        }
    }
}