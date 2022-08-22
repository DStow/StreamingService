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
        private ISubscriptionRepository _subscriptionRepository;
        private ILogger _logger;

        public UserService(IUserRepository userRepository, ISubscriptionRepository subscriptionRepository, ILogger logger)
        {
            _userRepository = userRepository;
            _subscriptionRepository = subscriptionRepository;
            _logger = logger;
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetAll();
        }

        public IEnumerable<User> GetUsersWithRemainingSongsThisMonth()
        {
            var results = GetUsers();
            // Include a check for unlimited users
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

            // Should this be from the subscription service? Rather than directly into the repository?
            var subscription = _subscriptionRepository.GetById(subscriptionId);

            // Also extracted this part of the function
            User newUser = CreateNewUser(subscription, emailAddress);

            _userRepository.Add(newUser);

            _logger.Log(string.Format("End add user with email '{0}'", emailAddress));

            return true;
        }

        private bool ValidateIncomingUserDetails(string emailAddress)
        {
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
        }
    }
}
