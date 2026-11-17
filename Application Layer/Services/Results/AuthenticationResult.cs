namespace _241419_DynamicWebsite.Application.Services.Results;

public sealed class AuthenticationResult
{
    public bool Succeeded { get; init; }

    public IReadOnlyList<string> Errors { get; init; } = [];
}
