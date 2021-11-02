namespace BetFriend.Infrastructure.Gateways
{
    using BetFriend.Domain.Users;
    using BetFriend.Domain.Users.Usecases;
    using System.Threading.Tasks;

    public class InMemoryAuthenticationGateway : IAuthenticationGateway
    {
        private readonly Authentication _authentication;

        public InMemoryAuthenticationGateway(Authentication authentication)
        {
            _authentication = authentication;
        }

        public Task<string> GetTokenAsync(string login, string password)
        {
            var token = _authentication.Login.Equals(login)
                      && _authentication.Password.Equals(password)
                      ? _authentication.Token
                      : null;
            return Task.FromResult(token);
        }
    }
}
