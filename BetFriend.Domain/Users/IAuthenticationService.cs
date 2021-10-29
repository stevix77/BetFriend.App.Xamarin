namespace BetFriend.Domain.Users
{
    public interface IAuthenticationService
    {
        void SetToken(string token);
        string Token { get; }
    }
}
