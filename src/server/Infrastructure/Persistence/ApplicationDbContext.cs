using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Post> Posts => Set<Post>();

    public DbSet<Comment> Comments => Set<Comment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(post => post.Id);

            entity.Property(post => post.Title)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(post => post.ImageUrl)
                .HasMaxLength(500);

            entity.Property(post => post.CreatedBy)
                .HasMaxLength(100);

            entity.Property(post => post.CreatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(comment => comment.Id);

            entity.Property(comment => comment.CommentBy)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(comment => comment.Message)
                .HasMaxLength(1000)
                .IsRequired();

            entity.Property(comment => comment.CreatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            entity.HasOne(comment => comment.Post)
                .WithMany(post => post.Comments)
                .HasForeignKey(comment => comment.PostId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_Comments_Posts");
        });
    }
}
