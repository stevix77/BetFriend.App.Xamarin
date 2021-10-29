namespace BetFriend.Domain.Users.Usecases.Register
{
    public class RegisterCommand
    {
        public string Username { get; }
        public string Email { get; }
        public string Password { get; }

        public RegisterCommand(string username, string email, string password)
        {
            Username = username;
            Email = email;
            Password = password;
        }
    }
}
