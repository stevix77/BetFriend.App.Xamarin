using BetFriend.Domain.Bets.Dto;

namespace BetFriend.Domain.Bets.Usecases.AnswerBet
{
    public class AnswerBetCommand
    {
        public AnswerBetCommand(BetOutput bet, bool answer)
        {
            Bet = bet;
            Answer = answer;
        }

        public BetOutput Bet { get; }
        public bool Answer { get; }
    }
}
