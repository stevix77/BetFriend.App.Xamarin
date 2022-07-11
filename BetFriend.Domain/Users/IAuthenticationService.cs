using BetFriend.Domain.Users.Dto;
using System;
using System.Collections.Generic;

namespace BetFriend.Domain.Users
{
    public interface IAuthenticationService
    {
        string Token { get; }
        string UserId { get; }
        string Username { get; }
        IReadOnlyCollection<Guid> GetSubscriptions();
        void AddSubscription(Guid subscriptionId);
        void RemoveSubscription(Guid subscriptionId);
        void SetToken(string token);
        void SetInfo(decimal coins, IEnumerable<Guid> subscriptions);
        decimal GetCoins();
    }
}
