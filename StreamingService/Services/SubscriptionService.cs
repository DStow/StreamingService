using StreamingService.Models;
using System;
using StreamingService.Repositories;

namespace StreamingService.Services
{
    // This class is meant to act as a dependency inversion layer to stop user server reaching down into the subscription repository
    // And leave itself ready for further extension in the future
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionService;
        public SubscriptionService(Repositories.ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionService = subscriptionRepository;
        }

        public Subscription GetById(Guid id)
        {
            return _subscriptionService.GetById(id);
        }
    }
}
