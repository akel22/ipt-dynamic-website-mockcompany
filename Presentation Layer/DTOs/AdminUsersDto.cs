namespace _241419_DynamicWebsite.DTOs;

public class AdminUsersDto
{
    public IReadOnlyList<AdminUserDto> Users { get; set; } = [];

    public int CurrentPage { get; set; }

    public int PageSize { get; set; }

    public int TotalUsers { get; set; }

    public int TotalPages { get; set; }
}
