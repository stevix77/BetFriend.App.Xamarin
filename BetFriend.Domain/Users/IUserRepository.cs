namespace BetFriend.Domain.Users
{
    using BetFriend.Domain.Users.Dto;
    using System;
    using System.Threading.Tasks;


    public interface IUserRepository
    {
        Task<string> SaveAsync(User user);
        Task SubscribeAsync(Guid subscriptionId);
        Task UnsubscribeAsync(Guid subscriptionId);
        Task<UserOutput> GetUserAsync();
    }
}
