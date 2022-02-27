using System.Threading.Tasks;

namespace BetFriend.Domain.Bets.Usecases.AnswerBet
{
    public interface IAnswerBetCommandHandler
    {
        Task Handle(AnswerBetCommand command);
    }
}