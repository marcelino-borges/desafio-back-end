using API.Exceptions;
using API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    public class ApiController
    {
        //Backup of the last registers for each city - <city, playlist>
        public static Dictionary<string, Playlist> citiesBackupPlaylist;
        //Backup of the last registers for each coordinate - <Coordinate, playlist>
        public static Dictionary<Coordinate, Playlist> coordinatesBackupPlaylist;        

        /// <summary>
        /// Gets a playlist by a city name
        /// Saves a playlist of a city each time it is succesfully queried
        /// Returns an existing city playlist from the backup in case it is saved
        /// </summary>
        /// <param name="city">City name</param>
        /// <returns>Task of type Playlist</returns>
        public static async Task<Playlist> GetPlaylistByCity(string city)
        {
            if (string.IsNullOrEmpty(city))
                throw new ApiException("City name can\'t be null or empty.");
            
            try {
                string stringTemperature = await WeatherService.GetWeatherByCity(city.ToLower());
                int temperature = Convert.ToInt32(Math.Floor(Convert.ToDouble(stringTemperature)));
                Playlist playlist = await GetPlaylistByTemperature(temperature);
                CheckAndSaveBackupPlaylist(citiesBackupPlaylist, playlist, city.ToLower());

                return playlist;
            } catch (Exception e) {
                if (citiesBackupPlaylist == null || !citiesBackupPlaylist.ContainsKey(city)) throw e;
                return citiesBackupPlaylist[city];
            }
        }

        /// <summary>
        /// Gets a playlist by a coordinate
        /// Saves a playlist of a coordinate each time it is succesfully queried
        /// Returns an existing coordinate playlist from the backup in case it is saved
        /// </summary>
        /// <param name="coordinate">Coordinate (Latitute and Longitude)</param>
        /// <returns>Task of type Playlist</returns>
        public static async Task<Playlist> GetPlaylistByCoordinate(Coordinate coordinate)
        {
            if (!Coordinate.IsValid(coordinate))
                throw new ApiException("Coordinate can\'t be null.");

            try
            {
                string stringTemperature = await WeatherService.GetWeatherByCoordinate(coordinate);
                int temperature = Convert.ToInt32(Math.Floor(Convert.ToDouble(stringTemperature)));
                Playlist playlist = await GetPlaylistByTemperature(temperature);
                CheckAndSaveBackupPlaylist(coordinatesBackupPlaylist, playlist, coordinate);

                return playlist;
            } catch(Exception e)
            {
                if (coordinatesBackupPlaylist == null || !coordinatesBackupPlaylist.ContainsKey(coordinate)) throw e;
                return coordinatesBackupPlaylist[coordinate];
            }
        }

        /// <summary>
        /// Checks if a playlist is fulfilled and fill it
        /// </summary>
        /// <typeparam name="T">Type of the key used to save the playlists</typeparam>
        /// <param name="dic">Dictionary where the playlists will be saved</param>
        /// <param name="playlist">Playlista to be be saved</param>
        /// <param name="newKey">Key of the searched location</param>
        private static void CheckAndSaveBackupPlaylist<T>(Dictionary<T, Playlist> dic, Playlist playlist, T newKey)
        {
            if (Playlist.IsPlaylistValid(playlist))
            {
                if (dic == null)
                    dic = new Dictionary<T, Playlist>();
                if (!dic.ContainsKey(newKey))
                    dic.Add(newKey, playlist);
                else
                    dic[newKey] = playlist;
            }
        }

        /// <summary>
        /// Gets a Playlist object by a temperature
        /// </summary>
        /// <param name="temperature"></param>
        /// <returns></returns>
        private static async Task<Playlist> GetPlaylistByTemperature(int temperature)
        {
            string playlistCategory = GetCategoryNameByTemperature(temperature);
            string playlistId = await SpotifyService.GetPlaylistIdByCategory(playlistCategory);
            Playlist playlist = await SpotifyService.GetPlaylistById(playlistId);
            return playlist;
        }

        /// <summary>
        /// Gets a category name in conformity with the temperature (business requirement)
        /// </summary>
        /// <param name="temperature">Temperature in Celsius</param>
        /// <returns>Category name</returns>
        private static string GetCategoryNameByTemperature(int temperature)
        {
            if (temperature > 30)
                return PlaylistCategory.party;
            else if (temperature >= 15 && temperature <= 30)
                return PlaylistCategory.pop;
            else if (temperature >= 10 && temperature <= 14)
                return PlaylistCategory.rock;
            else if (temperature < 10)
                return PlaylistCategory.classic;
            return null;
        }
    }
}
