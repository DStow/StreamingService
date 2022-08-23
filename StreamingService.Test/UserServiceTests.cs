using StreamingService.Services;
using System;
using Xunit;
using System.Linq;

namespace StreamingService.Test
{
    /// <summary>
    /// Test suite for <see cref="UserService"/>.
    /// </summary>
    public class UserServiceTests
    {
        //public readonly UserService SUT = new UserService()

        private readonly ISubscriptionService subscriptionService;

        private readonly Guid freemiumGuid = Guid.NewGuid();
        private readonly Guid premiumGuid = Guid.NewGuid();
        private readonly Guid unlimitedGuid = Guid.NewGuid();

        public UserServiceTests()
        {
            // Create a mock subscriptions repo that we can add some subscriptions too
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

        [Fact]
        public void SubscribePremium()
        {
            var mockUserRepo = new MockRepositories.MockUserRepository();

            var SUT = new UserService(mockUserRepo, subscriptionService, new StreamingService.Utilities.ConsoleLogger());

            bool result = SUT.Subscribe("Dan_Stow@live.co.uk", premiumGuid);

            Assert.True(result);
        }

        [Fact]
        public void SubscribeUnlimited()
        {
            var mockUserRepo = new MockRepositories.MockUserRepository();

            var SUT = new UserService(mockUserRepo, subscriptionService, new StreamingService.Utilities.ConsoleLogger());

            bool result = SUT.Subscribe("Dan_Stow@live.co.uk", unlimitedGuid);

            Assert.True(result);
        }

        [Fact]
        public void SubscribeEmptyEmail()
        {
            var mockUserRepo = new MockRepositories.MockUserRepository();

            var SUT = new UserService(mockUserRepo, subscriptionService, new StreamingService.Utilities.ConsoleLogger());

            bool result = SUT.Subscribe("", freemiumGuid);

            Assert.False(result);
        }

        [Fact]
        public void SubscribeDuplicateEmail()
        {
            var mockUserRepo = new MockRepositories.MockUserRepository();

            var SUT = new UserService(mockUserRepo, subscriptionService, new StreamingService.Utilities.ConsoleLogger());

            SUT.Subscribe("Test@test", freemiumGuid);

            // Attempt to subscribe with the same email
            bool result = SUT.Subscribe("Test@test", premiumGuid);

            Assert.False(result);
        }

        [Fact]
        public void GetUsers_MultipleUsers()
        {
            var mockUserRepo = new MockRepositories.MockUserRepository();

            var SUT = new UserService(mockUserRepo, subscriptionService, new StreamingService.Utilities.ConsoleLogger());
            SUT.Subscribe("DS@test.com", freemiumGuid);
            SUT.Subscribe("DS@test2.com", premiumGuid);


            var allUsers = SUT.GetUsers();

            int expectedResult = 2;

            Assert.Equal(expectedResult, allUsers.Count());
        }

        [Fact]
        public void GetUsers_NoUsers()
        {
            var mockUserRepo = new MockRepositories.MockUserRepository();

            var SUT = new UserService(mockUserRepo, subscriptionService, new StreamingService.Utilities.ConsoleLogger());

            var allUsers = SUT.GetUsers();
            int expectedResult = 0;

            Assert.Equal(expectedResult, allUsers.Count());
        }

        [Fact]
        public void GetUsersWithRemainingSongsThisMonth()
        {
            // We want to make sure that unlimited users are being included here
            var mockUserRepo = new MockRepositories.MockUserRepository();

            var SUT = new UserService(mockUserRepo, subscriptionService, new StreamingService.Utilities.ConsoleLogger());

            SUT.Subscribe("Free@test.com", freemiumGuid);
            SUT.Subscribe("Unlimited@test.com", unlimitedGuid);

            var allUsers = SUT.GetUsersWithRemainingSongsThisMonth();
            int expectedResult = 2;

            Assert.Equal(expectedResult, allUsers.Count());
        }

        // ToDo: After extracting the Free songs value from a hardcoded value to a database value, can mock and test the calculations
        // are happening correctly
    }
}
