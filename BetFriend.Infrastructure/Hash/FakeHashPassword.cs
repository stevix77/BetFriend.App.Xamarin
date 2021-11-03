namespace BetFriend.Infrastructure.Hash
{
    using BetFriend.Domain.Users;


    public class FakeHashPassword : IHashPassword
    {

        public string Hash(string password)
        {
            return password + password;
        }
    }
}
