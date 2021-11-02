namespace BetFriend.MobileApp.UnitTests.Implems
{
    using BetFriend.Domain.Users;


    internal class FakeHashPassword : IHashPassword
    {
        public FakeHashPassword()
        {
        }

        public string Hash(string password)
        {
            return password + password;
        }
    }
}
