namespace BetFriend.MobileApp.UnitTests
{
    using BetFriend.Domain.Bets.LaunchBet;
    using BetFriend.Infrastructure.DateTime;
    using BetFriend.Infrastructure.Repositories.InMemory;
    using BetFriend.MobileApp.UnitTests.Implems;
    using System;
    using System.Threading.Tasks;
    using Xunit;


    public class LaunchBetHandlerTest
    {
        private const string _description = "new bet description";
        private const int _coins = 20;
        private readonly Guid _betId = Guid.NewGuid();

        [Fact]
        public async Task ShouldCreateBet()
        {
            //arrange
            var endDate = new DateTime(2021, 12, 31);
            var command = new LaunchBetCommand(_betId, _description, endDate, _coins);
            var betRepository = new InMemoryBetRepository(authenticationService: new InMemoryAuthenticationService(Guid.NewGuid().ToString(), "username"));
            var handler = new LaunchBetCommandHandler(betRepository, new FakeDateTimeProvider(new DateTime(2021, 10, 10)));

            //act
            await handler.Handle(command);

            //assert
            var bets = betRepository.GetBets();
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
            var endDate = new DateTime(2020, 12, 12);
            var command = new LaunchBetCommand(_betId, _description, endDate, 20);
            var betRepository = new InMemoryBetRepository();
            var handler = new LaunchBetCommandHandler(betRepository, new FakeDateTimeProvider(new DateTime(2021, 3, 3)));

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
            var endDate = new DateTime(2021, 12, 31);
            var command = new LaunchBetCommand(_betId, null, endDate, 20);
            var betRepository = new InMemoryBetRepository();
            var handler = new LaunchBetCommandHandler(betRepository, new FakeDateTimeProvider(new DateTime(2021, 8, 3)));

            //act
            var record = await Record.ExceptionAsync(() => handler.Handle(command));

            //assert
            Assert.IsType<ArgumentException>(record);
            Assert.Equal("Description is not valid", record.Message);
        }
    }
}
