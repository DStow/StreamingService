using System.Collections.Generic;
using System;

namespace StreamingService.Repositories
{
    public interface IUserRepository
    {
        bool Exists(string emailAddress);

        IEnumerable<Models.User> GetAll();

        void Add(Models.User user);
    }
}
