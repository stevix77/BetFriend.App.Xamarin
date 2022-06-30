namespace BetFriend.Infrastructure.Repositories.InMemory
{
    using BetFriend.Domain.Bets;
    using BetFriend.Domain.Bets.Dto;
    using BetFriend.Domain.Users;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;


    public class InMemoryBetRepository : IBetRepository
    {
        private readonly Dictionary<Guid, BetOutput> _betOutputs = new Dictionary<Guid, BetOutput>();
        private readonly IAuthenticationService _authenticationService;

        public InMemoryBetRepository(List<BetOutput> betOutputs = null, IAuthenticationService authenticationService = null)
        {
            if(betOutputs != null)
                betOutputs.ForEach(x => _betOutputs.Add(x.Id, x));

            _authenticationService = authenticationService;
        }

        public Task SaveAsync(Bet bet)
        {
            _betOutputs.Add(bet.BetId, new BetOutput
            {
                Coins = bet.Coins,
                Description = bet.Description,
                EndDate = bet.EndDate,
                Id = bet.BetId,
                Members = new List<MemberOutput>(),
                Creator = new MemberOutput { Id = Guid.Parse(_authenticationService.UserId), Username = _authenticationService.Username }
            });
            return Task.CompletedTask;
        }

        public Task<BetOutput> GetBetAsync(Guid betId)
        {
            if(_betOutputs.ContainsKey(betId))
                return Task.FromResult(_betOutputs[betId]);

            return Task.FromResult<BetOutput>(default);
        }

        public async Task<IReadOnlyCollection<BetOutput>> GetBetsAsync()
        {
            return await Task.FromResult(_betOutputs.Values);
        }

        public IReadOnlyCollection<BetOutput> GetBets()
        {
            return _betOutputs.Values;
        }

        public Task AnswerBetAsync(Bet bet)
        {
            var betOutput = _betOutputs[bet.BetId];
            betOutput.Members = new List<MemberOutput>(betOutput.Members) 
            { 
                new MemberOutput
                {
                    Id = Guid.Parse(_authenticationService.UserId),
                    Username = _authenticationService.Username
                } 
            };

            return Task.CompletedTask;
        }
    }
}
