using CulturalShare.Posts.Data.Entities.NpSqlEntities;
using CulturalShare.PostWrite.Domain.Context;

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
                        CreatedAt = DateTime.UtcNow,
                        ImageUrl = i.ToString() + "Image",
                        Likes = 0,
                        OwnerId = i,
                        Comments = new List<CommentEntity>()
                        {
                            new()
                            {
                                OwnerId = i,
                                Text = i.ToString() + "Text",
                                CreatedAt = DateTime.UtcNow,
                                Username = "test",
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
