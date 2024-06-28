using DDD.Domain.Core.Bus;
using DDD.Domain.Core.Commands;
using DDD.Domain.Core.Notifications;
using DDD.Domain.Interfaces;

using MediatR;

namespace DDD.Domain.CommandHandlers;

public class CommandHandler(IUnitOfWork uow, IMediatorHandler bus, INotificationHandler<DomainNotification> notifications)
{
    private readonly IUnitOfWork _uow = uow;
    private readonly IMediatorHandler _bus = bus;
    private readonly DomainNotificationHandler _notifications = (DomainNotificationHandler)notifications;

    public bool Commit()
    {
        if (_notifications.HasNotifications())
        {
            return false;
        }

        if (_uow.Commit())
        {
            return true;
        }

        _bus.RaiseEvent(new DomainNotification("Commit", "We had a problem during saving your data."));

        return false;
    }

    protected void NotifyValidationErrors(Command message)
    {
        foreach (var error in message.ValidationResult.Errors)
        {
            _bus.RaiseEvent(new DomainNotification(message.MessageType, error.ErrorMessage));
        }
    }
}
