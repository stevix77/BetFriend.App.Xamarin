namespace BetFriend.Domain.Bets.GetBetsInProgressOrOver
{
    using BetFriend.Domain.Bets.Dto;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetBetsInProgressOrOverQueryHandler
    {
        private readonly IQueryBetRepository _betRepository;

        public GetBetsInProgressOrOverQueryHandler(IQueryBetRepository betRepository)
        {
            _betRepository = betRepository ?? throw new ArgumentNullException(nameof(betRepository));
        }

        public async Task<IReadOnlyCollection<BetOutput>> Handle(GetBetsInProgressOrOverQuery query, CancellationToken cancellationToken)
        {
            ValidateQuery(query);

            var bets = await _betRepository.GetBetsForMember(query.MemberId);
            return bets;
        }

        private static void ValidateQuery(GetBetsInProgressOrOverQuery query)
        {
            if (query is null)
                throw new System.ArgumentNullException(nameof(query));
        }
    }
}
