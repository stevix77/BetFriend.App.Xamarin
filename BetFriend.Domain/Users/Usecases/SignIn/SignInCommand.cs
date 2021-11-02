namespace BetFriend.Domain.Users.Usecases.SignIn
{
    public class SignInCommand
    {
        public SignInCommand(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public string Login { get; }
        public string Password { get; }
    }
}
