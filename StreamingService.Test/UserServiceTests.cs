using StreamingService.Services;
using System;
using Xunit;

namespace StreamingService.Test
{
    /// <summary>
    /// Test suite for <see cref="UserServiceOld"/>.
    /// </summary>
    public class UserServiceTests
    {
        //public readonly UserService SUT = new UserService()

        private readonly SubscriptionService subscriptionService;

        private readonly Guid freemiumGuid = Guid.NewGuid();
        private readonly Guid premiumGuid = Guid.NewGuid();
        private readonly Guid unlimitedGuid = Guid.NewGuid();

        public UserServiceTests()
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

            subscriptionService = new SubscriptionService(mockSubscriptionRepo);
        }

        [Fact]
        public void SubscribeFreemium()
        {
            var mockUserRepo = new MockRepositories.MockUserRepository();
            
            var SUT = new UserService(mockUserRepo, subscriptionService, new StreamingService.Utilities.ConsoleLogger());

            bool result = SUT.Subscribe("Dan_Stow@live.co.uk", freemiumGuid);

            Assert.True(result);
        }

    }
}
