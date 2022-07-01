using BetFriend.Domain.Bets;
using BetFriend.Domain.Bets.Dto;
using BetFriend.Domain.Bets.Usecases.UpdateBet;
using BetFriend.Domain.Users;
using BetFriend.Infrastructure.Abstractions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BetFriend.Infrastructure.Repositories.Http
{
    public class BetRepository : IBetRepository
    {
        private readonly IHttpService _httpService;
        private readonly string _host;
        private readonly IAuthenticationService _authenticationService;

        private const string CREATE_BET_URL = "api/bets/launch";
        private const string RETRIEVE_BETS_URL = "api/bets";
        private const string RETRIEVE_BET_URL = "api/bets/{0}";

        public BetRepository(IHttpService httpService,
                             IAuthenticationService authenticationService,
                             IConfiguration configuration)
        {
            _httpService = httpService;
            _authenticationService = authenticationService;
            _host = configuration["ApiHost"];
        }

        public async Task<BetOutput> GetBetAsync(Guid betId)
        {
            return await _httpService.GetAsync<BetOutput>($"{_host}{string.Format(RETRIEVE_BET_URL, betId)}", 
                                                        _authenticationService.Token)
                                     .ConfigureAwait(false);
        }

        public async Task<IReadOnlyCollection<BetOutput>> GetBetsAsync()
        {
            return await _httpService.GetAsync<IReadOnlyCollection<BetOutput>>($"{_host}{RETRIEVE_BETS_URL}", 
                                                                                _authenticationService.Token)
                                     .ConfigureAwait(false);
        }

        public async Task SaveAsync(Bet bet)
        {
            var jsonUser = JsonConvert.SerializeObject(bet, Formatting.Indented, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            await _httpService.PostAsync<object>($"{_host}{CREATE_BET_URL}", jsonUser)
                              .ConfigureAwait(false);
        }

        public Task AnswerBetAsync(Bet bet)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Bet bet)
        {
            throw new NotImplementedException();
        }
    }
}
