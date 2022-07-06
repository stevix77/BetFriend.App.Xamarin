using System.Collections.Generic;
using BetFriend.Domain.Bets.Dto;

namespace BetFriend.Infrastructure.Repositories.InMemory
{
    public class InMemoryMemberRepository
    {
        private readonly List<MemberOutput> _members;
        public InMemoryMemberRepository(List<MemberOutput> members = null)
        {
            _members = members ?? new List<MemberOutput>();
        }
    }
}
