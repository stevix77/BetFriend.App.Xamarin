namespace BetFriend.Infrastructure
{
    using BetFriend.Domain.Users;
    using BetFriend.Domain.Users.Dto;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;

    public class AuthenticationService : IAuthenticationService
    {
        public string Token { get; private set; }

        public string UserId { get; private set; }
        public string Username { get; private set; }
        private List<Guid> _subscriptions;
        private decimal _coins;

        public AuthenticationService()
        {
            _subscriptions = new List<Guid>();
            _coins = new Random().Next(100, 3600);
        }

        public void AddSubscription(Guid subscriptionId)
        {
            _subscriptions.Add(subscriptionId);
        }

        public IReadOnlyCollection<Guid> GetSubscriptions() => _subscriptions.AsReadOnly();

        public void RemoveSubscription(Guid subscriptionId)
        {
            _subscriptions.Remove(subscriptionId);
        }

        public void SetToken(string token)
        {
            if(string.IsNullOrEmpty(token))
            {
                Clear();
                return;
            }
            token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwidXNlcm5hbWUiOiJzdGV2aXgiLCJpYXQiOjE1MTYyMzkwMjIsImlkIjoiNTFmZWNiMmYtNTVhZC00MzhhLWJhODYtZWM3MDJmMTlmY2VjIn0.7wbhE9XrkS2M5AriO9VVkQZlt3j6bRYiaq3V1ePPqYk";
            Token = token;
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            UserId = "51fecb2f-55ad-438a-ba86-ec702f19fcec";
            Username = "stevix";
            _subscriptions = new List<Guid>();
        }

        private void Clear()
        {
            Token = null;
            UserId = null;
            Username = null;
            _subscriptions = null;
        }

        public void SetInfo(decimal coins, IEnumerable<Guid> subscriptions)
        {
            _coins = coins;
            _subscriptions.AddRange(subscriptions);
        }

        public decimal GetCoins() => _coins;
    }
}
