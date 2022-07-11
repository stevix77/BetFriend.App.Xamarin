using BetFriend.Domain.Bets.Dto;
using BetFriend.Domain.Bets.Usecases.SearchMembers;
using BetFriend.Domain.Users.Dto;
using BetFriend.Infrastructure.Repositories.InMemory;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BetFriend.MobileApp.UnitTests
{
    public class SearchMembersHandlerTest
    {
        [Fact]
        public async Task ShouldFindUsers()
        {
            var memberOutput = new MemberOutput();
            var handler = new SearchMembersQueryHandler(new InMemoryMemberRepository(new List<MemberOutput>() { memberOutput }));
            var query = new SearchMembersQuery("query");
            var searchResults = await handler.Handle(query);
            Assert.NotEmpty(searchResults);
        }


    }
}
