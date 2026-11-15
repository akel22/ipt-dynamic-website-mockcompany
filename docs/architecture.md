# Architecture

## Current solution

The solution currently contains one .NET 10 ASP.NET Core web project: `241419_DynamicWebsite.csproj`. The project is organized into convention-based folders rather than separate projects or assemblies.

The presentation framework is ASP.NET Core MVC. Controllers are under `Presentation Layer/Controllers`, Razor views are under `Presentation Layer/Views`, and static assets are stored under `Presentation Layer/wwwroot` and the project-level asset folders.

## Intended layers

### Presentation Layer

Responsible for HTTP concerns, MVC controllers, Razor views, view models, navigation, authentication pages, validation display, and Bootstrap-based styling. Controllers should coordinate requests and delegate business behavior to application services. Views should render data and should not contain business rules or database access.

### Application Layer

Responsible for use cases, business rules, service contracts, and application-facing models. Services should coordinate business operations and validation. This layer should not depend on MVC types or Razor views.

### Data Access Layer

Responsible for EF Core, SQL Server configuration, Identity persistence, entities, DTOs, and database-related mappings. The application will use direct `DbContext` access from application services rather than adding a repository abstraction for this simple website.

## Dependency direction

The Presentation Layer depends on the Application Layer. The Application Layer depends on Data Access abstractions needed to execute use cases. Data Access contains the concrete EF Core and SQL Server implementation. Views do not access the database directly.

## Current and future content flow

The first landing-page version will use HTML and Razor view markup so the page structure and visual design can be established quickly. Later, company information, services, CEO information, and contact-related data can move into EF Core entities and application services without changing the public section structure of the page.

## Authentication direction

ASP.NET Core Identity will provide registration, login, logout, password handling, and authorization. The company landing page will require an authenticated user. Identity persistence will use the planned SQL Server and EF Core stack.
