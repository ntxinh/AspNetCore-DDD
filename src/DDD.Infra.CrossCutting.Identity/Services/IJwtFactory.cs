using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DDD.Infra.CrossCutting.Identity.Services
{
    public interface IJwtFactory
    {
        Task<string> GenerateJwtToken(string email, IdentityUser identity);
    }
}
