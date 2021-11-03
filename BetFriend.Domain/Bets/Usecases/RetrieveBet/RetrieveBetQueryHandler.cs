namespace BetFriend.Domain.Bets.RetrieveBet
{
    using BetFriend.Domain.Bets.Dto;
    using BetFriend.Domain.Bets.Exceptions;
    using System.Threading.Tasks;


    public class RetrieveBetQueryHandler : IRetrieveBetQueryHandler
    {
        private readonly IBetRepository _betRepository;

        public RetrieveBetQueryHandler(IBetRepository betRepository)
        {
            _betRepository = betRepository;
        }

        public async Task<BetOutput> Handle(RetrieveBetQuery betQuery)
        {
            var betOutput = await _betRepository.GetBetAsync(betQuery.BetId)
                  ?? throw new BetNotFoundException(betQuery.BetId);
            return betOutput;
        }
    }
}
