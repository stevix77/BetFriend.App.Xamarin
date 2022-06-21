namespace BetFriend.Domain.Users
{
    public interface IAuthenticationService
    {
        string Token { get; }
        string UserId { get; }
        void SetToken(string token);
    }
}
