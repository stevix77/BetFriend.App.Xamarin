using BetFriend.Domain.Bets;
using BetFriend.Domain.Bets.Dto;
using BetFriend.Domain.Bets.Exceptions;
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
        public async Task ShouldReturnBetIfIdKnown()
        {
            var betId = Guid.NewGuid();
            var creator = new MemberOutput { Id = Guid.NewGuid(), Username = "toto" };
            var betOutput = new BetOutput() { Creator = creator, Id = betId, Description = "description bet",
                EndDate = DateTime.UtcNow.AddDays(10), Coins = 100 };
            var betQuery = new RetrieveBetQuery(betId);
            var queryBetRepository = new InMemoryQueryBetRepository(creator, new() { betOutput });
            var queryHandler = new RetrieveBetQueryHandler(queryBetRepository);

            var result = await queryHandler.Handle(betQuery);

            Assert.NotNull(result);
            Assert.Equal(betId, result.Id);
        }

        [Fact]
        public async Task ShouldThrowNotFoundExceptionIfIdUnknown()
        {
            var betId = Guid.NewGuid();
            var betQuery = new RetrieveBetQuery(betId);
            var queryBetRepository = new InMemoryQueryBetRepository(default);
            var queryHandler = new RetrieveBetQueryHandler(queryBetRepository);

            var record = await Record.ExceptionAsync(() => queryHandler.Handle(betQuery));

            Assert.IsType<BetNotFoundException>(record);
            Assert.Equal($"Bet with id '{betId}' is not found", record.Message);
        }
    }
}
