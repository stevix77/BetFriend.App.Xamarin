namespace BetFriend.Domain.Users.Usecases.Register
{
    public class RegisterResponse
    {
        public RegisterResponse(string token, User user)
        {
            Token = token;
            User = user;
        }

        public string Token { get; }
        public User User { get; }
    }
}
