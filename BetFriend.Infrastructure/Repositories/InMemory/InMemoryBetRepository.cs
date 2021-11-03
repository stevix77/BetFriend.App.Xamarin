namespace BetFriend.Infrastructure.Repositories.InMemory
{
    using BetFriend.Domain.Bets;
    using BetFriend.Domain.Bets.Dto;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;


    public class InMemoryBetRepository : IBetRepository
    {
        private readonly List<Bet> _bets;
        private readonly List<BetOutput> _betOutputs;

        public InMemoryBetRepository(List<BetOutput> betOutputs = null)
        {
            _bets = new List<Bet>();
            _betOutputs = betOutputs ?? new List<BetOutput>();
        }

        public Task SaveAsync(Bet bet)
        {
            _bets.Add(bet);
            _betOutputs.Add(new BetOutput
            {
                Coins = bet.Coins,
                Description = bet.Description,
                EndDate = bet.EndDate,
                Id = bet.BetId,
                Participants = new List<MemberOutput>()
            });
            return Task.CompletedTask;
        }

        public Task<BetOutput> GetBetAsync(Guid betId)
        {
            var betOutput = _betOutputs.FirstOrDefault(x => x.Id == betId);
            return Task.FromResult(betOutput);
        }

        public async Task<IReadOnlyCollection<BetOutput>> GetBetsAsync()
        {
            return await Task.FromResult(_betOutputs);
        }

        public IReadOnlyCollection<Bet> GetBets()
        {
            return _bets;
        }
    }
}
