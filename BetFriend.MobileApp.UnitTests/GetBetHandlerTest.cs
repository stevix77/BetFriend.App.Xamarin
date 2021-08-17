using BetFriend.Domain.Bets;
using BetFriend.Domain.Bets.Dto;
using BetFriend.Domain.Bets.RetrieveBet;
using BetFriend.Infrastructure.Repositories.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BetFriend.MobileApp.UnitTests
{
    public class GetBetHandlerTest
    {
        public GetBetHandlerTest()
        {
        }

        [Fact]
        public async Task ShouldReturnBetIfIdknown()
        {
            var betId = Guid.NewGuid();
            var creator = new MemberOutput { Id = Guid.NewGuid(), Username = "toto" };
            var betOutput = new BetOutput() { Creator = creator, Id = betId, Description = "description bet",
                EndDate = DateTime.UtcNow.AddDays(10), Tokens = 100 };
            var betQuery = new RetrieveBetQuery(betId);
            IQueryBetRepository queryBetRepository = new InMemoryQueryBetRepository(new() { betOutput });
            var queryHandler = new RetrieveBetQueryHandler(queryBetRepository);

            //act
            BetOutput result = await queryHandler.Handle(betQuery);

            //assert
            Assert.NotNull(result);
            Assert.Equal(betId, result.Id);
        }

        [Fact]
        public async Task ShouldThrowNotFoundExceptionIfIdUnknown()
        {

        }
    }
}
