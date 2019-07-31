using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MapsTestApp.Services
{
    public interface IRequestService
    {
        Task<string> Request(string serverUrl, string endpoint, Dictionary<string, string> parameters, HttpMethod method, string data = default(string));
    }
}