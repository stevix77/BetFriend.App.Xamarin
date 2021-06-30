namespace BetFriend.Domain.Bets.GetBetsInProgressOrOver
{
    using System;

    public sealed class GetBetsInProgressOrOverQuery
    {
        public Guid MemberId { get; }

        public GetBetsInProgressOrOverQuery(Guid memberId)
        {
            MemberId = memberId;
        }
    }
}
