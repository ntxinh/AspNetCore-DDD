using System.Threading.Tasks;
using Refit;

namespace DDD.Domain.Services.Http
{
    public interface IFooClient
    {
        [Get("/")]
        Task<object> GetAll();
    }
}
