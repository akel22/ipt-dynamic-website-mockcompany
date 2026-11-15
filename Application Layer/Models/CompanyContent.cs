namespace _241419_DynamicWebsite.Application.Models;

public sealed class CompanyContent
{
    public string CompanyName { get; init; } = string.Empty;
    public string HeroTitle { get; init; } = string.Empty;
    public string HeroDescription { get; init; } = string.Empty;
    public string AboutDescription { get; init; } = string.Empty;
    public IReadOnlyCollection<ServiceSummary> Services { get; init; } = [];
    public string ContactDescription { get; init; } = string.Empty;
    public string ChiefExecutiveDescription { get; init; } = string.Empty;
}

public sealed class ServiceSummary
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}
