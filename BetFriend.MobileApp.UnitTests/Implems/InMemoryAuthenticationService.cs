using BetFriend.Domain.Users;
using BetFriend.Domain.Users.Dto;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BetFriend.MobileApp.UnitTests.Implems
{
    public class InMemoryAuthenticationService : IAuthenticationService
    {
        private readonly string _userId;
        private readonly string _username;
        private ICollection<Guid> _subscription;

        public InMemoryAuthenticationService(string userId, string username)
        {
            _userId = userId;
            _username = username;
            _subscription = new List<Guid>();
        }

        public string UserId { get => _userId; }
        public string Username { get => _username; }

        public string Token => throw new System.NotImplementedException();

        public UserOutput User => throw new NotImplementedException();

        public void SetToken(string token)
        {
            throw new System.NotImplementedException();
        }
    }
}
