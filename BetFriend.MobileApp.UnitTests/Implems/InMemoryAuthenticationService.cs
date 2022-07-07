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
        private readonly UserOutput _currentUser;

        public InMemoryAuthenticationService(UserOutput currentUser)
        {
            _currentUser = currentUser;
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

        public UserOutput User => _currentUser;

        public void SetToken(string token)
        {
            throw new System.NotImplementedException();
        }
    }
}
