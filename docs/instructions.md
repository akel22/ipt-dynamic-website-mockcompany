# Development instructions

## Prerequisites

- Visual Studio 2026 or the .NET 10 SDK
- ASP.NET Core development workload
- SQL Server LocalDB or a compatible SQL Server instance

## Run the application

1. Open `241419_DynamicWebsite.slnx` in Visual Studio.
2. Restore NuGet packages.
3. Confirm the `DefaultConnection` value in `appsettings.json` points to a local SQL Server instance.
4. Apply the EF Core migrations when migrations are added.
5. Run the project over HTTPS.
6. Open `/Account/Register` to create the first user.

The home page requires an authenticated user. Users who are not signed in are redirected to the login page by the authorization cookie.

## Development workflow

- Keep HTTP coordination in MVC controllers.
- Put business rules and use cases in `Application Layer/Services`.
- Keep EF Core and SQL Server concerns in `Data Access Layer`.
- Do not add a repository abstraction for this application unless a clear future requirement justifies it.
- Keep landing-page content in Razor markup until the database-backed content milestone is started.
- Use the existing Bootstrap utilities and the CSS variables in `wwwroot/css/site.css` for styling.
