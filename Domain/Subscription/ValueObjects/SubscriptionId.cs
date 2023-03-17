using Domain.Common.Models;

namespace Domain.Subscription.ValueObjects;

public sealed class SubscriptionId : ValueObject
{
    public Guid Value { get; }

    private SubscriptionId(Guid value)
    {
        Value = value;
    }

    public static SubscriptionId CreateUnique()
    {
        // TODO: enforce invariants
        return new SubscriptionId(Guid.NewGuid());
    }

    public static SubscriptionId Create(Guid value)
    {
        // TODO: enforce invariants
        return new SubscriptionId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}