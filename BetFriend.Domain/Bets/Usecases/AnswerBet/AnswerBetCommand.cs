namespace BetFriend.Domain.Bets.Usecases.AnswerBet
{
    public class AnswerBetCommand
    {
        public AnswerBetCommand(string betId, bool answer)
        {
            BetId = betId;
            Answer = answer;
        }

        public string BetId { get; }
        public bool Answer { get; }
    }
}
