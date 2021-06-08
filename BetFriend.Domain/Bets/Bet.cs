using System;
using System.Collections.Generic;

namespace BetFriend.Domain.Bets
{
    public class Bet
    {
        public Bet(Guid betId, string description, DateTime endDate, int tokens, Guid creatorId)
        {
            BetId = betId;
            Description = description;
            EndDate = endDate;
            Tokens = tokens;
            CreatorId = creatorId;
        }

        public Guid BetId { get; }
        public string Description { get; }
        public DateTime EndDate { get; }
        public int Tokens { get; }
        public Guid CreatorId { get; }
    }
}

