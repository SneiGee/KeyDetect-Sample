using Domain.Common.Models;
using Domain.Subscription.ValueObjects;

namespace Domain.Subscription;

public sealed class SubscriptionPlan : Entity<SubscriptionPlanId>
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int DurationInDays { get; set; }

    private SubscriptionPlan(
        SubscriptionPlanId subscriptionPlanId,
        string name,
        decimal price,
        int durationInDays) : base(subscriptionPlanId)
    {
        Name = name;
        Price = price;
        DurationInDays = durationInDays;
    }

    public static SubscriptionPlan Create(
        string name,
        decimal price,
        int durationInDays)
    {
        return new SubscriptionPlan(
            SubscriptionPlanId.CreateUnique(),
            name,
            price,
            durationInDays);
    }

#pragma warning disable CS8618
    private SubscriptionPlan()
    {
    }
#pragma warning restore CS8618
}