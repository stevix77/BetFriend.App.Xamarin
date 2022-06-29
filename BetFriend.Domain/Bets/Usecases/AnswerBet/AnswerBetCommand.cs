namespace BetFriend.Domain.Bets.Usecases.AnswerBet
{
    public class AnswerBetCommand
    {
        public AnswerBetCommand(Bet bet, bool answer)
        {
            Bet = bet;
            Answer = answer;
        }

        public Bet Bet { get; }
        public bool Answer { get; }
    }
}
