using Application.Subscriptions.Commands.CreateSubscription;
using Contracts.Subscriptions;
using Domain.Subscription;
using Mapster;

namespace Api.Common.Mapping;

public class SubscriptionMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateSubscriptionRequest, CreateSubscriptionCommand>();
        config.NewConfig<SubscriptionPlan, SubscriptionResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);
    }
}