using _241419_DynamicWebsite.Application.Models;

namespace _241419_DynamicWebsite.Application.Services;

public interface ICompanyContentService
{
    Task<CompanyContent> GetAsync(CancellationToken cancellationToken = default);
}
