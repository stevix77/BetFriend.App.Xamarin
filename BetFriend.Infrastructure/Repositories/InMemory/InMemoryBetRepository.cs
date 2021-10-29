namespace BetFriend.Infrastructure.Repositories.InMemory
{
    using BetFriend.Domain.Bets;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;


    public class InMemoryBetRepository : IBetRepository
    {
        private readonly List<Bet> _bets;
        private readonly IQueryBetRepository _queryBetRepository;

        public InMemoryBetRepository(IQueryBetRepository queryBetRepository)
        {
            _bets = new List<Bet>();
            _queryBetRepository = queryBetRepository;
        }

        public Task SaveAsync(Bet bet)
        {
            _bets.Add(bet);
            (_queryBetRepository as InMemoryQueryBetRepository).AddBet(bet);
            return Task.CompletedTask;
        }

        public IReadOnlyCollection<Bet> GetBets()
        {
            return _bets;
        }
    }
}
