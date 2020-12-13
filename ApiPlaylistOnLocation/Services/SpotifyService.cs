using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Exceptions;
using API.Models;
using System.Diagnostics;

namespace API.Services
{
    public class SpotifyService
    {
        #region Authentication
        private static readonly string clientId = "4084c052e4cc4848b1e943725359afaf";
        private static readonly string clientSecret = "a501fc30c94d4c258b7325481c221c01"; //Abstracting security for this test's purposes
        #endregion

        #region Endpoints
        private static readonly string endpointAuth = "https://accounts.spotify.com";
        private static readonly string endpointRequests = "https://api.spotify.com/v1";
        #endregion

        #region Paths
        private static readonly string authPath = "/api/token";
        private static readonly string categoryPlaylistPath = "/browse/categories/{category_id}/playlists?limit=1";
        private static readonly string playlistsPath = "/playlists/{playlist_id}";
        #endregion

        #region Parameters names
        private static readonly string categoryIdParameter = "{category_id}";
        private static readonly string playlistIdParameter = "{playlist_id}";
        #endregion

        private static string AccessToken { get; set; }

        /// <summary>
        /// Authenticates on Spotify and returns a token necessary to further requests
        /// </summary>
        /// <returns>Access token</returns>
        public static async Task<string> GetAuthenticationToken()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(endpointAuth);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                        "Basic",
                        GetEncodedCredentials()
                    );
                    var dict = new Dictionary<string, string>();
                    dict.Add("Content-Type", "application/x-www-form-urlencoded");
                    dict.Add("grant_type", "client_credentials");

                    HttpResponseMessage response = await client.PostAsync(authPath, new FormUrlEncodedContent(dict));

                    if (response.IsSuccessStatusCode)
                    {
                        var json = JObject.Parse(await response.Content.ReadAsStringAsync());

                        return json["access_token"].ToString();
                    }

                    throw new ApiException("Error on the authentication!");
                }
            }
            catch (Exception e)
            {
                throw new ApiException(e.Message);
            }
        }

        /// <summary>
        /// Gets a playlist ID browsed by it's category keyword
        /// </summary>
        /// <param name="category">An existing keyword used by playlists</param>
        /// <returns>ID representing a playlist object</returns>
        public static async Task<string> GetPlaylistIdByCategory(string category)
        {
            try
            {
                if (string.IsNullOrEmpty(AccessToken))
                    AccessToken = await GetAuthenticationToken();
                
                string finalPath = endpointRequests + categoryPlaylistPath.Replace(categoryIdParameter, category);
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + AccessToken);
                var json = await Get(finalPath, headers);

                return json["playlists"]["items"][0]["id"].ToString();
            }
            catch (Exception e)
            {
                throw new ApiException(e.Message);
            }
        }

        /// <summary>
        /// Gets a playlist object by an ID
        /// </summary>
        /// <param name="playlistId">ID representing the playlist</param>
        /// <returns>Object containing all the playlist properties</returns>
        public static async Task<Playlist> GetPlaylistById(string playlistId)
        {
            try
            {
                if (string.IsNullOrEmpty(AccessToken))
                    AccessToken = await GetAuthenticationToken();

                string finalPath = endpointRequests + playlistsPath.Replace(playlistIdParameter, playlistId);
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + AccessToken);
                var json = await Get(finalPath, headers);
                List<Track> tracks = json["tracks"]["items"].Select(e => new Track(e["track"]["name"].ToString())).ToList();
                Playlist playlist = new Playlist(tracks);

                return playlist;
            }
            catch (Exception e)
            {
                throw new ApiException(e.Message);
            }
        }

        /// <summary>
        /// Makes an HTTP GET request
        /// </summary>
        /// <param name="endpoint">Request endpoint</param>
        /// <param name="headers">Request headers</param>
        /// <returns>Response JSON</returns>
        private static async Task<JObject> Get(string endpoint, Dictionary<string, string> headers)
        {
            return await Http.Get(endpoint, headers);
        }

        /// <summary>
        /// Encodes the credentials in base64
        /// </summary>
        /// <returns>Final string containing the encoded credentials</returns>
        private static string GetEncodedCredentials()
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}"));
        }
    }
}
