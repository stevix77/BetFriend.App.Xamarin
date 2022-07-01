using BetFriend.Domain.Bets.Dto;
using BetFriend.Infrastructure.Repositories.InMemory;
using BetFriend.Domain.Bets.Usecases.UpdateBet;
using BetFriend.MobileApp.UnitTests.Implems;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BetFriend.MobileApp.UnitTests
{
    public class UpdateBetHandlerTest
    {
        private readonly Guid _betId = Guid.Parse("7a1e9875-cc2a-44c9-bf51-4fe7d375f34a");
        private readonly Guid _memberId = Guid.Parse("7a1e9875-cc2a-44c9-bf51-4fe7d375f34b");

        [Fact]
        public async Task CreatorShouldUpdateBetWhenHasNoMembers()
        {
            var authenticationService = new InMemoryAuthenticationService(_memberId.ToString(), "username");
            var bet = new BetOutput
            {
                Id = _betId,
                Description = "description",
                EndDate = new DateTime(2022, 12, 30),
                Coins = 10,
                Creator = new MemberOutput { Id = Guid.Parse(authenticationService.UserId), Username = authenticationService.Username },
                Members = new List<MemberOutput>()
            };
            var repository = new InMemoryBetRepository(new() { bet }, authenticationService);
            var command = new UpdateBetCommand(bet, "new description", new DateTime(2022, 11, 30), 50);
            var handler = new UpdateBetCommandHandler(repository);
            await handler.Handle(command);
            Assert.Equal(command.Description, bet.Description);
            Assert.Equal(command.Coins, bet.Coins);
            Assert.Equal(command.EndDate, bet.EndDate);
        }

        [Fact]
        public async Task ShouldNotUpdateBetWhenHasAnyMember()
        {
            var bet = new BetOutput
            {
                Id = _betId,
                Description = "description",
                EndDate = new DateTime(2022, 12, 30),
                Coins = 10,
                Creator = new MemberOutput { Id = _memberId },
                Members = new List<MemberOutput> { new MemberOutput { Id = Guid.NewGuid() } }
            };
            var repository = new InMemoryBetRepository(new() { bet });
            var command = new UpdateBetCommand(bet, "new description", new DateTime(2022, 11, 30), 50);
            var handler = new UpdateBetCommandHandler(repository);
            await handler.Handle(command);
            Assert.Equal("description", bet.Description);
            Assert.Equal(10, bet.Coins);
            Assert.Equal(new DateTime(2022, 12, 30), bet.EndDate);
        }
    }
}
