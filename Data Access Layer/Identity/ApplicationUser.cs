using _241419_DynamicWebsite.Enums;
using Microsoft.AspNetCore.Identity;

namespace _241419_DynamicWebsite.Models;

public class ApplicationUser : IdentityUser
{
    public DateTime DateOfBirth { get; set; }

    public OccupationStatus OccupationStatus { get; set; }
}
