namespace _241419_DynamicWebsite.Application.Models;

public sealed class ContactRequest
{
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
}
