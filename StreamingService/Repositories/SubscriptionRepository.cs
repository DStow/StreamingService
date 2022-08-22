using StreamingService.Models;
using System;
using System.Linq;

namespace StreamingService.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly Context _context;

        public SubscriptionRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Subscription GetById(Guid id)
        {
            var result = this._context.Subscriptions.FirstOrDefault(x => x.Id == id);
            return result;
        }
    }
}
