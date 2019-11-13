using System.Threading.Tasks;

namespace DDD.Infra.CrossCutting.Identity.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
