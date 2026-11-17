using _241419_DynamicWebsite.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace _241419_DynamicWebsite.Models;

public class ApplicationUser : IdentityUser
{
    [Required]
    public bool IsActive { get; set; } = true;

    [Required, DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    [Required]
    public OccupationStatus OccupationStatus { get; set; }
}
