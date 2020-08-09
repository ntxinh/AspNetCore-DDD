using System.Threading.Tasks;
using Refit;

namespace DDD.Domain.Services
{
    public interface IFoo
    {
        [Get("/")]
        Task<object> GetAll();
    }
}
