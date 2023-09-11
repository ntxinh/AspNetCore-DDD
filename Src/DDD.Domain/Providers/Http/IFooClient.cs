using System.Threading.Tasks;
using Refit;

namespace DDD.Domain.Providers.Http
{
    public interface IFooClient
    {
        [Get("/")]
        Task<object> GetAll();
    }
}
