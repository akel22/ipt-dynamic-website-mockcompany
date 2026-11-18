using _241419_DynamicWebsite.Application.Services;
using _241419_DynamicWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

namespace _241419_DynamicWebsite.Controllers
{
    public class HomeController(IWebsiteContentService websiteContentService) : Controller
    {
        [Authorize(Policy = "ActiveUserRole")]
        public async Task<IActionResult> Index()
        {
            var content = await websiteContentService.GetPublicContentAsync();
            return View(content);
        }

      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
