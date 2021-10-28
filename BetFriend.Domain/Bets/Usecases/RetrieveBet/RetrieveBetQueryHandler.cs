namespace BetFriend.Domain.Bets.RetrieveBet
{
    using BetFriend.Domain.Bets.Dto;
    using BetFriend.Domain.Bets.Exceptions;
    using System.Threading.Tasks;


    public class RetrieveBetQueryHandler
    {
        private readonly IQueryBetRepository _queryBetRepository;

        public RetrieveBetQueryHandler(IQueryBetRepository queryBetRepository)
        {
            _queryBetRepository = queryBetRepository;
        }

        public async Task<BetOutput> Handle(RetrieveBetQuery betQuery)
        {
            var betOutput = await _queryBetRepository.GetBetAsync(betQuery.BetId)
                  ?? throw new BetNotFoundException(betQuery.BetId);
            return betOutput;
        }
    }
}
