using System;
using StreamingService.Models;

namespace StreamingService.Repositories
{
    public interface ISubscriptionRepository
    {
        Subscription GetById(Guid id);
    }
}
