using Application.Common.Interfaces.Persistence;
using Domain.Subscription;

namespace Infrastructure.Persistence.Repositories;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly KeyDetectDbContext _keyDetectDbContext;

    public SubscriptionRepository(KeyDetectDbContext keyDetectDbContext)
    {
        _keyDetectDbContext = keyDetectDbContext;
    }

    public void Add(SubscriptionPlan subscriptionPlan)
    {
        _keyDetectDbContext.Add(subscriptionPlan);
        _keyDetectDbContext.SaveChanges();
    }
}