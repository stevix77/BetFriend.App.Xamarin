namespace BetFriend.Infrastructure.Repositories.InMemory
{
    using BetFriend.Domain.Users;
    using BetFriend.Domain.Users.Dto;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class InMemoryUserRepository : IUserRepository
    {
        private readonly string _token;

        public User CurrentUser { get; private set; }
        private readonly ICollection<User> _users;
        private readonly List<UserOutput> _userOutputs;

        public InMemoryUserRepository(string token, List<User> users = null)
        {
            _users = users ?? new List<User>();
            _token = token;
        }

        public InMemoryUserRepository(List<UserOutput> userOutputs)
        {
            _userOutputs = userOutputs;
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

        public Task SubscribeAsync(Guid subscriptionId)
        {
            return Task.CompletedTask;
        }
    }
}
