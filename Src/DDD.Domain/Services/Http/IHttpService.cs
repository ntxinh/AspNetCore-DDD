using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace DDD.Domain.Services.Http
{
    public interface IHttpService
    {
        Task<T> GetAsync<T>(HttpClient httpClient, string url, Dictionary<string, string> queryParams = null, Dictionary<string, string> headers = null);
        Task<T> PostAsJsonAsync<T>(HttpClient httpClient, string url, object data, Dictionary<string, string> queryParams = null, Dictionary<string, string> headers = null);
        Task<T> PostAsFormUrlEncodedAsync<T>(HttpClient httpClient, string url, Dictionary<string, string> data, Dictionary<string, string> queryParams = null, Dictionary<string, string> headers = null);
        Task<Stream> GetStreamAsync(HttpClient httpClient, string url, Dictionary<string, string> queryParams = null, Dictionary<string, string> headers = null);
    }
}
