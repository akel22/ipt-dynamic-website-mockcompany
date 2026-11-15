# Authentication

## Provider

The project uses ASP.NET Core Identity with `ApplicationUser` and `ApplicationDbContext`. Identity manages password hashing, user creation, sign-in cookies, and logout.

## Routes

- `GET/POST /Account/Register` creates a user and signs them in.
- `GET/POST /Account/Login` authenticates an existing user.
- `POST /Account/Logout` signs the user out.
- `/Home/Index` requires an authenticated user.

## Security notes

- Passwords are processed by Identity and are not stored directly by the application.
- MVC forms use antiforgery validation for state-changing requests.
- The login redirect is restricted to local URLs through `LocalRedirect`.
- Production deployments should use a protected production connection string and HTTPS.
- Email confirmation, password reset, lockout policy, and profile management can be added when required.
