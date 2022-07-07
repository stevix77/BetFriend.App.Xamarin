using BetFriend.Domain.Users.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BetFriend.Domain.Bets.Usecases.SearchUsers
{
    public interface ISearchUsersQueryHandler
    {
        Task<IEnumerable<UserOutput>> Handle(SearchUsersQuery query);
    }
}
