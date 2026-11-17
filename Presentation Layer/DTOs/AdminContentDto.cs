using System.ComponentModel.DataAnnotations;

namespace _241419_DynamicWebsite.DTOs;

public sealed class AdminContentDto
{
    public int Id { get; set; }

    [Required, StringLength(150)]
    public string HeroTitle { get; set; } = string.Empty;

    [Required, StringLength(1000)]
    public string HeroDescription { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string HeroCtaText { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string HeroCtaTarget { get; set; } = "#contact";

    [Required, StringLength(100)]
    public string AboutEyebrow { get; set; } = string.Empty;

    [Required, StringLength(150)]
    public string AboutTitle { get; set; } = string.Empty;

    [Required, StringLength(1000)]
    public string AboutDescription { get; set; } = string.Empty;

    [Required, StringLength(1000)]
    public string AboutSecondaryDescription { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string ServicesEyebrow { get; set; } = string.Empty;

    [Required, StringLength(150)]
    public string ServicesTitle { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string ContactEyebrow { get; set; } = string.Empty;

    [Required, StringLength(150)]
    public string ContactTitle { get; set; } = string.Empty;

    [Required, StringLength(1000)]
    public string ContactDescription { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(256)]
    public string ContactEmail { get; set; } = string.Empty;

    [Required, Phone, StringLength(30)]
    public string ContactPhone { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string CeoEyebrow { get; set; } = string.Empty;

    [Required, StringLength(150)]
    public string CeoTitle { get; set; } = string.Empty;

    [Required, StringLength(150)]
    public string ChiefExecutiveName { get; set; } = string.Empty;

    [Required, StringLength(150)]
    public string ChiefExecutiveTitle { get; set; } = string.Empty;

    [Required, StringLength(1000)]
    public string ChiefExecutiveDescription { get; set; } = string.Empty;

    public List<AdminServiceContentDto> Services { get; set; } = [];
}

public sealed class AdminServiceContentDto
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(1000)]
    public string Description { get; set; } = string.Empty;
}
