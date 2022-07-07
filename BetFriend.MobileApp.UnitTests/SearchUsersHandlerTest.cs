using BetFriend.Domain.Bets.Usecases.SearchUsers;
using BetFriend.Domain.Users.Dto;
using BetFriend.Infrastructure.Repositories.InMemory;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BetFriend.MobileApp.UnitTests
{
    public class SearchUsersHandlerTest
    {
        [Fact]
        public async Task ShouldFindUsers()
        {
            var userOutput = new UserOutput();
            var handler = new SearchUsersQueryHandler(new InMemoryUserRepository(new List<UserOutput>() { userOutput }));
            var query = new SearchUsersQuery("query");
            var searchResults = await handler.Handle(query);
            Assert.NotEmpty(searchResults);
        }


    }
}
