using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DDD.Domain.Interfaces;
using DDD.Infra.CrossCutting.Identity.Data;
using DDD.Infra.CrossCutting.Identity.Models;
using DDD.Infra.CrossCutting.Identity.Models.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DDD.Infra.CrossCutting.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AuthDbContext _dbContext;
        private readonly IJwtFactory _jwtFactory;
        private readonly IUser _user;

        public AuthService(UserManager<ApplicationUser> userManager,
                            SignInManager<ApplicationUser> signInManager,
                            RoleManager<IdentityRole> roleManager,
                            AuthDbContext dbContext,
                            IJwtFactory jwtFactory,
                            IUser user)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
            _jwtFactory = jwtFactory;
            _user = user;
        }
        public async Task<(TokenViewModel, string)> LoginAsync(string email, string password)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(email, password, false, true);

            if (!signInResult.Succeeded)
            {
                return (null, signInResult.ToString());
            }

            var user = await _userManager.FindByEmailAsync(email);
            
            return (await GenerateToken(user), null);
        }

        public async Task<(bool, IdentityResult)> RegisterAsync(string email, string password)
        {
            var user = new ApplicationUser { UserName = email, Email = email };
            var userCreated = await _userManager.CreateAsync(user,password);
            if (!userCreated.Succeeded)
            {
                return (false, userCreated);
            }

            var roleAssigned = await _userManager.AddToRoleAsync(user, "Admin");
            if (!userCreated.Succeeded)
            {
                return (false, roleAssigned);
            }

            var userClaims = new List<Claim>
            {
                new("Customers_Write", "Write"),
                new("Customers_Remove", "Remove"),
            };
            await _userManager.AddClaimsAsync(user, userClaims);

            return (true, null);
        }

        public async Task<(TokenViewModel, string, string)> RefreshTokenAsync(string accessToken, string refreshToken)
        {
            var refreshTokenCurrent =  await _dbContext.RefreshTokens.SingleOrDefaultAsync
                (x => x.Token == refreshToken && !x.Used && !x.Invalidated);
            
            if (refreshTokenCurrent.ExpiryDate < DateTime.UtcNow)
            {
                refreshTokenCurrent.Invalidated = true;
                await _dbContext.SaveChangesAsync();
                return (null, "RefreshToken", "Refresh token invalid");
            }

            // Get User
            var appUser = await _userManager.FindByIdAsync(refreshTokenCurrent.UserId);
            if (appUser is null)
            {
                return new (null, "User", "User does not exist");
            }

            refreshTokenCurrent.Used = true;
            await _dbContext.SaveChangesAsync();

            return new(await GenerateToken(appUser), null, null);
        }

        public dynamic GetCurrentUser() =>
            new
            {
                IsAuthenticated = _user.IsAuthenticated(),
                ClaimsIdentity = _user.GetClaimsIdentity().Select(x => new { x.Type, x.Value }),
            };

        private async Task<TokenViewModel> GenerateToken(ApplicationUser appUser)
        {
            var claimsIdentity = new ClaimsIdentity(await GetClaims(appUser));

            var jwtToken = await _jwtFactory.GenerateJwtToken(claimsIdentity);

            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString("N"),
                UserId = appUser.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMinutes(90),
                JwtId = jwtToken.JwtId
            };
            await _dbContext.RefreshTokens.AddAsync(refreshToken);
            await _dbContext.SaveChangesAsync();

            return new TokenViewModel
            {
                AccessToken = jwtToken.AccessToken,
                RefreshToken = refreshToken.Token,
            };
        }

        private async Task<List<Claim>> GetClaims(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Email, user.Email),
                new(ClaimTypes.NameIdentifier, user.Id),
            };

            claims.AddRange(await _userManager.GetClaimsAsync(user));

            var userRoles = await _userManager.GetRolesAsync(user);
            claims.AddRange(userRoles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));

            foreach (var userRole in userRoles)
            {
                var role = await _roleManager.FindByNameAsync(userRole);
                var roleClaims = await _roleManager.GetClaimsAsync(role);
                claims.AddRange(roleClaims);
            }

            return claims;
        }
    }
}
