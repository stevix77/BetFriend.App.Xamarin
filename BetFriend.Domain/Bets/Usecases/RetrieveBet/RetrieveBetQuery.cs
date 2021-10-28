namespace BetFriend.Domain.Bets.RetrieveBet
{
    using System;


    public class RetrieveBetQuery
    {
        public RetrieveBetQuery(Guid betId)
        {
            BetId = betId;
        }

        public Guid BetId { get; }
    }
}
