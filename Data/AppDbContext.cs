using Microsoft.EntityFrameworkCore;
using QuickShiftZA.Api.Models;

namespace QuickShiftZA.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<WorkerProfile> WorkerProfiles => Set<WorkerProfile>();
    public DbSet<Gig> Gigs => Set<Gig>();
    public DbSet<Proposal> Proposals => Set<Proposal>();
    public DbSet<Rating> Ratings => Set<Rating>();
    public DbSet<PriceBenchmark> PriceBenchmarks => Set<PriceBenchmark>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<WorkerProfile>()
            .HasOne(w => w.User)
            .WithMany()
            .HasForeignKey(w => w.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Gig>()
            .HasOne(g => g.Customer)
            .WithMany()
            .HasForeignKey(g => g.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Proposal>()
            .HasOne(p => p.Gig)
            .WithMany()
            .HasForeignKey(p => p.GigId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Proposal>()
            .HasOne(p => p.Worker)
            .WithMany()
            .HasForeignKey(p => p.WorkerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
