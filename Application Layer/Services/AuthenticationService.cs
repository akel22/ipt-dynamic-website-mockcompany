using _241419_DynamicWebsite.Application.Services.Results;
using _241419_DynamicWebsite.DTOs;
using _241419_DynamicWebsite.Models;
using Microsoft.AspNetCore.Identity;

namespace _241419_DynamicWebsite.Application.Services;

public sealed class AuthenticationService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager) : IAuthenticationService
{
    public async Task<AuthenticationResult> RegisterAsync(RegisterDto model, CancellationToken cancellationToken = default)
    {
        var user = new ApplicationUser
        {
            UserName = model.UserName,
            Email = model.Email,
            DateOfBirth = model.DateOfBirth!.Value,
            OccupationStatus = model.OccupationStatus!.Value
        };

        var result = await userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            return new AuthenticationResult
            {
                Succeeded = false,
                Errors = result.Errors.Select(error => error.Description).ToList()
            };
        }

        var roleResult = await userManager.AddToRoleAsync(user, "User");
        if (!roleResult.Succeeded)
        {
            return new AuthenticationResult
            {
                Succeeded = false,
                Errors = roleResult.Errors.Select(error => error.Description).ToList()
            };
        }

        await signInManager.SignInAsync(user, isPersistent: false);
        return new AuthenticationResult { Succeeded = true };
    }

    public async Task<AuthenticationResult> LoginAsync(LoginDto model, CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByEmailAsync(model.Email);
        var result = user is null
            ? SignInResult.Failed
            : await signInManager.PasswordSignInAsync(
                user.UserName!,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: false);

        if (result.Succeeded)
        {
            return new AuthenticationResult { Succeeded = true };
        }

        return new AuthenticationResult
        {
            Succeeded = false,
            Errors = ["Invalid login attempt."]
        };
    }

    public async Task LogoutAsync(CancellationToken cancellationToken = default)
    {
        await signInManager.SignOutAsync();
    }
}
