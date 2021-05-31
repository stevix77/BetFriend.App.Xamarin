using System;

namespace BetFriend.Domain.Bets
{
    public class Bet
    {
        public Bet(Guid betId, string description, DateTime endDate, Guid[] participants)
        {
            BetId = betId;
            Description = description;
            EndDate = endDate;
            Participants = participants;
        }

        public Guid BetId { get; }
        public string Description { get; }
        public DateTime EndDate { get; }
        public Guid[] Participants { get; }
    }
}

