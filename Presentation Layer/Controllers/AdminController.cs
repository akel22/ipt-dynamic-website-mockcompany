using _241419_DynamicWebsite.Application.Services;
using _241419_DynamicWebsite.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _241419_DynamicWebsite.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController(
    IUserAccountsService userAccountsService,
    IWebsiteContentService websiteContentService) : Controller
{
    public IActionResult Index() => RedirectToAction(nameof(Dashboard));

    public async Task<IActionResult> Dashboard()
    {
        var dashboard = await userAccountsService.GetDashboardAsync();
        return View(dashboard);
    }

    public async Task<IActionResult> Users(int page = 1)
    {
        var users = await userAccountsService.GetUsersAsync(page);
        return View(users);
    }

    public async Task<IActionResult> UserDetails(string id)
    {
        var user = await userAccountsService.GetUserDetailsAsync(id);
        if (user is null)
        {
            return NotFound();
        }

        return View(user);
    }

    [HttpGet]
    public async Task<IActionResult> Content()
    {
        var content = await websiteContentService.GetAdminContentAsync();
        return View(content);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Content(AdminContentDto model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await websiteContentService.SaveAdminContentAsync(model);
        TempData["ContentMessage"] = "Website content has been saved.";
        return RedirectToAction(nameof(Content));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleStatus(string id, int page = 1)
    {
        var result = await userAccountsService.ToggleStatusAsync(id);
        if (result.NotFound)
        {
            return NotFound();
        }

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }

            return View("Users", await userAccountsService.GetUsersAsync(page));
        }

        return RedirectToAction(nameof(Users), new { page });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteUser(string id, int page = 1)
    {
        var result = await userAccountsService.DeleteAsync(id);
        if (result.NotFound)
        {
            return NotFound();
        }

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }

            return View("Users", await userAccountsService.GetUsersAsync(page));
        }

        return RedirectToAction(nameof(Users), new { page });
    }
}
