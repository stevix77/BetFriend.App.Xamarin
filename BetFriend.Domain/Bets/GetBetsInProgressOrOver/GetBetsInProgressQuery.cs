namespace BetFriend.Domain.Bets.GetBetsInProgress
{
    using System;

    public sealed class GetBetsInProgressQuery
    {
        public Guid MemberId { get; }

        public GetBetsInProgressQuery(Guid memberId)
        {
            MemberId = memberId;
        }
    }
}
