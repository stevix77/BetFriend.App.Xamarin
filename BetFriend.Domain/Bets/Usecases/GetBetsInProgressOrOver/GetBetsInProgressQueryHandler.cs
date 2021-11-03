namespace BetFriend.Domain.Bets.GetBetsInProgress
{
    using BetFriend.Domain.Bets.Dto;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class GetBetsInProgressQueryHandler : IGetBetsInProgressQueryHandler
    {
        private readonly IBetRepository _betRepository;

        public GetBetsInProgressQueryHandler(IBetRepository betRepository)
        {
            _betRepository = betRepository ?? throw new ArgumentNullException(nameof(betRepository));
        }

        public async Task<IReadOnlyCollection<BetOutput>> Handle()
        {
            return await _betRepository.GetBetsAsync();
        }
    }
}
