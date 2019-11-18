using System.Collections.Generic;
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

            // SignIn
            var signInResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);
            if (!signInResult.Succeeded)
            {
                NotifyError(signInResult.ToString(), "Login failure");
                return Response();
            }

            // Get User
            var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);

            // Get Roles
            var roles = await _userManager.GetRolesAsync(appUser);

            // Get Claims
            var claims = await _userManager.GetClaimsAsync(appUser);

            // Init ClaimsIdentity
            var claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaims(claims);
            claimsIdentity.AddClaims(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));
            // ClaimsIdentity.DefaultRoleClaimType & ClaimTypes.Role is the same

            // Generate token
            var jwt = await _jwtFactory.GenerateJwtToken(model.Email, claimsIdentity);

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

            // Add User
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var identityResult = await _userManager.CreateAsync(user, model.Password);
            if (!identityResult.Succeeded)
            {
                AddIdentityErrors(identityResult);
                return Response(model);
            }

            // Add Roles
            identityResult = await _userManager.AddToRoleAsync(user, "Admin");
            if (!identityResult.Succeeded)
            {
                AddIdentityErrors(identityResult);
                return Response(model);
            }

            // Add Claims
            var claims = new List<Claim>
            {
                new Claim("Customers", "Write"),
                new Claim("Customers", "Remove"),
            };
            await _userManager.AddClaimsAsync(user, claims);

            // SignIn
            //await _signInManager.SignInAsync(user, false);

            _logger.LogInformation(3, "User created a new account with password.");
            return Response(model);
        }
    }
}
