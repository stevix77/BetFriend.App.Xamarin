using System.Collections.Generic;
using System.Threading.Tasks;
using BetFriend.Domain.Bets;
using BetFriend.Domain.Bets.Dto;

namespace BetFriend.Infrastructure.Repositories.InMemory
{
    public class InMemoryMemberRepository : IMemberRepository
    {
        private readonly List<MemberOutput> _members;
        public InMemoryMemberRepository(List<MemberOutput> members = null)
        {
            _members = members ?? new List<MemberOutput>();
        }

        public Task<IEnumerable<MemberOutput>> SearchAsync(string keyword)
        {
            return Task.FromResult<IEnumerable<MemberOutput>>(_members);
        }
    }
}
