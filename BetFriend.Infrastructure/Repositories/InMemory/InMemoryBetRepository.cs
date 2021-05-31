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

        public InMemoryBetRepository()
        {
            _bets = new List<Bet>();
        }

        public Task LaunchBetAsync(LaunchBetCommand command)
        {
            _bets.Add(new Bet(Guid.NewGuid(), command.Description, command.EndDate, command.Participants));
            return Task.CompletedTask;
        }

        public IReadOnlyCollection<Bet> GetBets()
        {
            return _bets.AsReadOnly();
        }
    }
}
