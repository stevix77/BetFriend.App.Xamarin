using System;
using System.Collections.Generic;

namespace BetFriend.Domain.Users
{
    public class User
    {
        private readonly Guid _id;
        private readonly string _username;
        private readonly string _email;
        private readonly string _password;
        private decimal _coins;
        private List<Guid> _subscriptions;

        public User(string username, string email, string password)
        {
            _id = Guid.NewGuid();
            _username = username;
            _email = email;
            _password = password;
            _subscriptions = new List<Guid>();
        }

        public Guid Id { get => _id; }
        public string Username { get => _username; }
        public string Email { get => _email; }
        public string Password { get => _password; }
        public decimal Coins { get; set; }
        public IReadOnlyCollection<Guid> Subscriptions { get => _subscriptions; }

        public void AddSubscription(Guid subscriptionId)
        {
            _subscriptions.Add(subscriptionId);
        }

        public void RemoveSubscription(Guid subscriptionId)
        {
            _subscriptions.Remove(subscriptionId);
        }
    }
}
