namespace BetFriend.Infrastructure.Repositories.InMemory
{
    using BetFriend.Domain.Bets;
    using BetFriend.Domain.Bets.Dto;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System;
    using System.Linq;

    public class InMemoryQueryBetRepository : IQueryBetRepository
    {
        private readonly List<BetOutput> _betOutputs;

        public InMemoryQueryBetRepository(List<BetOutput> betOutputs = null)
        {
            _betOutputs = betOutputs ?? new List<BetOutput>();
        }

        public Task<BetOutput> GetBetAsync(Guid betId)
        {
            var betOutput = _betOutputs.FirstOrDefault(x => x.Id == betId);
            return Task.FromResult(betOutput);
        }

        public async Task<IReadOnlyCollection<BetOutput>> GetBetsForMember(Guid memberId)
        {
            return await Task.FromResult(_betOutputs);
        }

        internal void AddBet(Bet bet)
        {
            _betOutputs.Add(new BetOutput
            {
                Creator = new MemberOutput { Id = bet.CreatorId },
                Id = bet.BetId,
                Description = bet.Description,
                EndDate = bet.EndDate,
                Coins = bet.Tokens,
                Participants = new List<MemberOutput>()
            });
        }
    }
}
