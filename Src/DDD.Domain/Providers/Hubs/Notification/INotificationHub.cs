using System.Threading.Tasks;

namespace DDD.Domain.Providers.Hubs;

public interface INotificationHub
{
    Task Send(NotificationItem item);

    Task JoinGroup(string groupName);

    Task LeaveGroup(string groupName);
}
