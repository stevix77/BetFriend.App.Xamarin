namespace BetFriend.Domain.Bets.RetrieveBet
{
    using BetFriend.Domain.Bets.Dto;
    using System.Threading.Tasks;


    public interface IRetrieveBetQueryHandler
    {
        Task<BetOutput> Handle(RetrieveBetQuery betQuery);
    }
}