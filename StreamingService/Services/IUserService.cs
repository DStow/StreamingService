using System;
using System.Collections.Generic;
using StreamingService.Models;
using StreamingService.Repositories;

namespace StreamingService.Services
{
    public interface IUserService
    {
        bool Subscribe(string emailAddress, Guid subscriptionId);
        
        IEnumerable<User> GetUsers();

        IEnumerable<User> GetUsersWithRemainingSongsThisMonth();

        void ResetRemainingSongsThisMonth();
    }
}
