using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DDD.Domain.Core.Bus;
using DDD.Domain.Core.Notifications;
using DDD.Infra.CrossCutting.Identity.Models;
using DDD.Infra.CrossCutting.Identity.Models.AccountViewModels;
using DDD.Infra.CrossCutting.Identity.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DDD.Services.Api.Controllers
{
    [Authorize]
    public class AccountController : ApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly IJwtFactory _jwtFactory;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILoggerFactory loggerFactory,
            IJwtFactory jwtFactory,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _jwtFactory = jwtFactory;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);
            if (!result.Succeeded)
                NotifyError(result.ToString(), "Login failure");

            var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
            var jwt = await _jwtFactory.GenerateJwtToken(model.Email, appUser);

            _logger.LogInformation(1, "User logged in.");
            return Response(jwt);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(model);
            }

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // User claim for write customers data
                await _userManager.AddClaimAsync(user, new Claim("Customers", "Write"));

                await _signInManager.SignInAsync(user, false);
                _logger.LogInformation(3, "User created a new account with password.");
                return Response(model);
            }

            AddIdentityErrors(result);
            return Response(model);
        }
    }
}
