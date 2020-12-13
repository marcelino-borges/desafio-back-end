using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using API.Exceptions;
using API.Models;

namespace API.Services
{
    public class WeatherService
    {
        #region Authentication
        private static readonly string apiKey = "fb34a1007254b900de1aaf221ba02404"; //Abstracting security for this test's purposes
        #endregion

        #region Endpoints
        private static readonly string endpoint = "http://api.openweathermap.org/data/2.5/";
        #endregion

        #region Paths
        private static readonly string pathByCity = "weather?q={city name}&appid={API key}&units=metric";
        private static readonly string pathByCoordinates = "weather?lat={lat}&lon={lon}&appid={API key}&units=metric";
        #endregion

        #region Parameters names
        private static readonly string cityParameterName = "{city name}";
        private static readonly string apiKeyParameterName = "{API key}";
        private static readonly string latitudeParameterName = "{lat}";
        private static readonly string longitudeParameterName = "{lon}";
        #endregion        

        /// <summary>
        /// Gets the weather of a place by the city name
        /// </summary>
        /// <param name="city">Name of the city being browsed</param>
        /// <returns>Temperature of the city</returns>
        public static async Task<string> GetWeatherByCity(string city)
        {
            try
            {
                string finalPath = endpoint + pathByCity
                    .Replace(cityParameterName, city)
                    .Replace(apiKeyParameterName, apiKey);

                var json = await Get(finalPath);

                return json["main"]["temp"].ToString();                
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Gets the weather of a place by it's coordinate
        /// </summary>
        /// <param name="lat">Latitude</param>
        /// <param name="lon">Longitude</param>
        /// <returns>Temperature of the city</returns>
        public static async Task<string> GetWeatherByCoordinate(Coordinate coordinates)
        {
            try
            {
                string finalPath = endpoint + pathByCoordinates
                    .Replace(latitudeParameterName, coordinates.Latitude)
                    .Replace(longitudeParameterName, coordinates.Longitude)
                    .Replace(apiKeyParameterName, apiKey);

                var json = await Get(finalPath);

                return json["main"]["temp"].ToString();
            }
            catch (Exception e)
            {
                throw new ApiException(e.Message);
            }
        }

        /// <summary>
        /// Makes an HTTP GET request
        /// </summary>
        /// <param name="endpoint">Resquest endpoint and path</param>
        /// <returns>Response JSON</returns>
        private static async Task<JObject> Get(string endpoint)
        {
            return await Http.Get(endpoint, null);
        }
    }
}
