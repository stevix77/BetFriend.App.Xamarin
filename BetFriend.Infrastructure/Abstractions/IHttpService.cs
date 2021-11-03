using System.Threading.Tasks;

namespace BetFriend.Infrastructure.Abstractions
{
    public interface IHttpService
    {
        Task<T> PostAsync<T>(string url, string json, string token = null);
        Task<T> GetAsync<T>(string url, string token = null);
    }
}
