using System;
using System.Collections.Generic;
using System.Net;

namespace Weather
{
    public class DBProxy : IDBConnection
    {
        private Dictionary<string, WeatherInfo> weatherInfos = new Dictionary<string, WeatherInfo>();
        private IDBConnection connetion = new DBConnetion();
        private DateTime lastUpdate = new DateTime();

        public WeatherInfo GetData(string city)
        {
            DateTime dateTime = lastUpdate;
            if (dateTime.AddSeconds(30) < DateTime.Now)
            {
                weatherInfos.Clear();
            }
            WeatherInfo info = null;
            weatherInfos.TryGetValue(city, out info);
            if (info == null)
            {
                try
                {
                    info = connetion.GetData(city);
                    weatherInfos.Add(info.Name, info);
                    lastUpdate = DateTime.Now;
                }
                catch (WebException ex)
                {
                    Console.WriteLine(ex.Message);
                    switch (ex.Status)
                    {
                        case WebExceptionStatus.Success:
                            break;
                        case WebExceptionStatus.NameResolutionFailure:
                            break;
                        case WebExceptionStatus.ConnectFailure:
                            break;
                        case WebExceptionStatus.ReceiveFailure:
                            break;
                        case WebExceptionStatus.SendFailure:
                            break;
                        case WebExceptionStatus.PipelineFailure:
                            break;
                        case WebExceptionStatus.RequestCanceled:
                            break;
                        case WebExceptionStatus.ProtocolError:
                            break;
                        case WebExceptionStatus.ConnectionClosed:
                            break;
                        case WebExceptionStatus.TrustFailure:
                            break;
                        case WebExceptionStatus.SecureChannelFailure:
                            break;
                        case WebExceptionStatus.ServerProtocolViolation:
                            break;
                        case WebExceptionStatus.KeepAliveFailure:
                            break;
                        case WebExceptionStatus.Pending:
                            break;
                        case WebExceptionStatus.Timeout:
                            break;
                        case WebExceptionStatus.ProxyNameResolutionFailure:
                            break;
                        case WebExceptionStatus.UnknownError:
                            break;
                        case WebExceptionStatus.MessageLengthLimitExceeded:
                            break;
                        case WebExceptionStatus.CacheEntryNotFound:
                            break;
                        case WebExceptionStatus.RequestProhibitedByCachePolicy:
                            break;
                        case WebExceptionStatus.RequestProhibitedByProxy:
                            break;
                        default:
                            break;
                    }
                }

                return info;
            }
            else
            {
                return info;
            }
        }

        public List<WeatherInfo> GetDataCollection(string city)
        {
            return connetion.GetDataCollection(city);
        }
    }
}
