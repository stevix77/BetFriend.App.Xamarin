namespace BetFriend.Domain.Users.Usecases
{
    public class Authentication
    {
        public Authentication(string login, string password, string token)
        {
            Login = login;
            Password = password;
            Token = token;
        }

        public string Login { get; }
        public string Password { get; }
        public string Token { get; }
    }
}
