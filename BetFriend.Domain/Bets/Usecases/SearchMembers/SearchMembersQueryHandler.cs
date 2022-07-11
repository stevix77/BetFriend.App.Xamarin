using BetFriend.Domain.Bets.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BetFriend.Domain.Bets.Usecases.SearchMembers
{
    public class SearchMembersQueryHandler : ISearchMembersQueryHandler
    {
        private readonly IMemberRepository _memberRepository;

        public SearchMembersQueryHandler(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public Task<IEnumerable<MemberOutput>> Handle(SearchMembersQuery query)
        {
            return _memberRepository.SearchAsync(query.Keyword);
        }
    }
}
