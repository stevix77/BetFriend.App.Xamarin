namespace BetFriend.Infrastructure
{
    using BetFriend.Domain.Users;

    public class AuthenticationService : IAuthenticationService
    {
        public string Token { get; private set; }

        public string UserId { get; private set; }
        public string Username { get; private set; }

        public void SetToken(string token)
        {
            throw new System.NotImplementedException();
        }
    }
}
