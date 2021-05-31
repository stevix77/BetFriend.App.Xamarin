namespace BetFriend.Domain.Bets.LaunchBet
{
    using System;

    public class LaunchBetCommand
    {
        public DateTime EndDate { get; }
        public string Description { get; }
        public Guid[] Participants { get; }

        public LaunchBetCommand(string description, DateTime endDate, Guid[] participants)
        {
            Description = description;
            EndDate = endDate;
            Participants = participants;
        }
    }
}
