using _241419_DynamicWebsite.Enums;

namespace _241419_DynamicWebsite.DTOs;

public class AdminUserDto
{
    public string Id { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public OccupationStatus OccupationStatus { get; set; }

    public DateTime DateOfBirth { get; set; }

    public bool IsActive { get; set; }
}
