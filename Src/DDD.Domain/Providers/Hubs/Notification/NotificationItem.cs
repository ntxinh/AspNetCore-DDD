namespace DDD.Domain.Providers.Hubs;

public class NotificationItem
{
    public bool Status { get; set; } = true;
    public string Message { get; set; } = string.Empty;
    public int UserId { get; set; }
}
