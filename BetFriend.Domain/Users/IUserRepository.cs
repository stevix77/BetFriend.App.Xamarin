namespace BetFriend.Domain.Users
{
    using System;
    using System.Threading.Tasks;


    public interface IUserRepository
    {
        Task<string> SaveAsync(User user);
        Task SubscribeAsync(Guid subscriptionId);
    }
}
