using _241419_DynamicWebsite.Application.Services.Results;
using _241419_DynamicWebsite.DTOs;
using _241419_DynamicWebsite.Enums;
using _241419_DynamicWebsite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace _241419_DynamicWebsite.Application.Services;

public sealed class UserAccountsService(UserManager<ApplicationUser> userManager) : IUserAccountsService
{
    public async Task<AdminDashboardDto> GetDashboardAsync(CancellationToken cancellationToken = default)
    {
        var occupationCounts = await userManager.Users
            .GroupBy(user => user.OccupationStatus)
            .ToDictionaryAsync(group => group.Key, group => group.Count(), cancellationToken);

        return new AdminDashboardDto
        {
            StudentCount = GetCount(occupationCounts, OccupationStatus.Student),
            EmployedCount = GetCount(occupationCounts, OccupationStatus.Employed),
            UnemployedCount = GetCount(occupationCounts, OccupationStatus.Unemployed),
            OthersCount = GetCount(occupationCounts, OccupationStatus.Others),
            ActiveAccountCount = await userManager.Users.CountAsync(user => user.IsActive, cancellationToken),
            InactiveAccountCount = await userManager.Users.CountAsync(user => !user.IsActive, cancellationToken)
        };
    }

    public async Task<AdminUsersDto> GetUsersAsync(int page, CancellationToken cancellationToken = default)
    {
        const int pageSize = 10;
        var totalUsers = await userManager.Users.CountAsync(cancellationToken);
        var totalPages = Math.Max(1, (int)Math.Ceiling(totalUsers / (double)pageSize));
        var currentPage = Math.Clamp(page, 1, totalPages);

        var users = await userManager.Users
            .OrderBy(user => user.Email)
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .Select(user => new AdminUserDto
            {
                Id = user.Id,
                Email = user.Email ?? user.UserName ?? "Unknown",
                OccupationStatus = user.OccupationStatus,
                DateOfBirth = user.DateOfBirth,
                IsActive = user.IsActive
            })
            .ToListAsync(cancellationToken);

        return new AdminUsersDto
        {
            Users = users,
            CurrentPage = currentPage,
            PageSize = pageSize,
            TotalUsers = totalUsers,
            TotalPages = totalPages
        };
    }

    public async Task<UserAccountStatusResult> ToggleStatusAsync(string id, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return new UserAccountStatusResult { NotFound = true };
        }

        var user = await userManager.FindByIdAsync(id);
        if (user is null)
        {
            return new UserAccountStatusResult { NotFound = true };
        }

        user.IsActive = !user.IsActive;
        var result = await userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return new UserAccountStatusResult
            {
                Succeeded = false,
                Errors = result.Errors.Select(error => error.Description).ToList()
            };
        }

        return new UserAccountStatusResult { Succeeded = true };
    }

    private static int GetCount(
        IReadOnlyDictionary<OccupationStatus, int> occupationCounts,
        OccupationStatus occupationStatus)
    {
        return occupationCounts.TryGetValue(occupationStatus, out var count) ? count : 0;
    }
}
