using _241419_DynamicWebsite.Application.Models;

namespace _241419_DynamicWebsite.Application.Services;

public interface IContactService
{
    Task SubmitAsync(ContactRequest request, CancellationToken cancellationToken = default);
}
