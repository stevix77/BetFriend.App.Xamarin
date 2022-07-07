using BetFriend.Domain.Users;
using BetFriend.Domain.Users.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BetFriend.Domain.Bets.Usecases.SearchUsers
{
    public class SearchUsersQueryHandler : ISearchUsersQueryHandler
    {
        private readonly IUserRepository _userRepository;

        public SearchUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<IEnumerable<UserOutput>> Handle(SearchUsersQuery query)
        {
            return _userRepository.SearchAsync(query.Query);
        }
    }
}
