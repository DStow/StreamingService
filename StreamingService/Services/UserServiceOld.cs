//using StreamingService.Models;
//using StreamingService.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace StreamingService.Services
//{
//    public class UserServiceOld
//    {
//        public bool Subscribe(string emailAddress, Guid subscriptionId)
//        {
//            Console.WriteLine(string.Format("Log: Start add user with email '{0}'", emailAddress));

//            if (string.IsNullOrWhiteSpace(emailAddress))
//            {
//                return false;
//            }

//            var context = new Context();
//            var userRepo = new UserRepository(context);

//            if (userRepo.Exists(emailAddress))
//            {
//                return false;
//            }

//            var subscriptionRepository = new SubscriptionRepository(context);

//            var subscrition = subscriptionRepository.GetById(subscriptionId);

//            var user = new User
//            {
//                EmailAddress = emailAddress,
//                SubscriptionId = subscriptionId,
//            };

//            if (subscrition.Package == Packages.Freemium)
//            {
//                user.FreeSongs = 3;
//                user.RemainingSongsThisMonth = user.FreeSongs;
//            }
//            else if (subscrition.Package == Packages.Premium)
//            {
//                user.FreeSongs = 3 * 5;
//                user.RemainingSongsThisMonth = user.FreeSongs;
//            }
//            else if (subscrition.Package == Packages.Unlimitted)
//            {
//                user = new UnlimittedUser
//                {
//                    EmailAddress = emailAddress,
//                    SubscriptionId = subscriptionId,
//                };
//            }

//            userRepo.Add(user);

//            Console.WriteLine(string.Format("Log: End add user with email '{0}'", emailAddress));

//            return true;
//        }

//        public IEnumerable<User> GetUsers()
//        {
//            var context = new Context();
//            var userRepo = new UserRepository(context);
//            return userRepo.GetAll();
//        }

//        public IEnumerable<User> GetUsersWithRemainingSongsThisMonth()
//        {
//            //Todo
//            var context = new Context();
//            var users = context.Users.Where(x => x.RemainingSongsThisMonth > 0).ToList();

//            return users;
//        }

//        /// <summary>
//        /// To be called once per month at a fixed day/time to set every user's 
//        ///  RemainingSongsThisMonth back to their FreeSongs limit.
//        /// </summary>
//        public void ResetRemainingSongsThisMonth()
//        {
//            var context = new Context();
//            var userRepository = new UserRepository(context);
//            foreach (User u in userRepository.GetAll())
//            {
//                u.ResetRemainingSongsThisMonth();
//            }
//            context.SaveChanges();
//        }

//    }



//}
