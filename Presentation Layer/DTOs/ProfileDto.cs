using System.ComponentModel.DataAnnotations;
using _241419_DynamicWebsite.Enums;

namespace _241419_DynamicWebsite.DTOs;

public class ProfileDto
{
    [Required, StringLength(50, MinimumLength = 3)]
    [Display(Name = "Username")]
    public string UserName { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, DataType(DataType.Date)]
    [Display(Name = "Date of birth")]
    public DateTime? DateOfBirth { get; set; }

    [Required]
    [Display(Name = "Occupation status")]
    public OccupationStatus? OccupationStatus { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Current password")]
    public string? CurrentPassword { get; set; }

    [DataType(DataType.Password), MinLength(8)]
    [Display(Name = "New password")]
    public string? NewPassword { get; set; }

    [DataType(DataType.Password), Compare(nameof(NewPassword))]
    [Display(Name = "Confirm new password")]
    public string? ConfirmNewPassword { get; set; }
}
