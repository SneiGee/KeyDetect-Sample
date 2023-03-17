using Domain.Common.Models;

namespace Domain.Subscription.ValueObjects;

public sealed class SubscriptionPlanId : ValueObject
{
    public Guid Value { get; }

    private SubscriptionPlanId(Guid value)
    {
        Value = value;
    }

    public static SubscriptionPlanId CreateUnique()
    {
        // TODO: enforce invariants
        return new SubscriptionPlanId(Guid.NewGuid());
    }

    public static SubscriptionPlanId Create(Guid value)
    {
        // TODO: enforce invariants
        return new SubscriptionPlanId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}