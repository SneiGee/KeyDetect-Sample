using Domain.Subscription;
using Domain.Subscription.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class SubscriptionPlanConfigurations : IEntityTypeConfiguration<SubscriptionPlan>
{
    public void Configure(EntityTypeBuilder<SubscriptionPlan> builder)
    {
        builder.ToTable("SubscriptionPlans");

        builder.HasKey(sp => sp.Id);

        builder.Property(sp => sp.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => SubscriptionPlanId.Create(value));

        builder.Property(sp => sp.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(sp => sp.Price)
            .HasColumnType("decimal(18,2)");

        builder.Property(sp => sp.DurationInDays);
    }
}