using StreamingService.Models;
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

        public UserService(IUserRepository userRepository, ISubscriptionRepository subscriptionRepository)
        {
            _userRepository = userRepository;
            _subscriptionRepository = subscriptionRepository;
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetAll();
        }

        public IEnumerable<User> GetUsersWithRemainingSongsThisMonth()
        {
            var results = GetUsers();
            results = results.Where(x => x.RemainingSongsThisMonth > 0);
            return results;
        }

        public bool Subscribe(string emailAddress, Guid subscriptionId)
        {
            Console.WriteLine(string.Format("Log: Start add user with email '{0}'", emailAddress));

            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                return false;
            }

            if (_userRepository.Exists(emailAddress))
            {
                return false;
            }

            var subscription = _subscriptionRepository.GetById(subscriptionId);

            var user = new User
            {
                EmailAddress = emailAddress,
                SubscriptionId = subscriptionId,
            };

            if (subscription.Package == Packages.Freemium)
            {
                user.FreeSongs = 3;
                user.RemainingSongsThisMonth = user.FreeSongs;
            }
            else if (subscription.Package == Packages.Premium)
            {
                user.FreeSongs = 3 * 5;
                user.RemainingSongsThisMonth = user.FreeSongs;
            }
            else if (subscription.Package == Packages.Unlimitted)
            {
                user = new UnlimittedUser
                {
                    EmailAddress = emailAddress,
                    SubscriptionId = subscriptionId,
                };
            }

            _userRepository.Add(user);

            Console.WriteLine(string.Format("Log: End add user with email '{0}'", emailAddress));

            return true;
        }
    }
}
