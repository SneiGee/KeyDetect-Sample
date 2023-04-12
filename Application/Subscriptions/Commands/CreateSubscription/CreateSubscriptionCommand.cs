using Domain.Subscription;
using ErrorOr;
using MediatR;

namespace Application.Subscriptions.Commands.CreateSubscription;

public record CreateSubscriptionCommand(
    string Name,
    decimal Price,
    int DurationInDays) : IRequest<ErrorOr<SubscriptionPlan>>;