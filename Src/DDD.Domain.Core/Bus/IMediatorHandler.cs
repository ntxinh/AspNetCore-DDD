using System.Threading.Tasks;
using DDD.Domain.Core.Commands;
using DDD.Domain.Core.Events;

namespace DDD.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
