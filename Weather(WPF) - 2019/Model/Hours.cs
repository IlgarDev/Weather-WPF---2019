using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace Weather
{
    public class Hours
    {
        public static List<WeatherInfo> ByHours(string city)
        {
                const string token = "9e806ad68210ebdcb7f17fef52a41dfa";
                const string webApi = "http://api.openweathermap.org/data/2.5/forecast?q=";
                string resultapi;
                List<WeatherInfo> forecastWeather = new List<WeatherInfo>();
                WebClient client = new WebClient();
            try
            {
                resultapi = client.DownloadString($"{webApi}{city}&appid={token}");
                JsonConvert.DeserializeObject(resultapi);
                dynamic json = JsonConvert.DeserializeObject(resultapi);
                for (int i = 0; i < 40; i++)
                {
                    forecastWeather.Add(new WeatherInfo()
                    {
                        Name = json.city["name"],
                        TempMin = json.list[i].main["temp_min"] - 273,
                        TempMax = json.list[i].main["temp_max"] - 273,
                        Icon = json.list[i].weather[0]["icon"],
                        WeatherDescription = json.list[i].weather[0]["description"],
                        Date = Convert.ToDateTime(json.list[i].dt_txt),
                    });
                }
            }
            catch { }
                return forecastWeather;
            }
        }
    }