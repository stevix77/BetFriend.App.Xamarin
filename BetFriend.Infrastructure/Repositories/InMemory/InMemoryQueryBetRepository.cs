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
        private readonly MemberOutput _currentUser;

        public InMemoryQueryBetRepository(MemberOutput currentUser, List<BetOutput> betOutputs = null)
        {
            _currentUser = currentUser;
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
                Creator = _currentUser,
                Id = bet.BetId,
                Description = bet.Description,
                EndDate = bet.EndDate,
                Coins = bet.Coins,
                Participants = new List<MemberOutput>()
            });
        }
    }
}
