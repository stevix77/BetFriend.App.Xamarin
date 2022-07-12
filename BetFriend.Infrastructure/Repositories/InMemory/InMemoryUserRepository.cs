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
        private readonly ICollection<User> _users;
        private readonly User _currentUser;

        public InMemoryUserRepository(string token, List<User> users = null)
        {
            _users = users ?? new List<User>();
            _token = token;
        }

        public InMemoryUserRepository(User user, decimal? coins = null, IEnumerable<Guid>? subscriptions = null)
        {
            _currentUser = user;
            _currentUser.Coins = coins.GetValueOrDefault();
            if (subscriptions != null)
                foreach (var item in subscriptions)
                    _currentUser.AddSubscription(item);
        }

        public Task<string> SaveAsync(User user)
        {
            if (_users.Any(x => x.Username == user.Username
                             || x.Email == user.Email))
                return Task.FromResult(string.Empty);
            _users.Add(user);
            return Task.FromResult(_token);
        }

        public Task SubscribeAsync(Guid subscriptionId)
        {
            _currentUser.AddSubscription(subscriptionId);
            return Task.CompletedTask;
        }

        public Task UnsubscribeAsync(Guid subscriptionId)
        {
            _currentUser.RemoveSubscription(subscriptionId);
            return Task.CompletedTask;
        }

        public Task<UserOutput> GetUserAsync()
        {
            if(_currentUser is null)
                return Task.FromResult<UserOutput>(null);

            return Task.FromResult(new UserOutput(_currentUser));
        }
    }
}
