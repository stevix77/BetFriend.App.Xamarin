using System;
using System.Collections.Generic;

namespace BetFriend.Domain.Bets
{
    public class Bet
    {
        public Bet(Guid betId, string description, DateTime endDate, int coins)
        {
            BetId = betId;
            Description = description;
            EndDate = endDate;
            Coins = coins;
        }

        public Guid BetId { get; }
        public string Description { get; }
        public DateTime EndDate { get; }
        public int Coins { get; }
    }
}

