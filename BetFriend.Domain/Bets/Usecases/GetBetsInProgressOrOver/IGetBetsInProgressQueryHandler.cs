namespace BetFriend.Domain.Bets.GetBetsInProgress
{
    using BetFriend.Domain.Bets.Dto;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGetBetsInProgressQueryHandler
    {
        Task<IReadOnlyCollection<BetOutput>> Handle(GetBetsInProgressQuery query);
    }
}