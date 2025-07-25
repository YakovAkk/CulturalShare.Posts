using CulturalShare.PostWrite.Domain.Context;
using DomainEntity.Entities;

namespace CulturalShare.PostWrite.API.DependencyInjection;

public static class SeedDatabaseExtension
{
    public static void SeedDatabase(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            try
            {
                var dbContextDealerPortal = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                for (int i = 0; i < 10000; i++)
                {
                    dbContextDealerPortal.Posts.Add(new PostEntity()
                    {
                        Caption = i.ToString() + "Caption",
                        Text = string.Empty,
                        UserId = i,
                        Comments = new List<CommentEntity>()
                        {
                            new()
                            {
                                UserId = i,
                                Text = i.ToString() + "Text",
                                IsDeleted = false,
                            }
                        }
                    });

                    dbContextDealerPortal.SaveChanges();

                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
