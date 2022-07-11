using BetFriend.Domain.Users;
using System;
using System.Collections.Generic;

namespace BetFriend.MobileApp.UnitTests.Implems
{
    public class InMemoryAuthenticationService : IAuthenticationService
    {
        private readonly string _userId;
        private readonly string _username;
        private readonly ICollection<Guid> _subscription;
        private decimal _coins;

        public InMemoryAuthenticationService(User currentUser)
        {
            _userId = currentUser.Id.ToString();
            _username = currentUser.Username;
            _subscription = new List<Guid>(currentUser.Subscriptions);
        }

        public InMemoryAuthenticationService(string userId, string username)
        {
            _userId = userId;
            _username = username;
            _subscription = new List<Guid>();
        }

        public string UserId { get => _userId; }
        public string Username { get => _username; }

        public string Token => throw new NotImplementedException();


        public void AddSubscription(Guid subscriptionId)
        {
            _subscription.Add(subscriptionId);
        }

        public decimal GetCoins() => _coins;

        public IReadOnlyCollection<Guid> GetSubscriptions()
        {
            return new List<Guid>(_subscription);
        }

        public void RemoveSubscription(Guid subscriptionId)
        {
            _subscription.Remove(subscriptionId);
        }

        public void SetInfo(decimal coins, IEnumerable<Guid> subscriptions)
        {
            foreach (var subscription in subscriptions)
                _subscription.Add(subscription);
            _coins = coins;
        }

        public void SetToken(string token)
        {
            throw new System.NotImplementedException();
        }
    }
}
