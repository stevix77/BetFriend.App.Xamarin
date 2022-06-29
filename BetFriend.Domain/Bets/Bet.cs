using BetFriend.Domain.Abstractions;
using BetFriend.Domain.Bets.LaunchBet;
using System;

namespace BetFriend.Domain.Bets
{
    public class Bet
    {
        private Bet(Guid betId,
                    string description,
                    DateTime endDate,
                    int coins)
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

        internal static Bet Create(LaunchBetCommand command, IDateTimeProvider dateTimeProvider)
        {
            if (command.EndDate <= dateTimeProvider.Now())
                throw new ArgumentException("End date is not valid");

            if (string.IsNullOrEmpty(command.Description))
                throw new ArgumentException("Description is not valid");

            return new Bet(command.BetId, command.Description, command.EndDate, command.Coins);
        }
    }
}

