using CloudIn.Domains.Data.Models;

namespace CloudIn.Domains.Data.Extensions;

public static class DataSeeder
{
    public static IApplicationBuilder SeedDatabase(this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateAsyncScope())
        {
            var context = serviceScope.ServiceProvider.GetService<DataContext>();

            context?.ApplySeedAsync();
        }

        return app;
    }

    public async static Task ApplySeedAsync(this DataContext context)
    {
        await context.Folders.AddRangeAsync(
            new FolderModel
            {
                Id = Guid.NewGuid(),
                Name = "Documents",
                Files = new List<FileModel>
                {
                    new FileModel { Id = Guid.NewGuid(), Name = "Payments.pdf", MimeType = "application/pdf", PhysicalPath = "/Payments.pdf" },
                },
                Folders = new List<FolderModel>
                {
                    new FolderModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "Lessons",
                        Files = new List<FileModel>
                        {
                            new FileModel { Id = Guid.NewGuid(), Name = "MyLesson1.odt", MimeType = "application/odt", PhysicalPath = "/MyLesson1.odt" }
                        }
                    }
                }
            }
        );

        await context.SaveChangesAsync();
    }
}