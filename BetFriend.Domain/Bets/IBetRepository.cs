namespace BetFriend.Domain.Bets
{
    using System.Threading.Tasks;


    public interface IBetRepository
    {
        Task SaveAsync(Bet bet);
    }
}
