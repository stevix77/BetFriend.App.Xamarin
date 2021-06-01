namespace BetFriend.Domain.Bets.LaunchBet
{
    using System;

    public class LaunchBetCommand
    {
        public DateTime EndDate { get; }
        public string Description { get; }
        public int Tokens { get; }

        public LaunchBetCommand(string description, DateTime endDate, int tokens)
        {
            Description = description;
            EndDate = endDate;
            Tokens = tokens;
        }
    }
}
