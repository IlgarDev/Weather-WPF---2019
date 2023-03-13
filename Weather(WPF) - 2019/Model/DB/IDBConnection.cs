using System.Collections.Generic;

namespace Weather
{
    public interface IDBConnection
    {
        WeatherInfo GetData(string city);
        List<WeatherInfo> GetDataCollection(string city);
    }
}
