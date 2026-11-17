using _241419_DynamicWebsite.Enums;

namespace _241419_DynamicWebsite.DTOs;

public class AdminDashboardDto
{
    public int StudentCount { get; set; }

    public int EmployedCount { get; set; }

    public int UnemployedCount { get; set; }

    public int OthersCount { get; set; }

    public int ActiveAccountCount { get; set; }

    public int InactiveAccountCount { get; set; }

    public int TotalAccountCount => ActiveAccountCount + InactiveAccountCount;

    public int GetOccupationCount(OccupationStatus occupationStatus) => occupationStatus switch
    {
        OccupationStatus.Student => StudentCount,
        OccupationStatus.Employed => EmployedCount,
        OccupationStatus.Unemployed => UnemployedCount,
        OccupationStatus.Others => OthersCount,
        _ => 0
    };
}
