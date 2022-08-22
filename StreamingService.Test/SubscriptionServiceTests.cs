using StreamingService.Services;
using System;
using Xunit;
using System.Linq;

namespace StreamingService.Test
{
    /// <summary>
    /// Test suite for <see cref="SubscriptionService"/>.
    /// </summary>
    public class SubscriptionServiceTests
    {
        private readonly SubscriptionService SUT;

        private readonly Guid freemiumGuid = Guid.NewGuid();
        private readonly Guid premiumGuid = Guid.NewGuid();
        private readonly Guid unlimitedGuid = Guid.NewGuid();

        public SubscriptionServiceTests()
        {
            var mockSubscriptionRepo = new MockRepositories.MockSubscriptionRepository();
            mockSubscriptionRepo.Add(new Models.Subscription()
            {
                Id = freemiumGuid,
                Package = Models.Packages.Freemium,
                Price = 0f
            });
            mockSubscriptionRepo.Add(new Models.Subscription()
            {
                Id = premiumGuid,
                Package = Models.Packages.Premium,
                Price = 4.99f
            });
            mockSubscriptionRepo.Add(new Models.Subscription()
            {
                Id = unlimitedGuid,
                Package = Models.Packages.Unlimitted,
                Price = 9.99f
            });

            SUT = new SubscriptionService(mockSubscriptionRepo);
        }

        [Fact]
        public void GetById()
        {
            var result = SUT.GetById(premiumGuid);

            Assert.Equal(premiumGuid, result.Id);
        }
    }
}
