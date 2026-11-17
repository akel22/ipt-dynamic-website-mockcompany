using _241419_DynamicWebsite.Application.Services;
using _241419_DynamicWebsite.DTOs;
using _241419_DynamicWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _241419_DynamicWebsite.Controllers;

public class AccountController(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IAuthenticationService authenticationService) : Controller
{
    [AllowAnonymous]
    [HttpGet]
    public IActionResult Register(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterDto model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await authenticationService.RegisterAsync(model);
        if (result.Succeeded)
        {
            return LocalRedirect(returnUrl ?? Url.Action("Index", "Home")!);
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error);
        }

        return View(model);
    }

    [AllowAnonymous]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginDto model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await authenticationService.LoginAsync(model);
        if (result.Succeeded)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user is not null && await userManager.IsInRoleAsync(user, "Admin"))
            {
                return RedirectToAction("Dashboard", "Admin");
            }

            return RedirectToAction("Index", "Home");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error);
        }

        return View(model);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var user = await userManager.GetUserAsync(User);
        if (user is null)
        {
            return Challenge();
        }

        return View(new ProfileDto
        {
            UserName = user.UserName ?? string.Empty,
            Email = user.Email ?? string.Empty,
            DateOfBirth = user.DateOfBirth,
            OccupationStatus = user.OccupationStatus
        });
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Profile(ProfileDto model)
    {
        var user = await userManager.GetUserAsync(User);
        if (user is null)
        {
            return Challenge();
        }

        var changingPassword = !string.IsNullOrWhiteSpace(model.NewPassword)
            || !string.IsNullOrWhiteSpace(model.CurrentPassword)
            || !string.IsNullOrWhiteSpace(model.ConfirmNewPassword);

        if (changingPassword && string.IsNullOrWhiteSpace(model.CurrentPassword))
        {
            ModelState.AddModelError(nameof(model.CurrentPassword), "Enter your current password to change it.");
        }

        if (changingPassword && string.IsNullOrWhiteSpace(model.NewPassword))
        {
            ModelState.AddModelError(nameof(model.NewPassword), "Enter a new password.");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (!string.Equals(user.UserName, model.UserName, StringComparison.OrdinalIgnoreCase))
        {
            var result = await userManager.SetUserNameAsync(user, model.UserName);
            AddIdentityErrors(result);
            if (!result.Succeeded)
            {
                return View(model);
            }
        }

        if (!string.Equals(user.Email, model.Email, StringComparison.OrdinalIgnoreCase))
        {
            var result = await userManager.SetEmailAsync(user, model.Email);
            AddIdentityErrors(result);
            if (!result.Succeeded)
            {
                return View(model);
            }
        }

        if (changingPassword)
        {
            var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword!, model.NewPassword!);
            AddIdentityErrors(result);
            if (!result.Succeeded)
            {
                return View(model);
            }
        }

        user.DateOfBirth = model.DateOfBirth!.Value;
        user.OccupationStatus = model.OccupationStatus!.Value;
        var updateResult = await userManager.UpdateAsync(user);
        AddIdentityErrors(updateResult);
        if (!updateResult.Succeeded)
        {
            return View(model);
        }

        await signInManager.RefreshSignInAsync(user);
        TempData["ProfileMessage"] = "Your profile has been updated.";
        return RedirectToAction(nameof(Profile));
    }

    private void AddIdentityErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await authenticationService.LogoutAsync();
        return RedirectToAction("Login");
    }
}
