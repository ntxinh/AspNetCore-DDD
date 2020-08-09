using System.Threading.Tasks;
using Refit;

namespace DDD.Domain.Services
{
    public interface IFooClient
    {
        [Get("/")]
        Task<object> GetAll();
    }
}
