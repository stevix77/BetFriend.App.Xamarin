namespace BetFriend.Domain.Bets.LaunchBet
{
    using System;

    public class LaunchBetCommand
    {
        public DateTime EndDate { get; }
        public string Description { get; }
        public int Coins { get; }
        public Guid BetId { get; set; }

        public LaunchBetCommand(Guid betId, string description, DateTime endDate, int coins)
        {
            BetId = betId;
            Description = description;
            EndDate = endDate;
            Coins = coins;
        }
    }
}
