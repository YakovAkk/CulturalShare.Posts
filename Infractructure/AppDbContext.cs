using Microsoft.EntityFrameworkCore;

namespace CulturalShare.PostWrite.Domain.Context;

public class AppDbContext : DbContext
{
    public DbSet<Posts.Data.Entities.NpSqlEntities.CommentEntity> Comments { get; set; }
    public DbSet<Posts.Data.Entities.NpSqlEntities.PostEntity> Posts { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        try
        {
            Database.EnsureCreated();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Posts.Data.Entities.NpSqlEntities.PostEntity>().ToTable("posts");
        modelBuilder.Entity<Posts.Data.Entities.NpSqlEntities.CommentEntity>().ToTable("comments");

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        base.OnModelCreating(modelBuilder);
    }
}
