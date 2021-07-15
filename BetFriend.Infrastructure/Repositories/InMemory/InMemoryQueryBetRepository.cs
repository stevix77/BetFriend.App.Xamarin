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

        public async Task<IReadOnlyCollection<BetOutput>> GetBetsForMember(Guid memberId)
        {
            var bets = _betOutputs.Where(x => x.Creator.Id == memberId 
                                        || x.Participants.Any(y => y.Id == memberId))
                                  .ToList();
            return await Task.FromResult(bets);
        }
    }
}
