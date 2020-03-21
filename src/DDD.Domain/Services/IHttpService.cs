using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DDD.Domain.Services
{
    public interface IHttpService
    {
        Task<Response> GetAsync(string url, Dictionary<string, string> headers = null);
        Response Get(string url, Dictionary<string, string> headers = null);

        Task<Response> PostAsync(string url, object data, Dictionary<string, string> headers = null);
        Response Post(string url, object data, Dictionary<string, string> headers = null);

        Task<Stream> GetStreamAsync(string url);
    }
}
