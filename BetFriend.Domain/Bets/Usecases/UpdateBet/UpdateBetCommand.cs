using BetFriend.Domain.Bets.Dto;
using System;

namespace BetFriend.Domain.Bets.Usecases.UpdateBet
{
    public class UpdateBetCommand
    {
        public UpdateBetCommand(BetOutput bet, string description, DateTime? endDate, int? coins)
        {
            Bet = bet;
            Description = description;
            EndDate = endDate;
            Coins = coins;
        }

        public BetOutput Bet { get; }
        public string Description { get; }
        public DateTime? EndDate { get; }
        public int? Coins { get; }
    }
        
}
