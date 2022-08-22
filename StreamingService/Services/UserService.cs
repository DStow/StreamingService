using StreamingService.Models;
using StreamingService.Utilities;
using System;
using System.Collections.Generic;
using StreamingService.Repositories;
using System.Linq;

namespace StreamingService.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private ISubscriptionService _subscriptionService;
        private ILogger _logger;

        public UserService(IUserRepository userRepository, ISubscriptionService subscriptionService, ILogger logger)
        {
            _userRepository = userRepository;
            _subscriptionService = subscriptionService;
            _logger = logger;
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetAll();
        }

        public IEnumerable<User> GetUsersWithRemainingSongsThisMonth()
        {
            var results = GetUsers();
            // This now includes a check for unlimitted users
            results = results.Where(x => x.RemainingSongsThisMonth > 0 || x.Subscription.Package == Packages.Unlimitted);
            return results;
        }

        public bool Subscribe(string emailAddress, Guid subscriptionId)
        {
            // This function is quite long, best to cut it down to more bitesize chunks
            // for better readability

            // Also implemented the ILogger, encase we want to swap this to a text file log or other later
            _logger.Log(string.Format("Start add user with email '{0}'", emailAddress));

            // Extracted this part of the function
            if (ValidateIncomingUserDetails(emailAddress) == false)
            {
                return false;
            }

            // This is now called from a service. Lateralling moving into a service rather than down into another
            // models repository
            var subscription = _subscriptionService.GetById(subscriptionId);

            // Also extracted this part of the function
            User newUser = CreateNewUser(subscription, emailAddress);

            _userRepository.Add(newUser);

            _logger.Log(string.Format("End add user with email '{0}'", emailAddress));

            return true;
        }

        // ToDo: This should be a scheduled service outside of this website.
        // Beyond the scope of the streaming service and is currently exposed to any user
        // to call from the controller (Appreciate this is just a mock test)
        /// <summary>
        /// To be called once per month at a fixed day/time to set every user's 
        ///  RemainingSongsThisMonth back to their FreeSongs limit.
        /// </summary>
        public void ResetRemainingSongsThisMonth()
        {
            var context = new Context();
            var userRepository = new UserRepository(context);
            foreach (User u in userRepository.GetAll())
            {
                u.ResetRemainingSongsThisMonth();
            }
            context.SaveChanges();
        }

        private bool ValidateIncomingUserDetails(string emailAddress)
        {
            // This class has been seperated out.
            // Would need to consider if this is the sole place a user can be created...
            // ... and would need their details checked
            bool result = true;

            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                return false;
            }

            if (_userRepository.Exists(emailAddress))
            {
                return false;
            }

            return result;
        }

        private User CreateNewUser(Subscription subscription, string emailAddress)
        {
            var newUser = new User
            {
                EmailAddress = emailAddress,
                SubscriptionId = subscription.Id,
                Subscription = subscription
            };

            // ToDo: The magic "FreeSongs" values should be added into a package database class
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
