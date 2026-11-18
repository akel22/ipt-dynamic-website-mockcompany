using _241419_DynamicWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace _241419_DynamicWebsite.Authorization;

public sealed class ActiveUserRequirement : IAuthorizationRequirement
{
}

public sealed class ActiveUserRequirementHandler(UserManager<ApplicationUser> userManager)
    : AuthorizationHandler<ActiveUserRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ActiveUserRequirement requirement)
    {
        if (context.User?.Identity?.IsAuthenticated != true)
        {
            return;
        }

        if (!context.User.IsInRole("User"))
        {
            return;
        }

        var user = await userManager.GetUserAsync(context.User);
        if (user is not null && user.IsActive)
        {
            context.Succeed(requirement);
        }
    }
}
