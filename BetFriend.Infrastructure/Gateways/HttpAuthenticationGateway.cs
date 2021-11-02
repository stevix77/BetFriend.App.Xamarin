using BetFriend.Domain.Users;
using BetFriend.Infrastructure.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;

namespace BetFriend.Infrastructure.Gateways
{
    public class HttpAuthenticationGateway : IAuthenticationGateway
    {
        private readonly IHttpService _httpService;
        private const string SIGNIN_URL = "https://betfriend-dev.azurewebsites.net/api/users/signin";

        public HttpAuthenticationGateway(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<string> GetTokenAsync(string login, string password)
        {
            var json = JsonConvert.SerializeObject(
                    new { Login = login, Password = password }, 
                    Formatting.Indented, 
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });
            var token = await _httpService.PostAsync<string>(SIGNIN_URL, json).ConfigureAwait(false);
            return token;
        }
    }
}
