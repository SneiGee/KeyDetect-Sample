using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts.Subscriptions;

public record CreateSubscriptionRequest(
    string Name,
    decimal Price,
    int DurationInDays);