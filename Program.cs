using _241419_DynamicWebsite.Application.Services;
using _241419_DynamicWebsite.Authorization;
using _241419_DynamicWebsite.DataAccess;
using _241419_DynamicWebsite.Enums;
using _241419_DynamicWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Add("/Presentation Layer/Views/{1}/{0}.cshtml");
        options.ViewLocationFormats.Add("/Presentation Layer/Views/Shared/{0}.cshtml");
    });

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddCookie(IdentityConstants.ApplicationScheme, options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ActiveUserRole", policy =>
        policy.Requirements.Add(new ActiveUserRequirement()));
});

builder.Services.AddScoped<IAuthorizationHandler, ActiveUserRequirementHandler>();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserAccountsService, UserAccountsService>();
builder.Services.AddScoped<IWebsiteContentService, WebsiteContentService>();

var app = builder.Build();

await SeedAdminAsync(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRouting();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "assets")),
    RequestPath = "/assets"
});

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "admin-user-details",
    pattern: "Admin/Users/{id}",
    defaults: new { controller = "Admin", action = "UserDetails" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();

static async Task SeedAdminAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var serviceProvider = scope.ServiceProvider;

    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();

    var adminUserName = configuration["AdminSeed:UserName"];
    var adminEmail = configuration["AdminSeed:Email"];
    var adminPassword = configuration["AdminSeed:Password"];

    if (string.IsNullOrWhiteSpace(adminUserName) ||
        string.IsNullOrWhiteSpace(adminEmail) ||
        string.IsNullOrWhiteSpace(adminPassword))
    {
        return;
    }

    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    if (!await roleManager.RoleExistsAsync("User"))
    {
        await roleManager.CreateAsync(new IdentityRole("User"));
    }

    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser is null)
    {
        adminUser = new ApplicationUser
        {
            UserName = adminUserName,
            Email = adminEmail,
            DateOfBirth = new DateTime(2000, 1, 1),
            OccupationStatus = OccupationStatus.Employed
        };

        var createResult = await userManager.CreateAsync(adminUser, adminPassword);
        if (!createResult.Succeeded)
        {
            return;
        }
    }

    if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
    {
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }
}

