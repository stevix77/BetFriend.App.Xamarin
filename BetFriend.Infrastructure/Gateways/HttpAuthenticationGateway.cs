using BetFriend.Domain.Users;
using BetFriend.Infrastructure.Abstractions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;

namespace BetFriend.Infrastructure.Gateways
{
    public class HttpAuthenticationGateway : IAuthenticationGateway
    {
        private readonly IHttpService _httpService;
        private readonly string _host;
        private const string SIGNIN_URL = "api/users/signin";

        public HttpAuthenticationGateway(IHttpService httpService, IConfiguration configuration)
        {
            _httpService = httpService;
            _host = configuration["ApiHost"];
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
            var token = await _httpService.PostAsync<string>($"{_host}{SIGNIN_URL}", json).ConfigureAwait(false);
            return token;
        }
    }
}
