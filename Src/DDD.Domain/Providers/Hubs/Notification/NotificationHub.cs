using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace DDD.Domain.Providers.Hubs;

public interface INotificationHub
{
    Task Send(NotificationItem item);
    Task JoinGroup(string groupName);
    Task LeaveGroup(string groupName);
}

public class NotificationHub : Hub<INotificationHub>
{
    public Task Send(NotificationItem item)
    {
        var groupName = $"{nameof(NotificationItem.UserId)}_{item.UserId}";
        return Clients.Group(groupName).Send(item);
    }

    public async Task JoinGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        await Clients.Group(groupName).JoinGroup(groupName);
    }

    public async Task LeaveGroup(string groupName)
    {
        await Clients.Group(groupName).LeaveGroup(groupName);
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }
}
