using Microsoft.Extensions.Logging;
using Satellites.Core.Entities;
using Satellites.Core.Interfaces;
using Satellites.Core.Responses;
using Satellites.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Satellites.Core.Services
{
    public class SatelliteManager : ISatelliteManager
    {
        private readonly ISatelliteRepository _satelliteRepository;
        Dictionary<string, int[]> _satellitesPosition;
        private readonly ILogger<SatelliteManager> _logger;
        List<string> _messageSecret;

        public SatelliteManager(ISatelliteRepository satelliteRepository, ILogger<SatelliteManager> logger)
        {
            _satelliteRepository = satelliteRepository;
            _logger = logger;

            _satellitesPosition = new Dictionary<string, int[]>
            {
                { "kenobi" , new [] { -500,-200 } },
                { "skywalker"  , new [] { 100, -100 }},
                { "sato" , new [] { 500, 100 }}
            };

            _messageSecret = new List<string>() { "este", "es", "un", "mensaje", "secreto" };
        }
        public async Task<ResponseSpaceship> CreateSatellites(SatellitesViewModel models)
        {
            var satelliteResponse = new ResponseSpaceship();
            try
            {
                var satellitesRequest = models.Satellites.Select(x => x.Name.ToLower());
                var wasFoundInDictonary = satellitesRequest.Where(x => _satellitesPosition.ContainsKey(x.ToString())).Count() == 3 ? true : false;
                var countData = _satelliteRepository.GetAll().Result.Count();
                var countDataDb = countData < 3 ? true : false ;
                if (wasFoundInDictonary && countDataDb)
                {
                    var messageSatellite = "";
                    var messageAllSatellites = "";
                    var distances = new List<float?>();
                    foreach (var item in models.Satellites)
                    {
                        distances.Add(item.Distance);
                        messageSatellite = await GetMessage(item.Message.ToArray());
                        await SaveData(item.Name, item.Distance, messageSatellite);
                        messageAllSatellites += messageSatellite + ",";
                    }
                    var getLocation = GetLocation(distances);
                    var valuesTuple = await WasMessageDetermine(messageAllSatellites);
                    if (valuesTuple.Item1)
                    {
                        var sResponse = new PositionAndMessage { Message = valuesTuple.Item2, X = getLocation.Item1, Y = getLocation.Item2 };
                        satelliteResponse = BuildResponseSatellite(true, 1, "succcess", sResponse);
                    }
                    else
                    {
                        satelliteResponse = BuildResponseSatellite(false, 2, "without enough information");
                    }
                }
                else
                {
                    var messa = countData < 3 ? "Object satellites aren't equals" : "No more post with satellites allow 3";
                    satelliteResponse = BuildResponseSatellite(false, 3, messa);
                }

                return satelliteResponse;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error - {ex.Message}-{ex.StackTrace}");
                satelliteResponse = BuildResponseSatellite(false, 4, $"error on server");
                return satelliteResponse;
            }
        }

        public async Task<ResponseSpaceship> GetLocationMessage()
        {
            var satelliteResponse = new ResponseSpaceship();
            try
            {
                var getSatellites = await _satelliteRepository.GetAll();
                if (getSatellites.Count() > 0)
                {
                    var messageSatellite = "";
                    var messageAllSatellites = "";
                    var distances = new List<float?>();
                    foreach (var item in getSatellites)
                    {
                        distances.Add(item.Distance);
                        messageSatellite = string.Join(",", item.Message);
                        messageAllSatellites += messageSatellite + ",";
                    }
                    var getLocation = GetLocation(distances);
                    var valuesTuple = await WasMessageDetermine(messageAllSatellites);
                    if (valuesTuple.Item1)
                    {
                        var sResponse = new PositionAndMessage { Message = valuesTuple.Item2, X = getLocation.Item1, Y = getLocation.Item2 };
                        satelliteResponse = BuildResponseSatellite(true, 1, "succcess", sResponse);
                    }
                    else
                    {
                        satelliteResponse = BuildResponseSatellite(false, 2, "without enough information");
                    }
                }
                else
                {
                    satelliteResponse = BuildResponseSatellite(false, 3, "empty data");
                }
                return satelliteResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error - {ex.Message}-{ex.StackTrace}");
                satelliteResponse = BuildResponseSatellite(false, 4, "problem on server");
                return satelliteResponse;
            }
        }

        public async Task<ResponseSpaceship> UpdateSatellite(Satellite data)
        {
            var satelliteResponse = new ResponseSpaceship();
            try
            {
                var message = GetMessage(data.Message).Result.Split(',');                
                var getSatellite = await _satelliteRepository.UpdateSatellite(data,message);
                if (getSatellite)
                    satelliteResponse = BuildResponseSatellite(true, 1, "succcess");
                else
                    satelliteResponse = BuildResponseSatellite(false, 2, "no found");
                
                return satelliteResponse;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error - {ex.Message}-{ex.StackTrace}");
                satelliteResponse = BuildResponseSatellite(false, 3, "problem on server");
                return satelliteResponse;

            }
        }

        private async Task<string> GetMessage(string[] messageArray)
        {
            var message = "";
            foreach (var item in messageArray)
            {
                if (_messageSecret.Contains(item))
                {
                    message += item + ",";
                }
                else
                {
                    message += "" + ",";
                }
            }
            var newMessage = await Task.FromResult(message.Substring(0, message.Length - 1));
            return newMessage;
        }

        private async Task<Tuple<bool, string>> WasMessageDetermine(string messagesConcate)
        {
            var arrayMessage = messagesConcate.Split(",");
            var cleanMessage = arrayMessage.Distinct().ToList();

            string[] message = new string[_messageSecret.Count()];

            foreach (var item in cleanMessage)
            {
                if (_messageSecret.Contains(item))
                {
                    int index = _messageSecret.IndexOf(item);
                    message[index] = item;
                }
            }
            var messageRequest = string.Join(" ", message);

            return messageRequest.Trim() == "este es un mensaje secreto" ? await Task.FromResult(Tuple.Create(true, messageRequest.Trim())) : await Task.FromResult(Tuple.Create(false, ""));
        }

        private async Task SaveData(string name, float? distance, string messsage)
        {
            var positionSatellite = _satellitesPosition.Where(x => x.Key == name).Select(y => y.Value).FirstOrDefault();
            var nPosition = $"{ positionSatellite[0]},{positionSatellite[1]}";
            await _satelliteRepository.InsertSatellite(name,distance,nPosition,messsage);

        }

        private Tuple<float, float> GetLocation(List<float?> distances)
        {
            var firstSallellite = _satellitesPosition.Where(x => x.Key == "kenobi").Select(y => y.Value).FirstOrDefault();
            var secondSatellite = _satellitesPosition.Where(x => x.Key == "skywalker").Select(y => y.Value).FirstOrDefault();
            var thirdSatellite = _satellitesPosition.Where(x => x.Key == "sato").Select(y => y.Value).FirstOrDefault();
            var xFS = firstSallellite[0];

            var distanceOne = Convert.ToDouble(distances[0]);
            var distanceTwo = Convert.ToDouble(distances[1]);
            var distanceThree = Convert.ToDouble(distances[2]);

            float d = secondSatellite[0] - firstSallellite[0];
            float i = thirdSatellite[0];
            float j = thirdSatellite[1];

            double x, y;

            x = (Squeare(distanceOne) - Squeare(distanceTwo) + Squeare(d)) / (2 * d);
            y = (Squeare(distanceOne) - Squeare(distanceThree) + Squeare(i) + Squeare(j)) / (2 * j) - (i / j) * x;

            return Tuple.Create((float)x, (float)y);
        }

        private static double Squeare(double value)
        {
            return value * value;
        }

        private ResponseSpaceship BuildResponseSatellite(bool isSuccess, short status, string message, PositionAndMessage satelliteResponse = null)
        {
            return new ResponseSpaceship()
            {
                ResponseSuccess = isSuccess,
                Status = status,
                Message = message,
                Data = satelliteResponse
            };
        }
    }
}
