using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Media;
using System.Speech.Synthesis;
using System.Threading;

namespace Weather.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
        private SoundPlayer soundPlayer = new SoundPlayer();
        private DBProxy connection = new DBProxy();
        private readonly BackgroundWorker worker;


        #region Properties

        private List<WeatherInfo> _byHours;
        public List<WeatherInfo> ByHours
        {
            get => _byHours;
            set
            {
                _byHours = value;
                RaisePropertyChanged(nameof(ByHours));
            }
        }

        private WeatherInfo _actualWeather;
        public WeatherInfo ActualWeather
        {
            get => _actualWeather;
            set
            {
                _actualWeather = value;
                RaisePropertyChanged(nameof(ActualWeather));
            }
        }



        private List<WeatherInfo> _actualForecastWeather;
        public List<WeatherInfo> ActualForecastWeather
        {
            get => _actualForecastWeather;
            set
            {
                _actualForecastWeather = value;
                RaisePropertyChanged(nameof(ActualForecastWeather));
            }
        }

        private string _dateText;
        public string DateText
        {
            get => _dateText;
            set
            {
                _dateText = value;
                RaisePropertyChanged(nameof(DateText));
            }
        }

        private string _imagePath;
        public string ImagePath
        {
            get => _imagePath;
            set
            {
                _imagePath = value;
                RaisePropertyChanged(nameof(ImagePath));
            }
        }

        private string _nameOfCity;
        public string NameOfCity
        {
            get => _nameOfCity;
            set
            {
                _nameOfCity = value;
                RaisePropertyChanged(nameof(NameOfCity));
            }
        }

        private string _maxTemperature;
        public string MaxTemperature
        {
            get => _maxTemperature;
            set
            {
                _maxTemperature = value;
                RaisePropertyChanged(nameof(MaxTemperature));
            }
        }

        private int _progressValue;
        public int ProgressValue
        {
            get { return _progressValue; }
            set
            {
                _progressValue = value;
                RaisePropertyChanged("ProgressValue");
                RaisePropertyChanged("ProgressText");
            }
        }
        #endregion

        #region cmd

        private RelayCommand<object> _selectionChangedCmd;
        public RelayCommand<object> SelectionChangedCmd => _selectionChangedCmd ?? (_selectionChangedCmd = new RelayCommand<object>((t) =>
        {
            int index = (int)t;
            var collec = Hours.ByHours(ActualWeather.Name);
            switch (index)
            {
                case 0: ByHours = collec.GetRange(0, 5); break;
                case 1: ByHours = collec.GetRange(8, 5); break;
                case 2: ByHours = collec.GetRange(16, 5); break;
                case 3: ByHours = collec.GetRange(24, 5); break;
                case 4: ByHours = collec.GetRange(32, 5); break;
            }
        }));

        private RelayCommand _celsisCmd;
        public RelayCommand CelsisCmd => _celsisCmd ?? (_celsisCmd = new RelayCommand(() =>
        {
            ActualWeather.Date = DateTime.Now;
            MaxTemperature = $"{ActualWeather.TempMax.ToString()} °C";
            foreach (var one in ActualForecastWeather)
            {
                one.TempMax = ActualWeather.TempMax;
                one.TempMin = ActualWeather.TempMin;
            }
            foreach (var one in ByHours)
            {
                one.TempMax = ActualWeather.TempMax;
                one.TempMin = ActualWeather.TempMin;
            }
            speechSynthesizer.Speak("Celsis");
        }));
        private RelayCommand _forengeitÑmd;
        public RelayCommand ForengeitÑmd => _forengeitÑmd ?? (_forengeitÑmd = new RelayCommand(() =>
        {
            ActualWeather.Date = DateTime.Now;
            int result = (ActualWeather.TempMax * 9) / 5 + 32;
            MaxTemperature = $"{result.ToString()} °F";
            foreach (var one in ActualForecastWeather)
            {
                one.TempMax = (ActualWeather.TempMax * 9) / 5 + 32;
                one.TempMin = (ActualWeather.TempMin * 9) / 5 + 32;
            }
            foreach (var one in ByHours)
            {
                one.TempMax = (ActualWeather.TempMax * 9) / 5 + 32;
                one.TempMin = (ActualWeather.TempMax * 9) / 5 + 32;
            }
            speechSynthesizer.Speak("Forengeit");
        }));


        private RelayCommand _searchCmd;
        public RelayCommand SearchCmd => _searchCmd ?? (_searchCmd = new RelayCommand(() =>
        {
            DBProxy connection = new DBProxy();
            if (!String.IsNullOrEmpty(NameOfCity))
            {
                ByHours = Hours.ByHours(NameOfCity).GetRange(0, 5);
                ActualWeather = connection.GetData(NameOfCity);
                ActualForecastWeather = connection.GetDataCollection(NameOfCity);
                ActualWeather.Date = DateTime.Now;
                MaxTemperature = ActualWeather.TempMax.ToString() + " °C";
                speechSynthesizer.Speak($"{NameOfCity} information");
                Choose();
            }
            else
            {
                speechSynthesizer.Speak("Enter the right city please");
            }
        }));

        public void Choose()
        {
            if (ActualWeather.WeatherType == "Rain")
            {
                soundPlayer.SoundLocation = $@"../../Elements/Sounds/rain.wav";
                soundPlayer.PlayLooping();
            }
            else if (ActualWeather.WeatherType == "Clear")
            {
                soundPlayer.Stop();
                soundPlayer.SoundLocation = $@"../../Elements/Sounds/sunny.wav";
                soundPlayer.PlayLooping();
            }
            else if (ActualWeather.WeatherType == "Snow")
            {
                soundPlayer.Stop();
                soundPlayer.SoundLocation = $@"../../Elements/Sounds/snow.wav";
                soundPlayer.PlayLooping();
            }
            else if (ActualWeather.WeatherType == "Clouds")
            {
                soundPlayer.Stop();
                soundPlayer.SoundLocation = $@"../../Elements/Sounds/snow.wav";
                soundPlayer.PlayLooping();
            }
            else if (ActualWeather.WeatherType == "Thunder")
            {
                soundPlayer.Stop();
                soundPlayer.SoundLocation = $@"../../Elements/Sounds/thunder.wav";
                soundPlayer.PlayLooping();
            }
        }

        private RelayCommand _refreshCmd;
        public RelayCommand RefreshCmd => _refreshCmd ?? (_refreshCmd = new RelayCommand(() =>
        {
            speechSynthesizer.Speak("The weather info successfully refreshed");
            ActualWeather.Date = DateTime.Now;
            DateText = $" Updated as of {ActualWeather.Date.ToLongTimeString()}";

            ByHours = Hours.ByHours(ActualWeather.Name).GetRange(0, 5);
            ActualWeather = connection.GetData(ActualWeather.Name);
            ActualForecastWeather = connection.GetDataCollection(ActualWeather.Name);
            ActualWeather.Date = DateTime.Now;
            MaxTemperature = ActualWeather.TempMax.ToString() + " °C";

        }));
        #endregion

        public MainViewModel()
        {
            string city = "Baku";
            ByHours = Hours.ByHours(city).GetRange(0, 5);
            ActualWeather = connection.GetData(city);
            var data = connection.GetData(city);
            ActualForecastWeather = connection.GetDataCollection(city);
            ActualWeather.Date = DateTime.Now;
            MaxTemperature = ActualWeather.TempMax.ToString() + " °C";
            this.worker = new BackgroundWorker();
            this.worker.WorkerReportsProgress = true;
            this.worker.DoWork += this.DoWork;
            Choose();
        }

        public string ProgressText
        {
            get { return string.Format("{0} %", _progressValue); }
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                Thread.Sleep(100);
                worker.ReportProgress(i);
            }
        }
    }
}