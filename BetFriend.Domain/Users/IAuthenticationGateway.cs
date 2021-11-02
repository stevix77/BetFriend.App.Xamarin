using System.Threading.Tasks;

namespace BetFriend.Domain.Users
{
    public interface IAuthenticationGateway
    {
        Task<string> GetTokenAsync(string login, string password);
    }
}
