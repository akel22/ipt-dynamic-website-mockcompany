using _241419_DynamicWebsite.Application.Services.Results;
using _241419_DynamicWebsite.DTOs;

namespace _241419_DynamicWebsite.Application.Services;

public interface IUserAccountsService
{
    Task<AdminDashboardDto> GetDashboardAsync(CancellationToken cancellationToken = default);

    Task<AdminUsersDto> GetUsersAsync(int page, CancellationToken cancellationToken = default);

    Task<UserAccountStatusResult> ToggleStatusAsync(string id, CancellationToken cancellationToken = default);
}
