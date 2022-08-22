using System;

namespace StreamingService.Services
{
    public interface ISubscriptionService
    {
        Models.Subscription GetById(Guid id);
    }
}
