namespace BetFriend.Domain.Bets.GetBetsInProgress
{
    using BetFriend.Domain.Bets.Dto;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class GetBetsInProgressQueryHandler
    {
        private readonly IQueryBetRepository _betRepository;

        public GetBetsInProgressQueryHandler(IQueryBetRepository betRepository)
        {
            _betRepository = betRepository ?? throw new ArgumentNullException(nameof(betRepository));
        }

        public async Task<IReadOnlyCollection<BetOutput>> Handle(GetBetsInProgressQuery query)
        {
            ValidateQuery(query);

            return await _betRepository.GetBetsForMember(query.MemberId);
        }

        private static void ValidateQuery(GetBetsInProgressQuery query)
        {
            if (query is null)
                throw new ArgumentNullException(nameof(query));
        }
    }
}
