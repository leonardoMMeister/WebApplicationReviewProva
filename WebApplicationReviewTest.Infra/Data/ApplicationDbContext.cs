namespace WebApplicationReviewTest.Infra.Data;

using Microsoft.EntityFrameworkCore;
using WebApplicationReviewTest.Domain.Entities;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Job> Jobs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuração de User
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<User>()
            .Property(u => u.Username)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .HasMaxLength(255)
            .IsRequired();

        modelBuilder.Entity<User>()
            .HasMany(u => u.Jobs)
            .WithOne(j => j.User)
            .HasForeignKey(j => j.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configuração de Job
        modelBuilder.Entity<Job>()
            .HasKey(j => j.Id);

        modelBuilder.Entity<Job>()
            .Property(j => j.Title)
            .HasMaxLength(200)
            .IsRequired();

        modelBuilder.Entity<Job>()
            .Property(j => j.Description)
            .HasMaxLength(2000);

    }
}
