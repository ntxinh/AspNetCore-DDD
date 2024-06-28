using Microsoft.AspNetCore.Authorization;

namespace DDD.Infra.CrossCutting.Identity.Authorization;

public class ClaimRequirement(string claimName, string claimValue) : IAuthorizationRequirement
{
    public string ClaimName { get; set; } = claimName;

    public string ClaimValue { get; set; } = claimValue;
}
