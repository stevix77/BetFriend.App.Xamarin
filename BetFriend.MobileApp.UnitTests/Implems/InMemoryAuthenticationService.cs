using BetFriend.Domain.Users;
using BetFriend.Domain.Users.Dto;
using System;
using System.Collections.Generic;

namespace BetFriend.MobileApp.UnitTests.Implems
{
    public class InMemoryAuthenticationService : IAuthenticationService
    {
        private readonly string _userId;
        private readonly string _username;
        private readonly ICollection<Guid> _subscription;

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

        public string Token => throw new System.NotImplementedException();


        public void AddSubscription(Guid subscriptionId)
        {
            _subscription.Add(subscriptionId);
        }

        public IReadOnlyCollection<Guid> GetSubscriptions()
        {
            return new List<Guid>(_subscription);
        }

        public void RemoveSubscription(Guid subscriptionId)
        {
            _subscription.Remove(subscriptionId);
        }

        public void SetToken(string token)
        {
            throw new System.NotImplementedException();
        }
    }
}
