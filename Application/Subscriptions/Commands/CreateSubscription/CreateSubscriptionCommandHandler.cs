using Application.Common.Interfaces.Persistence;

using Domain.Subscription;
using ErrorOr;
using MediatR;

namespace Application.Subscriptions.Commands.CreateSubscription;

public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, ErrorOr<SubscriptionPlan>>
{
    private readonly ISubscriptionRepository _subscriptionRepository;

    public CreateSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<ErrorOr<SubscriptionPlan>> Handle(CreateSubscriptionCommand request,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var subscription = SubscriptionPlan.Create(
            name: request.Name,
            price: request.Price,
            durationInDays: request.DurationInDays
        );

        _subscriptionRepository.Add(subscription);

        return subscription;
    }
}