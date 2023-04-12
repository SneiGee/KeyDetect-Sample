namespace Contracts.Subscriptions;

public record SubscriptionResponse(
    string Id,
    string Name,
    decimal Price,
    int DurationInDays);