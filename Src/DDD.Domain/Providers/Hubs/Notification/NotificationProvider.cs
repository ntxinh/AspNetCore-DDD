using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace DDD.Domain.Providers.Hubs;

public interface INotificationProvider
{
    Task Send(NotificationItem item);
    Task JoinGroup(string groupName);
    Task LeaveGroup(string groupName);
}

public class NotificationProvider : INotificationProvider
{
    private readonly IHubContext<NotificationHub, INotificationHub> _hubContext;

    public NotificationProvider(IHubContext<NotificationHub, INotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task JoinGroup(string groupName)
    {
        // TODO: Find connectionId
        // https://learn.microsoft.com/en-us/aspnet/core/signalr/hubcontext?view=aspnetcore-6.0
        // When client methods are called from outside of the Hub class, there's no caller associated
        // with the invocation. Therefore, there's no access to the ConnectionId, Caller, and Others
        // properties.
        var connectionId = string.Empty;
        await _hubContext.Groups.AddToGroupAsync(connectionId, groupName);
        await _hubContext.Clients.Group(groupName).JoinGroup(groupName);
    }

    public async Task LeaveGroup(string groupName)
    {
        // TODO: Find connectionId
        var connectionId = string.Empty;
        await _hubContext.Clients.Group(groupName).LeaveGroup(groupName);
        await _hubContext.Groups.RemoveFromGroupAsync(connectionId, groupName);
    }

    public async Task Send(NotificationItem item)
    {
        var groupName = $"{nameof(NotificationItem.UserId)}_{item.UserId}";
        await _hubContext.Clients.Group(groupName).Send(item);
    }
}

