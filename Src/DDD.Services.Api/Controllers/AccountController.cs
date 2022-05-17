using System.Threading.Tasks;
using DDD.Domain.Core.Bus;
using DDD.Domain.Core.Notifications;
using DDD.Infra.CrossCutting.Identity.Models.AccountViewModels;
using DDD.Infra.CrossCutting.Identity.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DDD.Services.Api.Controllers
{
    [Authorize]
    public class AccountController : ApiController
    {
        private readonly IAuthService _authService;
        private readonly ILogger _logger;

        public AccountController(
            IAuthService authService,
            ILoggerFactory loggerFactory,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _authService = authService;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response();
            }

            var (token, error) = await _authService.LoginAsync(model.Email, model.Password);

            if (token is null)
            {
                NotifyError(error, "Login failure");
                return Response();
            }

            _logger.LogInformation(1, "User logged in.");

            return Response(token);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response();
            }

            var (isRegistrationCompleted, error) = await _authService.RegisterAsync(model.Email, model.Password);
            
            if (!isRegistrationCompleted)
            {
                AddIdentityErrors(error);
                return Response();
            }

            _logger.LogInformation(3, "User created a new account with password.");
            return Response();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("refresh")]
        public async Task<IActionResult> Refresh(TokenViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response();
            }

            var (token, errorCode, errorMessage) = await _authService.RefreshTokenAsync(model.AccessToken, model.RefreshToken);

            if (token is null)
            {
                NotifyError(errorCode, errorMessage);
                return Response();
            }
            
            return Response(token);
        }

        [HttpGet]
        [Route("current")]
        public IActionResult GetCurrent() => Response(_authService.GetCurrentUser());
    }
}
