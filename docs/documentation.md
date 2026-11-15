# Website documentation

## Purpose

BrightNest Cleaning is a cleaning-services portfolio and company website. It presents the company, its services, contact information, and CEO story to authenticated visitors.

## Current user journey

1. A visitor opens the site.
2. The visitor registers or logs in through ASP.NET Core Identity.
3. Successful authentication redirects the visitor to the protected landing page.
4. The landing page displays the hero, company overview, services, contact, and CEO sections.
5. The visitor can log out from the shared navigation.

## Landing page sections

- **Hero:** Primary value proposition and consultation call to action.
- **About:** Company positioning and service philosophy.
- **Services:** Residential, commercial, and deep-cleaning offerings.
- **Contact:** Email and telephone contact actions.
- **CEO:** Leadership message and portrait placeholder.

The initial content is intentionally static Razor markup. A later milestone can replace it with view models supplied by application services.

## Presentation

The project uses ASP.NET Core MVC and Razor views. Bootstrap supplies the base responsive components. Custom brand styles are in `wwwroot/css/site.css` and use the requested blue, yellow, grey, and white palette.
