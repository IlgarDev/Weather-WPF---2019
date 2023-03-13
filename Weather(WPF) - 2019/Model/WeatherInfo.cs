using GalaSoft.MvvmLight;
using System;

namespace Weather
{
    public class WeatherInfo : ViewModelBase
    {
        private double _feel;
        private string _icon;
        private string _lat;
        private string _lon;
        private string _name;
        private int _tempMin;
        private int _tempMax;
        private int _uvindex;
        private double _sunrise;
        private double _sunset;
        private DateTime _date;
        private string _weatherType;
        private string _weatherdescription;
        private double _windspeed;
        private int _humidy;
        private double _dewpoint;
        private int _visibillity;
        private DateTime _truesunrise;
        private DateTime _truesunset;
        private string _pressure;
        private string _countryindex;
        private string _cloudsall;

        public string Lat
        {
            get => _lat;
            set
            {
                _lat = value;
                RaisePropertyChanged(nameof(Lat));
            }
        }

        public string Lon
        {
            get => _lon;
            set
            {
                _lon = value;
                RaisePropertyChanged(nameof(Lon));
            }
        }

        public int UvIndex
        {
            get => _uvindex;
            set
            {
                _uvindex = value;
                RaisePropertyChanged(nameof(UvIndex));
            }
        }

        public string CloudsAll
        {
            get => _cloudsall;
            set
            {
                _cloudsall = value;
                RaisePropertyChanged(nameof(CloudsAll));
            }
        }

        public string CountryIndex
        {
            get => _countryindex;
            set
            {
                _countryindex = value;
                RaisePropertyChanged(nameof(CountryIndex));
            }
        }

        public string TrueBackgroundIcon
        {
            get => $@"../Elements/Images/{_icon}.jpg";
        }

        public double DewPoint
        {
            get
            {
                _dewpoint = _tempMax - (1 - (_humidy / 100) / 0.05);
                return _dewpoint;
            }
            set
            {
                _dewpoint = value;
                RaisePropertyChanged(nameof(DewPoint));
            }
        }

        public string TrueIcon
        {
            get => $@"../Elements/Icons/{_icon}.png";
        }

        public double SunSet
        {
            get => _sunset;
            set
            {
                _sunset = value;
                RaisePropertyChanged(nameof(SunSet));
            }
        }

        public DateTime TrueSunSet
        {
            get
            {
                double timestamp = _sunset;
                _truesunset = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                _truesunset = _truesunset.AddSeconds(timestamp);
                _truesunset = _truesunset.AddHours(4);
                return _truesunset;
            }
        }

        public DateTime TrueSunRise
        {
            get
            {
                double timestamp = _sunrise;
                _truesunrise = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                _truesunrise = _truesunrise.AddSeconds(timestamp);
                _truesunrise = _truesunrise.AddHours(4);
                return _truesunrise;
            }
            set
            {
                _truesunrise = value;
                RaisePropertyChanged(nameof(TrueSunRise));
            }
        }

        public double SunRise
        {
            get => _sunrise;
            set
            {
                _sunrise = value;
                RaisePropertyChanged(nameof(SunRise));
            }
        }

        public int Visibillity
        {
            get => _visibillity / 1000;
            set
            {
                _visibillity = value;
                RaisePropertyChanged(nameof(Visibillity));
            }
        }

        public double Feeling
        {
            get
            {
                _feel = Convert.ToInt32( _tempMax - 2 * (_windspeed * 10 / 36));
                return _feel;
            }
            set
            {
                _feel = value;
                RaisePropertyChanged(nameof(Feeling));
            }
        }

        public string Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                RaisePropertyChanged(nameof(Icon));
            }
        }
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value; RaisePropertyChanged(nameof(Date));
            }
        }

        public int Humidy
        {
            get => _humidy;
            set
            {
                _humidy = value;
                RaisePropertyChanged(nameof(Humidy));
            }
        }

        public string Pressure
        {
            get => _pressure;
            set
            {
                _pressure = value;
                RaisePropertyChanged(nameof(Pressure));
            }
        }

        public double WindSpeed
        {
            get => _windspeed;
            set
            {
                _windspeed = value;
                RaisePropertyChanged(nameof(WindSpeed));
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        public int TempMin
        {
            get => _tempMin;
            set
            {
                _tempMin = value;
                RaisePropertyChanged(nameof(TempMin));
            }
        }
        public int TempMax
        {
            get => _tempMax;
            set
            {
                _tempMax = value;
                RaisePropertyChanged(nameof(TempMax));
            }
        }
        public string WeatherType
        {
            get => _weatherType;
            set
            {
                _weatherType = value;
                RaisePropertyChanged(nameof(WeatherType));
            }
        }
        public string WeatherDescription
        {
            get => _weatherdescription;
            set
            {
                _weatherdescription = value;
                RaisePropertyChanged(nameof(WeatherDescription));
            }
        }
    }
}