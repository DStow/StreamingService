using StreamingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingService.Test.MockRepositories
{
    internal class MockUserRepository : StreamingService.Repositories.IUserRepository
    {
        private List<User> users = new List<User>();

        public void Add(User user)
        {
            users.Add(user);
        }

        public bool Exists(string emailAddress)
        {
            return users.Exists(x => x.EmailAddress == emailAddress);
        }

        public IEnumerable<User> GetAll()
        {
            return users.ToList();
        }
    }
}
