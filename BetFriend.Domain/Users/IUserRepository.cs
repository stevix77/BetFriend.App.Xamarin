namespace BetFriend.Domain.Users
{
    using System.Threading.Tasks;


    public interface IUserRepository
    {
        Task<string> SaveAsync(User user);
    }
}
