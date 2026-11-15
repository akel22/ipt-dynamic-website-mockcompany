using System.ComponentModel.DataAnnotations;
using _241419_DynamicWebsite.Enums;

namespace _241419_DynamicWebsite.DTOs;

public class RegisterDto
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, DataType(DataType.Password), MinLength(8)]
    public string Password { get; set; } = string.Empty;

    [Required, DataType(DataType.Password), Compare(nameof(Password))]
    [Display(Name = "Confirm password")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required, DataType(DataType.Date)]
    [Display(Name = "Date of birth")]
    public DateTime? DateOfBirth { get; set; }

    [Required]
    [Display(Name = "Occupation status")]
    public OccupationStatus? OccupationStatus { get; set; }
}
