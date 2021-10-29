namespace BetFriend.Infrastructure.Repositories.InMemory
{
    using BetFriend.Domain.Users;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class InMemoryUserRepository : IUserRepository
    {
        private readonly string _token;

        public User CurrentUser { get; private set; }
        private readonly ICollection<User> _users;
        public InMemoryUserRepository(string token, List<User> users = null)
        {
            _users = users ?? new List<User>();
            _token = token;
        }

        public Task<string> SaveAsync(User user)
        {
            if (_users.Any(x => x.Username == user.Username
                             || x.Email == user.Email))
                return Task.FromResult(string.Empty);
            _users.Add(user);
            CurrentUser = user;
            return Task.FromResult(_token);
        }
    }
}
