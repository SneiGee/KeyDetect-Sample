using Domain.Identity;
using Domain.Subscription;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class KeyDetectDbContext : IdentityDbContext<AppUser>
{
    public KeyDetectDbContext(DbContextOptions<KeyDetectDbContext> options) : base(options)
    {
    }

    public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; } = null!;
    public DbSet<Subscription> Subscriptions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
         modelBuilder.ApplyConfigurationsFromAssembly(
             typeof(KeyDetectDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}