namespace BetFriend.MobileApp.UnitTests
{
    using BetFriend.Domain.Bets;
    using BetFriend.Domain.Bets.Dto;
    using BetFriend.Domain.Bets.Usecases.AnswerBet;
    using BetFriend.Infrastructure.DateTime;
    using BetFriend.Infrastructure.Repositories.InMemory;
    using BetFriend.MobileApp.UnitTests.Implems;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;


    public class AnswerBetHandlerTest
    {
        private readonly Guid _betId = Guid.Parse("7a1e9875-cc2a-44c9-bf51-4fe7d375f34a");
        private readonly Guid _memberId = Guid.Parse("7a1e9875-cc2a-44c9-bf51-4fe7d375f34b");

        [Fact]
        public async Task ShouldAcceptMemberToParticipate()
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
            var repository = new InMemoryBetRepository(new() { bet }, authenticationService: authenticationService);
            var command = new AnswerBetCommand(bet, true);
            var handler = new AnswerBetCommandHandler(repository, authenticationService);
            await handler.Handle(command);
            Assert.Contains(bet.Members, x => x.Id == _memberId);
        }

        [Fact]
        public async Task ShouldRejectMemberToParticipateMoreThatOneTime()
        {
            var authenticationService = new InMemoryAuthenticationService(_memberId.ToString(), "username");
            var bet = new BetOutput
            {
                Id = _betId,
                Description = "description",
                EndDate = new DateTime(2022, 12, 30),
                Coins = 10,
                Members = new List<MemberOutput> { new MemberOutput { Id = Guid.Parse(authenticationService.UserId) } }
            };
            var command = new AnswerBetCommand(bet, true);
            var repository = new InMemoryBetRepository(new List<BetOutput>() { bet }, authenticationService);
            var handler = new AnswerBetCommandHandler(repository, authenticationService);
            await Record.ExceptionAsync(() => handler.Handle(command));
            Assert.Single(bet.Members);
        }

        [Fact]
        public async Task ShouldUpdateAnswerWhenChangeResponse()
        {
            var authenticationService = new InMemoryAuthenticationService(_memberId.ToString(), "username");
            var bet = new BetOutput { Id = _betId,
                Description = "description",
                Members = new List<MemberOutput> { new MemberOutput { Id = _memberId } } };
            var repository = new InMemoryBetRepository(new List<BetOutput>() { bet }, authenticationService);
            var command = new AnswerBetCommand(bet, false);
            var handler = new AnswerBetCommandHandler(repository, authenticationService);
            await handler.Handle(command);
            Assert.Empty(bet.Members);
        }
    }
}
