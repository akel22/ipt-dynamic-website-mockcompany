using _241419_DynamicWebsite.Application.Services.Results;
using _241419_DynamicWebsite.DTOs;

namespace _241419_DynamicWebsite.Application.Services;

public interface IAuthenticationService
{
    Task<AuthenticationResult> RegisterAsync(RegisterDto model, CancellationToken cancellationToken = default);

    Task<AuthenticationResult> LoginAsync(LoginDto model, CancellationToken cancellationToken = default);

    Task LogoutAsync(CancellationToken cancellationToken = default);
}
