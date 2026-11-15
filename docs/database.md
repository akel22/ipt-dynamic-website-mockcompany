# Database and EF Core

## Current configuration

`Data Access Layer/ApplicationDbContext.cs` derives from `IdentityDbContext<ApplicationUser>`. `Program.cs` configures EF Core with SQL Server using the `DefaultConnection` connection string.

The default local development connection targets SQL Server LocalDB and is intended to be replaced for other environments. Do not commit production credentials to configuration files.

## Future migration path

1. Add EF Core migrations after the database model is finalized.
2. Add company, service, CEO, and contact-submission entities in the Data Access Layer.
3. Add entity configuration and indexes to the DbContext.
4. Implement application services that use the DbContext directly.
5. Map entities to application/view models before rendering Razor views.
6. Replace the static landing-page markup with database-backed data while preserving its section structure.

A repository layer is intentionally not part of this design for the current scope.
