using BetFriend.Domain.Bets.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BetFriend.Domain.Bets.Usecases.SearchMembers
{
    public interface ISearchMembersQueryHandler
    {
        Task<IEnumerable<MemberOutput>> Handle(SearchMembersQuery query);
    }
}
