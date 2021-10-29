namespace BetFriend.Domain.Users
{
    public class User
    {
        private readonly string _username;
        private readonly string _email;
        private readonly string _password;

        public User(string username, string email, string password)
        {
            _username = username;
            _email = email;
            _password = password;
        }

        public string Username { get => _username; }
        public string Email { get => _email; }
        public string Password { get => _password; }
    }
}
