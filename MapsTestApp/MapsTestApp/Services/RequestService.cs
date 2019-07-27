using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace MapsTestApp.Services
{
    public class RequestService : IRequestService
    {
        public async Task<string> Request(string serverUrl, string endpoint, Dictionary<string, string> parameters, HttpMethod method)
        {
            var uri = new Uri(FormUrl(serverUrl, endpoint, parameters));

            if (method == HttpMethod.Get)
                return await GetRequest(uri);

            if (method == HttpMethod.Get)
                await PostRequest(uri);

            throw new NotImplementedException();
        }

        private async Task<string> GetRequest(Uri uri)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(uri);
                    return await response.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    return default(string);
                }
            }
        }

        private async Task<string> PostRequest(Uri uri)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(uri);
                    return await response.Content.ReadAsStringAsync();
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
