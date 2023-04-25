using Application.Subscriptions.Commands.CreateSubscription;
using Contracts.Subscriptions;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/{controller}")]
[AllowAnonymous]
public class SubscriptionsController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public SubscriptionsController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubscription(CreateSubscriptionRequest request)
    {
        var command = _mapper.Map<CreateSubscriptionCommand>(request);

        var createSubscriptionResult = await _mediator.Send(command);

        return createSubscriptionResult.Match(
            subscription => Ok(_mapper.Map<SubscriptionResponse>(subscription)),
            errors => Problem(errors));
    }
}