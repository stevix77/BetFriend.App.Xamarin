namespace BetFriend.Infrastructure
{
    using BetFriend.Domain.Users;

    public class AuthenticationService : IAuthenticationService
    {
        public string Token { get; private set; }

        public string UserId => throw new System.NotImplementedException();

        public void SetToken(string token)
        {
            Token = token;
        }
    }
}
