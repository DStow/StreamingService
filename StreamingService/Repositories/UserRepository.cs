using StreamingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StreamingService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public bool Exists(string emailAddress)
        {
            var result = this._context.Users.Any(x => x.EmailAddress == emailAddress);
            return result;
        }

        public IEnumerable<User> GetAll()
        {
            var result = this._context.Users.ToList();
            return result;
        }

        public void Add(User user)
        {
            this._context.Users.Add(user);
            this._context.SaveChanges();
        }
    }
}
