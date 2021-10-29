namespace BetFriend.Domain.Users
{
    public interface IHashPassword
    {
        string Hash(string password);
    }
}
