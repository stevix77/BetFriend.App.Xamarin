namespace BetFriend.Infrastructure.Repositories.Http
{
    using BetFriend.Domain.Users;
    using BetFriend.Infrastructure.Abstractions;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System.Threading.Tasks;

    public class UserRepository : IUserRepository
    {
        private readonly IHttpService _httpService;
        private const string REGISTER_URL = "https://betfriend-dev.azurewebsites.net/api/users/register";

        public UserRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<string> SaveAsync(User user)
        {
            var jsonUser = JsonConvert.SerializeObject(user, Formatting.Indented, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            var token = await _httpService.PostAsync<string>(REGISTER_URL, jsonUser).ConfigureAwait(false);
            return token;
        }
    }
}
