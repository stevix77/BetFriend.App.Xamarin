namespace BetFriend.MobileApp.UnitTests
{
    using BetFriend.Domain.Bets.Dto;
    using BetFriend.Domain.Bets.Usecases.AnswerBet;
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
            var command = new AnswerBetCommand(_betId.ToString(), true);
            var bet = new BetOutput { Id = _betId };
            var repository = new InMemoryBetRepository(new List<BetOutput> { bet }, new InMemoryAuthenticationService(_memberId.ToString(), "username"));
            var handler = new AnswerBetCommandHandler(repository);
            await handler.Handle(command);
            Assert.Contains(bet.Participants, x => x.Id == _memberId);
        }

        [Fact]
        public async Task ShouldRejectMemberToParticipateMoreThatOneTime()
        {
            var command = new AnswerBetCommand(_betId.ToString(), true);
            var bet = new BetOutput { Id = _betId, Participants = new List<MemberOutput> { new MemberOutput { Id = _memberId } } };
            var repository = new InMemoryBetRepository(new List<BetOutput>() { bet }, new InMemoryAuthenticationService(_memberId.ToString(), "username"));
            var handler = new AnswerBetCommandHandler(repository);
            await Record.ExceptionAsync(() => handler.Handle(command));
            Assert.Single(bet.Participants);
        }
    }
}
