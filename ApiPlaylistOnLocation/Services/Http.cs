using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using API.Exceptions;

namespace API.Services
{
    public class Http
    {
        public static async Task<JObject> Get(string endpoint, Dictionary<string, string> headers = null)
        {
            using (HttpClient client = new HttpClient())
            {
                if (headers != null) { 
                    foreach (string key in headers.Keys)
                    {
                        client.DefaultRequestHeaders.Add(key, headers[key]);
                    }
                }

                HttpResponseMessage response = await client.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var json = JObject.Parse(await response.Content.ReadAsStringAsync());

                    return json;
                }

                throw new ApiException("Get wasn't sucessful. " + response.Content.ToString());
            }
        }
    }
}
