using BetFriend.Domain.Bets.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BetFriend.Domain.Bets
{
    public interface IQueryBetRepository
    {
        Task<IReadOnlyCollection<BetOutput>> GetBetsForMember(Guid memberId);
    }
}
