using StreamingService.Models;

namespace StreamingService.Factories
{
    // Moved this logic into a factory as it needs to happen if we are creating a user and removes
    // The responsibility from the UserService.

    // ToDo: Potential here to make this accesible to the testing library to test the free songs logic
    // It's hidden away in here and hard coded :(

    // ToDo: Make this an interface encase we want to branch out to a unlimited or limited user factories
    internal static class UserFactory
    {
        public static User CreateUser(Subscription subscription, string emailAddress)
        {
            var newUser = new User
            {
                EmailAddress = emailAddress,
                SubscriptionId = subscription.Id,
                Subscription = subscription
            };

            // ToDo: The magic "FreeSongs" values should be added into a package database class
            // We could then do some less fixed unit testing on the outcome
            if (subscription.Package == Packages.Freemium)
            {
                newUser.FreeSongs = 3;
                newUser.RemainingSongsThisMonth = newUser.FreeSongs;
            }
            else if (subscription.Package == Packages.Premium)
            {
                newUser.FreeSongs = 3 * 5;
                newUser.RemainingSongsThisMonth = newUser.FreeSongs;
            }
            //// This makes no odds. The Unlimited user does nothing special and is not currently return from the database as such
            //// Ideally added to the database as a limited and unlimited user with a database disciminator?
            //else if (subscription.Package == Packages.Unlimitted)
            //{
            //    user = new UnlimittedUser
            //    {
            //        EmailAddress = emailAddress,
            //        SubscriptionId = subscriptionId,
            //    };
            //}

            return newUser;
        }
    }
}
