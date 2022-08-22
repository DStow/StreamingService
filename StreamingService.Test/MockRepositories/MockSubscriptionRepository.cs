using StreamingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingService.Test.MockRepositories
{
    internal class MockSubscriptionRepository : Repositories.ISubscriptionRepository
    {
        List<Subscription> subscriptions = new List<Subscription>();

        public void Add(Subscription subscription)
        {
            subscriptions.Add(subscription);
        }

        public Subscription GetById(Guid id)
        {
            return subscriptions.FirstOrDefault(x => x.Id == id);
        }
    }
}
