using BetFriend.Domain.Bets.Dto;
using System.Threading.Tasks;

namespace BetFriend.Domain.Bets.RetrieveBet
{
    public class RetrieveBetQueryHandler
    {
        private IQueryBetRepository _queryBetRepository;

        public RetrieveBetQueryHandler(IQueryBetRepository queryBetRepository)
        {
            _queryBetRepository = queryBetRepository;
        }

        public Task<BetOutput> Handle(RetrieveBetQuery betQuery)
        {
            return Task.FromResult(new BetOutput { Id = betQuery.BetId });
        }
    }
}
