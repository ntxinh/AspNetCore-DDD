using DDD.Domain.Core.Bus;
using DDD.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DDD.Services.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Route("api/[controller]/[action]")]
public abstract class ApiController : ControllerBase
{
    private readonly DomainNotificationHandler _notifications;
    private readonly IMediatorHandler _mediator;

    protected ApiController(INotificationHandler<DomainNotification> notifications,
                            IMediatorHandler mediator)
    {
        _notifications = (DomainNotificationHandler)notifications;
        _mediator = mediator;
    }

    protected IEnumerable<DomainNotification> Notifications => _notifications.GetNotifications();

    protected bool IsValidOperation()
    {
        return (!_notifications.HasNotifications());
    }

    protected new IActionResult Response(object result = null)
    {
        if (IsValidOperation())
        {
            return Ok(new
            {
                success = true,
                data = result
            });
        }

        return BadRequest(new
        {
            success = false,
            errors = _notifications.GetNotifications().Select(n => n.Value)
        });
    }

    protected void NotifyModelStateErrors()
    {
        var erros = ModelState.Values.SelectMany(v => v.Errors);
        foreach (var erro in erros)
        {
            var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
            NotifyError(string.Empty, erroMsg);
        }
    }

    protected void NotifyError(string code, string message)
    {
        _mediator.RaiseEvent(new DomainNotification(code, message));
    }

    protected void AddIdentityErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
        {
            NotifyError(result.ToString(), error.Description);
        }
    }
}
