namespace BetFriend.Infrastructure.Repositories.InMemory
{
    using BetFriend.Domain.Bets;
    using BetFriend.Domain.Bets.LaunchBet;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;


    public class InMemoryBetRepository : IBetRepository
    {
        private readonly List<Bet> _bets;
        private readonly Guid _creator;
        private readonly IQueryBetRepository _queryBetRepository;

        public InMemoryBetRepository(Guid creator, IQueryBetRepository queryBetRepository)
        {
            _bets = new List<Bet>();
            _creator = creator;
            _queryBetRepository = queryBetRepository;
        }

        public Task SaveAsync(LaunchBetCommand command)
        {
            var bet = new Bet(command.BetId, command.Description, command.EndDate, command.Coins, _creator);
            _bets.Add(bet);
            (_queryBetRepository as InMemoryQueryBetRepository).AddBet(bet);
            return Task.CompletedTask;
        }

        public IReadOnlyCollection<Bet> GetBets()
        {
            return _bets.AsReadOnly();
        }
    }
}
