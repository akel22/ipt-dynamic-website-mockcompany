using System.ComponentModel.DataAnnotations;

namespace _241419_DynamicWebsite.Models;

public sealed class CompanyContent
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string CompanyName { get; set; } = "EZ Clean.";

    [Required, StringLength(150)]
    public string HeroTitle { get; set; } = "A brighter, healthier place to live and work.";

    [Required, StringLength(1000)]
    public string HeroDescription { get; set; } = "Reliable cleaning services delivered with care, consistency, and attention to the details that matter.";

    [Required, StringLength(100)]
    public string HeroCtaText { get; set; } = "Learn more";

    [Required, StringLength(100)]
    public string HeroCtaTarget { get; set; } = "#contact";

    [Required, StringLength(100)]
    public string AboutEyebrow { get; set; } = "About the company";

    [Required, StringLength(150)]
    public string AboutTitle { get; set; } = "Cleaning built around your routine.";

    [Required, StringLength(1000)]
    public string AboutDescription { get; set; } = "We help families and businesses enjoy well-kept spaces without adding another task to their day. Our team brings dependable processes, thoughtful products, and a commitment to doing the job right.";

    [Required, StringLength(1000)]
    public string AboutSecondaryDescription { get; set; } = "From a regular home refresh to a detailed commercial clean, we tailor every visit to the people and spaces we serve.";

    [Required, StringLength(100)]
    public string ServicesEyebrow { get; set; } = "What we do";

    [Required, StringLength(150)]
    public string ServicesTitle { get; set; } = "Services for every kind of space.";

    [Required, StringLength(100)]
    public string ContactEyebrow { get; set; } = "Getting in touch";

    [Required, StringLength(150)]
    public string ContactTitle { get; set; } = "Let’s make your space shine.";

    [Required, StringLength(1000)]
    public string ContactDescription { get; set; } = "Tell us a little about your space and what you need. We’ll help you find the right cleaning plan.";

    [Required, StringLength(100)]
    public string CeoEyebrow { get; set; } = "Meet our Owner";

    [Required, StringLength(150)]
    public string CeoTitle { get; set; } = "People-first service, from our family to yours.";

    [Required, StringLength(150)]
    public string ChiefExecutiveName { get; set; } = "Ezekiel Velasco";

    [Required, StringLength(150)]
    public string ChiefExecutiveTitle { get; set; } = "Student";

    [Required, StringLength(1000)]
    public string ChiefExecutiveDescription { get; set; } = "Our company was built on a simple belief: excellent service begins with listening. Our CEO leads a team that values trust, professionalism, and the difference a cared-for space can make.";

    [Required, EmailAddress, StringLength(256)]
    public string ContactEmail { get; set; } = string.Empty;

    [Required, Phone, StringLength(30)]
    public string ContactPhone { get; set; } = string.Empty;

    [Required, StringLength(256)]
    public string HomeAddress { get; set; } = string.Empty;

    public ICollection<ServiceSummary> Services { get; set; } = new List<ServiceSummary>();
}

public sealed class ServiceSummary
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(1000)]
    public string Description { get; set; } = string.Empty;

    public int CompanyContentId { get; set; }
    public CompanyContent? CompanyContent { get; set; }
}
