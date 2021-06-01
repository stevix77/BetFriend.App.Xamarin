using BetFriend.Domain.Bets;
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
        private const int _tokens = 20;

        public LaunchBetHandlerTest()
        {

        }

        [Fact]
        public async Task ShouldCreateBet()
        {
            //arrange
            var endDate = DateTime.UtcNow.AddDays(7);
            var command = new LaunchBetCommand(_description, endDate, _tokens);
            InMemoryBetRepository betRepository = new();
            var handler = new LaunchBetCommandHandler(betRepository);

            //act
            await handler.Handle(command);

            //assert
            IEnumerable<Bet> bets = betRepository.GetBets();
            Assert.NotEmpty(bets);
        }

        [Fact]
        public async Task ShouldThrowArgumentExceptionIfEndDateNotValid()
        {
            //arrange
            var endDate = DateTime.UtcNow.AddDays(-7);
            var command = new LaunchBetCommand(_description, endDate, 20);
            IBetRepository betRepository = new InMemoryBetRepository();
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
            var command = new LaunchBetCommand(null, endDate, 20);
            IBetRepository betRepository = new InMemoryBetRepository();
            var handler = new LaunchBetCommandHandler(betRepository);

            //act
            var record = await Record.ExceptionAsync(() => handler.Handle(command)); 

            //assert
            Assert.IsType<ArgumentException>(record);
            Assert.Equal("Description is not valid", record.Message);
        }
    }
}
