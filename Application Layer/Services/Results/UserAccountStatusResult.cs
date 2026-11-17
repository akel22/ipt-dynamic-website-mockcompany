namespace _241419_DynamicWebsite.Application.Services.Results;

public sealed class UserAccountStatusResult
{
    public bool Succeeded { get; init; }

    public bool NotFound { get; init; }

    public IReadOnlyList<string> Errors { get; init; } = [];
}
