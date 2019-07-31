using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace MapsTestApp.Services
{
    public class RequestService : IRequestService
    {
        public async Task<string> Request(string serverUrl, string endpoint, 
            Dictionary<string, string> parameters, HttpMethod method, string data = default(string))
        {
            var uri = new Uri(FormUrl(serverUrl, endpoint, parameters));

            if (method == HttpMethod.Get)
                return await GetRequestAsync(uri);

            if (method == HttpMethod.Get)
                await PostRequestAsync(uri, data);

            throw new NotImplementedException();
        }

        private async Task<string> GetRequestAsync(Uri uri)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(uri);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
                catch (HttpRequestException ex)
                {
                    // TODO: Handle bad requests
                    Debug.WriteLine(ex);
                    return default(string);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    return default(string);
                }
            }
        }

        private async Task<string> PostRequestAsync(Uri uri, string data)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var queryString = new StringContent(data);
                    var response = await client.PostAsync(uri, queryString);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
                catch (HttpRequestException ex)
                {
                    // TODO: Handle bad requests
                    Debug.WriteLine(ex);
                    return default(string);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    return default(string);
                }
            }
        }

        private string FormUrl(string serverUrl, string endpoint, Dictionary<string, string> parameters)
        {
            var url = $"{serverUrl}{endpoint}";
            var firstParam = true;

            if (parameters.Count != 0)
            {
                foreach (var parameter in parameters)
                {
                    url += firstParam ? "?" : "&";
                    url += $"{parameter.Key}={parameter.Value}";
                    firstParam = false;
                }
            }

            return url;
        }
    }
}
