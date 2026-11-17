namespace _241419_DynamicWebsite.Models;

public static class CompanyContentDefaults
{
    public static CompanyContent Create() => new()
    {
        Services =
        [
            new ServiceSummary
            {
                Name = "Residential cleaning",
                Description = "Thoughtful recurring or one-time cleaning that makes your home feel ready for what comes next."
            },
            new ServiceSummary
            {
                Name = "Commercial cleaning",
                Description = "Professional care for offices and business spaces so your team and guests can focus on the work."
            },
            new ServiceSummary
            {
                Name = "Deep cleaning",
                Description = "A detailed reset for the areas that need extra attention, from top to bottom and room to room."
            }
        ]
    };
}
