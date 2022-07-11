using BetFriend.Domain.Bets.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BetFriend.Domain.Bets
{
    public interface IMemberRepository
    {
        Task<IEnumerable<MemberOutput>> SearchAsync(string keyword);
    }
}
