using System;

using DDD.Domain.Core.Events;

namespace DDD.Domain.Core.Notifications;

public class DomainNotification(string key, string value) : Event
{
    public Guid DomainNotificationId { get; private set; } = Guid.NewGuid();

    public string Key { get; private set; } = key;

    public string Value { get; private set; } = value;

    public int Version { get; private set; } = 1;
}
