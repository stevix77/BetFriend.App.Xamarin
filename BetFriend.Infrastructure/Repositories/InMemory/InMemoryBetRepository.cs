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

        public InMemoryBetRepository(Guid creator)
        {
            _bets = new List<Bet>();
            _creator = creator;
        }

        public Task LaunchBetAsync(LaunchBetCommand command)
        {
            _bets.Add(new Bet(command.BetId, command.Description, command.EndDate, command.Coins, _creator));
            return Task.CompletedTask;
        }

        public IReadOnlyCollection<Bet> GetBets()
        {
            return _bets.AsReadOnly();
        }
    }
}
