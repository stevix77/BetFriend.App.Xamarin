using System;

namespace BetFriend.Domain.Bets
{
    public class Bet
    {
        public Bet(Guid betId, string description, DateTime endDate, int tokens)
        {
            BetId = betId;
            Description = description;
            EndDate = endDate;
            Tokens = tokens;
        }

        public Guid BetId { get; }
        public string Description { get; }
        public DateTime EndDate { get; }
        public int Tokens { get; }
    }
}

