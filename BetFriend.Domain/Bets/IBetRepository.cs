namespace BetFriend.Domain.Bets
{
    using BetFriend.Domain.Bets.LaunchBet;
    using System.Threading.Tasks;


    public interface IBetRepository
    {
        Task LaunchBetAsync(LaunchBetCommand command);
    }
}
