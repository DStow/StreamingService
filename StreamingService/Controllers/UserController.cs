using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StreamingService.Models;
using StreamingService.Services;
using System;
using System.Collections.Generic;
using StreamingService.Repositories;

namespace StreamingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserService _userService;

        public UserController(IUserRepository userRepository, ISubscriptionRepository subscriptionRepository)
        {
            _userService = new UserService(userRepository, subscriptionRepository);
        }

        [HttpPost]
        public void Subscribe([FromBody] SubscribeModel model)
        {
            var userService = new UserServiceOld();
            userService.Subscribe(model.EmailAddress, model.SubscriptionId);
        }

        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            var result = _userService.GetUsers();
            return result;
        }

        [HttpGet]
        [Route("withRemainingSongs")]
        public IEnumerable<User> GetUsersWithRemainingSongsThisMonth()
        {
            var result = _userService.GetUsersWithRemainingSongsThisMonth();
            return result;
        }

        [HttpPost]
        [Route("reset")]
        public void ResetRemainingSongsThisMonth()
        {
           // var userService = new UserService(new Repositories.UserRepository(_context));
            // ToDo: Implement this
           // userService.ResetRemainingSongsThisMonth();
        }

        public class SubscribeModel
        {
            public string EmailAddress { get; set; }

            public Guid SubscriptionId { get; set; }
        }
    }
}
