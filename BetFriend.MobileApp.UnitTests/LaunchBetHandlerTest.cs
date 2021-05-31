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
            Assert.False(result.HasError);
        }

        [Fact]
        public async Task ShouldReturnResultWithErrorIfEndDateNotValid()
        {
            //arrange
            var description = "new bet description";
            var endDate = DateTime.UtcNow.AddDays(-7);
            var participants = new[] { Guid.NewGuid() };
            var command = new LaunchBetCommand(description, endDate, participants);
            IBetRepository betRepository = new InMemoryBetRepository();
            var handler = new LaunchBetCommandHandler(betRepository);
            var expectedResult = new Result<object>(null, "End date is not valid");

            //act
            Result<object> result = await handler.Handle(command);

            //assert
            Assert.Equal(expectedResult, result);
            Assert.True(result.HasError);
        }

        [Fact]
        public async Task ShouldReturnResultWithErrorIfDescriptionIsEmpty()
        {
            //arrange
            var description = "";
            var endDate = DateTime.UtcNow.AddDays(7);
            var participants = new[] { Guid.NewGuid() };
            var command = new LaunchBetCommand(description, endDate, participants);
            IBetRepository betRepository = new InMemoryBetRepository();
            var handler = new LaunchBetCommandHandler(betRepository);
            var expectedResult = new Result<object>(null, "Description is not valid");

            //act
            Result<object> result = await handler.Handle(command);

            //assert
            Assert.Equal(expectedResult, result);
            Assert.True(result.HasError);
        }

        [Fact]
        public async Task ShouldReturnResultWithErrorIfHasNoParticipant()
        {
            //arrange
            var description = "description";
            var endDate = DateTime.UtcNow.AddDays(7);
            var participants = new Guid[] {  };
            var command = new LaunchBetCommand(description, endDate, participants);
            IBetRepository betRepository = new InMemoryBetRepository();
            var handler = new LaunchBetCommandHandler(betRepository);
            var expectedResult = new Result<object>(null, "There is no participant");

            //act
            Result<object> result = await handler.Handle(command);

            //assert
            Assert.Equal(expectedResult, result);
            Assert.True(result.HasError);
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

        public bool HasError { get => !string.IsNullOrEmpty(errorMsg); }
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
            try
            {
                ValidateCommand(command);

                await betRepository.LaunchBetAsync(command);
                return new Result<object>(null, null);
            }
            catch (Exception ex)
            {
                return new Result<object>(null, ex.Message);
            }
        }

        private void ValidateCommand(LaunchBetCommand command)
        {
            if (command.EndDate <= DateTime.UtcNow)
                throw new ArgumentException("End date is not valid");

            if (string.IsNullOrEmpty(command.Description))
                throw new ArgumentException("Description is not valid");

            if (command.Participants.Length == 0)
                throw new ArgumentException("There is no participant");
        }
    }

    internal class LaunchBetCommand
    {
        public DateTime EndDate { get; }
        public string Description { get; }
        public Guid[] Participants { get; }

        public LaunchBetCommand(string description, DateTime endDate, Guid[] participants)
        {
            Description = description;
            EndDate = endDate;
            Participants = participants;
        }
    }
}
