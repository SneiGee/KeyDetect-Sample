using Domain.Subscription;

namespace Application.Common.Interfaces.Persistence;

public interface ISubscriptionRepository
{
    void Add(SubscriptionPlan subscriptionPlan);
}