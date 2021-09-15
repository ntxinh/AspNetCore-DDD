using System.Threading.Tasks;
using DDD.Infra.CrossCutting.Identity.Models.AccountViewModels;
using Microsoft.AspNetCore.Identity;

namespace DDD.Infra.CrossCutting.Identity.Services
{
    public interface IAuthService
    {
        Task<(TokenViewModel, string)> LoginAsync(string email, string password);
        Task<(bool, IdentityResult)> RegisterAsync(string email, string password);
        Task<(TokenViewModel, string, string)> RefreshTokenAsync(string accessToken, string refreshToken);
        dynamic GetCurrentUser();
    }
}
