using Domain.Common.Models;
using Domain.Subscription.ValueObjects;

namespace Domain.Subscription;

public sealed class Subscription : Entity<SubscriptionId>
{
    public string AppUserId { get; private set; }
    public SubscriptionPlanId SubscriptionPlanId { get; private set; }
    public SubscriptionPlan SubscriptionPlan { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }

    private Subscription(
        SubscriptionId subscriptionId,
        string appUserId,
        SubscriptionPlanId subscriptionPlanId,
        SubscriptionPlan subscriptionPlan,
        DateTime startDate,
        DateTime endDate) : base(subscriptionId)
    {
        AppUserId = appUserId;
        SubscriptionPlanId = subscriptionPlanId;
        SubscriptionPlan = subscriptionPlan;
        StartDate = startDate;
        EndDate = endDate;
    }

    public static Subscription Create(
        string appUserId,
        SubscriptionPlanId subscriptionPlanId,
        SubscriptionPlan subscriptionPlan,
        DateTime startDate,
        DateTime endDate)
    {
        return new Subscription(
            SubscriptionId.CreateUnique(),
            appUserId,
            subscriptionPlanId,
            subscriptionPlan,
            startDate,
            endDate);
    }

#pragma warning disable CS8618
    private Subscription()
    {
    }
#pragma warning restore CS8618
}