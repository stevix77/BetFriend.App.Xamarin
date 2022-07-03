using System.Threading.Tasks;

namespace BetFriend.Domain.Bets.Usecases.UpdateBet
{
    public interface IUpdateBetCommandHandler
    {
        Task Handle(UpdateBetCommand command);
    }
}