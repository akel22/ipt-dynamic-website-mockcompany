using _241419_DynamicWebsite.Enums;

namespace _241419_DynamicWebsite.DTOs;

public sealed class AdminUserDetailsDto
{
    public string Id { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }

    public OccupationStatus OccupationStatus { get; set; }

    public bool IsActive { get; set; }
}
