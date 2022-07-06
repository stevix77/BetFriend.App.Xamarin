namespace BetFriend.Infrastructure.Repositories.Http
{
    using BetFriend.Domain.Users;
    using BetFriend.Infrastructure.Abstractions;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System;
    using System.Threading.Tasks;

    public class UserRepository : IUserRepository
    {
        private readonly IHttpService _httpService;
        private const string REGISTER_URL = "api/users/register";
        private readonly string _host;

        public UserRepository(IHttpService httpService, IConfiguration configuration)
        {
            _httpService = httpService;
            _host = configuration["ApiHost"];
        }

        public async Task<string> SaveAsync(User user)
        {
            var jsonUser = JsonConvert.SerializeObject(user, Formatting.Indented, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            var token = await _httpService.PostAsync<string>($"{_host}{REGISTER_URL}", jsonUser).ConfigureAwait(false);
            return token;
        }

        public Task SubscribeAsync(Guid subscriptionId)
        {
            throw new NotImplementedException();
        }
    }
}
