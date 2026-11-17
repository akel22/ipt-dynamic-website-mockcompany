using _241419_DynamicWebsite.DTOs;
using _241419_DynamicWebsite.Models;

namespace _241419_DynamicWebsite.Application.Services;

public interface IWebsiteContentService
{
    Task<CompanyContent> GetPublicContentAsync(CancellationToken cancellationToken = default);

    Task<AdminContentDto> GetAdminContentAsync(CancellationToken cancellationToken = default);

    Task SaveAdminContentAsync(AdminContentDto model, CancellationToken cancellationToken = default);
}
