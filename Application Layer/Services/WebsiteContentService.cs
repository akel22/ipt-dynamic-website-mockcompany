using _241419_DynamicWebsite.DataAccess;
using _241419_DynamicWebsite.DTOs;
using _241419_DynamicWebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace _241419_DynamicWebsite.Application.Services;

public sealed class WebsiteContentService(ApplicationDbContext dbContext) : IWebsiteContentService
{
    public async Task<CompanyContent> GetPublicContentAsync(CancellationToken cancellationToken = default)
    {
        var content = await dbContext.CompanyContents
            .Include(item => item.Services)
            .OrderBy(item => item.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (content is not null)
        {
            return content;
        }

        content = CompanyContentDefaults.Create();//if no content exists, create a default one and save it to the database

        dbContext.CompanyContents.Add(content);
        await dbContext.SaveChangesAsync(cancellationToken);
        return content;
    }

    public async Task<AdminContentDto> GetAdminContentAsync(CancellationToken cancellationToken = default)
    {
        var content = await GetOrCreateContentAsync(cancellationToken);
        return ToAdminContentDto(content);
    }

    public async Task SaveAdminContentAsync(AdminContentDto model, CancellationToken cancellationToken = default)
    {
        var content = await dbContext.CompanyContents
            .Include(item => item.Services)
            .OrderBy(item => item.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (content is null)
        {
            content = new CompanyContent();
            dbContext.CompanyContents.Add(content);
        }

        CopyContent(model, content);

        dbContext.ServiceSummaries.RemoveRange(content.Services);
        content.Services = model.Services.Select(service => new ServiceSummary
        {
            Name = service.Name,
            Description = service.Description
        }).ToList();

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task<CompanyContent> GetOrCreateContentAsync(CancellationToken cancellationToken)
    {
        var content = await dbContext.CompanyContents
            .Include(item => item.Services)
            .OrderBy(item => item.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (content is not null)
        {
            return content;
        }

        content = CompanyContentDefaults.Create();
        dbContext.CompanyContents.Add(content);
        await dbContext.SaveChangesAsync(cancellationToken);
        return content;
    }

    private static AdminContentDto ToAdminContentDto(CompanyContent content) => new()
    {
        Id = content.Id,
        HeroTitle = content.HeroTitle,
        HeroDescription = content.HeroDescription,
        HeroCtaText = content.HeroCtaText,
        HeroCtaTarget = content.HeroCtaTarget,
        AboutEyebrow = content.AboutEyebrow,
        AboutTitle = content.AboutTitle,
        AboutDescription = content.AboutDescription,
        AboutSecondaryDescription = content.AboutSecondaryDescription,
        ServicesEyebrow = content.ServicesEyebrow,
        ServicesTitle = content.ServicesTitle,
        ContactEyebrow = content.ContactEyebrow,
        ContactTitle = content.ContactTitle,
        ContactDescription = content.ContactDescription,
        ContactEmail = content.ContactEmail,
        ContactPhone = content.ContactPhone,
        CeoEyebrow = content.CeoEyebrow,
        CeoTitle = content.CeoTitle,
        ChiefExecutiveName = content.ChiefExecutiveName,
        ChiefExecutiveTitle = content.ChiefExecutiveTitle,
        ChiefExecutiveDescription = content.ChiefExecutiveDescription,
        Services = content.Services.OrderBy(service => service.Id).Select(service => new AdminServiceContentDto
        {
            Id = service.Id,
            Name = service.Name,
            Description = service.Description
        }).ToList()
    };

    private static void CopyContent(AdminContentDto model, CompanyContent content)
    {
        content.HeroTitle = model.HeroTitle;
        content.HeroDescription = model.HeroDescription;
        content.HeroCtaText = model.HeroCtaText;
        content.HeroCtaTarget = model.HeroCtaTarget;
        content.AboutEyebrow = model.AboutEyebrow;
        content.AboutTitle = model.AboutTitle;
        content.AboutDescription = model.AboutDescription;
        content.AboutSecondaryDescription = model.AboutSecondaryDescription;
        content.ServicesEyebrow = model.ServicesEyebrow;
        content.ServicesTitle = model.ServicesTitle;
        content.ContactEyebrow = model.ContactEyebrow;
        content.ContactTitle = model.ContactTitle;
        content.ContactDescription = model.ContactDescription;
        content.ContactEmail = model.ContactEmail;
        content.ContactPhone = model.ContactPhone;
        content.CeoEyebrow = model.CeoEyebrow;
        content.CeoTitle = model.CeoTitle;
        content.ChiefExecutiveName = model.ChiefExecutiveName;
        content.ChiefExecutiveTitle = model.ChiefExecutiveTitle;
        content.ChiefExecutiveDescription = model.ChiefExecutiveDescription;
    }
}
