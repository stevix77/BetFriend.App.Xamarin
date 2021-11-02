using BetFriend.Domain.Bets;
using BetFriend.Domain.Bets.Dto;
using BetFriend.Domain.Bets.LaunchBet;
using BetFriend.Infrastructure.Repositories.InMemory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BetFriend.MobileApp.UnitTests
{
    public class LaunchBetHandlerTest
    {
        private const string _description = "new bet description";
        private const int _coins = 20;
        private readonly Guid _betId = Guid.NewGuid();
        private readonly MemberOutput _member = new() { Id = Guid.NewGuid(), Username = "currentUser" };

        [Fact]
        public async Task ShouldCreateBet()
        {
            //arrange
            var endDate = DateTime.UtcNow.AddDays(7);
            var command = new LaunchBetCommand(_betId, _description, endDate, _coins);
            InMemoryBetRepository betRepository = new(new InMemoryQueryBetRepository(_member));
            var handler = new LaunchBetCommandHandler(betRepository);

            //act
            await handler.Handle(command);

            //assert
            IEnumerable<Bet> bets = betRepository.GetBets();
            Assert.Collection(bets, (bet) =>
            {
                Assert.Equal(command.Description, bet.Description);
                Assert.Equal(command.EndDate, bet.EndDate);
                Assert.Equal(command.Coins, bet.Coins);
                Assert.Equal(command.BetId, bet.BetId);
            });
        }

        [Fact]
        public async Task ShouldThrowArgumentExceptionIfEndDateNotValid()
        {
            //arrange
            var endDate = DateTime.UtcNow.AddDays(-7);
            var command = new LaunchBetCommand(_betId, _description, endDate, 20);
            IBetRepository betRepository = new InMemoryBetRepository(new InMemoryQueryBetRepository(_member));
            var handler = new LaunchBetCommandHandler(betRepository);

            //act
            var record = await Record.ExceptionAsync(() => handler.Handle(command));

            //assert
            Assert.IsType<ArgumentException>(record);
            Assert.Equal("End date is not valid", record.Message);
        }

        [Fact]
        public async Task ShouldThrowArgumentExceptionIfDescriptionIsEmpty()
        {
            //arrange
            var endDate = DateTime.UtcNow.AddDays(7);
            var command = new LaunchBetCommand(_betId, null, endDate, 20);
            IBetRepository betRepository = new InMemoryBetRepository(new InMemoryQueryBetRepository(_member));
            var handler = new LaunchBetCommandHandler(betRepository);

            //act
            var record = await Record.ExceptionAsync(() => handler.Handle(command));

            //assert
            Assert.IsType<ArgumentException>(record);
            Assert.Equal("Description is not valid", record.Message);
        }
    }
}
