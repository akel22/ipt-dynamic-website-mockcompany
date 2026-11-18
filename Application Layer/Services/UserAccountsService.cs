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
        var adminUsers = await userManager.GetUsersInRoleAsync("Admin");
        var adminUserIds = adminUsers.Select(user => user.Id).ToArray();

        var userQuery = userManager.Users
            .Where(user => !adminUserIds.Contains(user.Id));

        var occupationCounts = await userQuery
            .GroupBy(user => user.OccupationStatus)
            .ToDictionaryAsync(group => group.Key, group => group.Count(), cancellationToken);

        return new AdminDashboardDto
        {
            StudentCount = GetCount(occupationCounts, OccupationStatus.Student),
            EmployedCount = GetCount(occupationCounts, OccupationStatus.Employed),
            UnemployedCount = GetCount(occupationCounts, OccupationStatus.Unemployed),
            OthersCount = GetCount(occupationCounts, OccupationStatus.Others),
            ActiveAccountCount = await userQuery.CountAsync(user => user.IsActive, cancellationToken),
            InactiveAccountCount = await userQuery.CountAsync(user => !user.IsActive, cancellationToken)
        };
    }

    public async Task<AdminUsersDto> GetUsersAsync(int page, CancellationToken cancellationToken = default)
    {
        const int pageSize = 10;
        var adminUsers = await userManager.GetUsersInRoleAsync("Admin");
        var adminUserIds = adminUsers.Select(user => user.Id).ToArray();

        var userQuery = userManager.Users
            .Where(user => !adminUserIds.Contains(user.Id));

        var totalUsers = await userQuery.CountAsync(cancellationToken);
        var totalPages = Math.Max(1, (int)Math.Ceiling(totalUsers / (double)pageSize));
        var currentPage = Math.Clamp(page, 1, totalPages);

        var users = await userQuery
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

    public async Task<AdminUserDetailsDto?> GetUserDetailsAsync(string id, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return null;
        }

        return await userManager.Users
            .Where(user => user.Id == id)
            .Select(user => new AdminUserDetailsDto
            {
                Id = user.Id,
                UserName = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                DateOfBirth = user.DateOfBirth,
                OccupationStatus = user.OccupationStatus,
                IsActive = user.IsActive
            })
            .FirstOrDefaultAsync(cancellationToken);
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

    public async Task<UserAccountStatusResult> DeleteAsync(string id, CancellationToken cancellationToken = default)
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

        var result = await userManager.DeleteAsync(user);
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
