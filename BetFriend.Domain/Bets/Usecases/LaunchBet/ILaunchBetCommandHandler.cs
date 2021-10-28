namespace BetFriend.Domain.Bets.LaunchBet
{
    using System.Threading.Tasks;

    public interface ILaunchBetCommandHandler
    {
        Task Handle(LaunchBetCommand command);
    }
}