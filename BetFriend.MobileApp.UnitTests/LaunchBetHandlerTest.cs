using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BetFriend.MobileApp.UnitTests
{
    public class LaunchBetHandlerTest
    {
        public LaunchBetHandlerTest()
        {

        }

        [Fact]
        public async Task ShouldReturnResultNullIfBetCreated()
        {
            //arrange
            var description = "new bet description";
            var endDate = DateTime.UtcNow.AddDays(7);
            var participants = new[] { Guid.NewGuid() };
            var command = new LaunchBetCommand(description, endDate, participants);
            IBetRepository betRepository = new InMemoryBetRepository();
            var handler = new LaunchBetCommandHandler(betRepository);
            var expectedResult = new Result<object>(null, null);

            //act
            Result<object> result = await handler.Handle(command);

            //assert
            Assert.Equal(expectedResult, result);
        }
    }

    internal class InMemoryBetRepository : IBetRepository
    {
        private readonly Dictionary<Guid, LaunchBetCommand> bets;

        public InMemoryBetRepository()
        {
            bets = new Dictionary<Guid, LaunchBetCommand>();
        }

        public Task LaunchBetAsync(LaunchBetCommand command)
        {
            bets.Add(Guid.NewGuid(), command);
            return Task.CompletedTask;
        }
    }

    internal struct Result<T>
    {
        private T data;
        private string errorMsg;

        public Result(T data, string errorMsg)
        {
            this.data = data;
            this.errorMsg = errorMsg;
        }
    }

    internal interface IBetRepository
    {
        Task LaunchBetAsync(LaunchBetCommand command);
    }

    internal class LaunchBetCommandHandler
    {
        private IBetRepository betRepository;

        public LaunchBetCommandHandler()
        {
        }

        public LaunchBetCommandHandler(IBetRepository betRepository)
        {
            this.betRepository = betRepository;
        }

        internal async Task<Result<object>> Handle(LaunchBetCommand command)
        {
            await betRepository.LaunchBetAsync(command);
            return new Result<object>(null, null);
        }
    }

    internal class LaunchBetCommand
    {
        private string description;
        private DateTime endDate;
        private Guid[] participants;

        public LaunchBetCommand(string description, DateTime endDate, Guid[] participants)
        {
            this.description = description;
            this.endDate = endDate;
            this.participants = participants;
        }
    }
}
