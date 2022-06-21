using BetFriend.Domain.Users;

namespace BetFriend.MobileApp.UnitTests.Implems
{
    public class InMemoryAuthenticationService : IAuthenticationService
    {
        private readonly string _userId;
        private readonly string _username;

        public InMemoryAuthenticationService(string userId, string username)
        {
            _userId = userId;
            _username = username;
        }

        public string UserId { get => _userId; }
        public string Username { get => _username; }

        public string Token => throw new System.NotImplementedException();

        public void SetToken(string token)
        {
            throw new System.NotImplementedException();
        }

        public void SetUser(string username, string token, string userid)
        {
            throw new System.NotImplementedException();
        }
    }
}
