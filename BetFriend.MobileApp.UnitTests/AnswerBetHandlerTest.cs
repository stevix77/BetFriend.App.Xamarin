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
        private readonly string _betId = "7a1e9875-cc2a-44c9-bf51-4fe7d375f34a";
        private readonly string _memberId = "7a1e9875-cc2a-44c9-bf51-4fe7d375f34b";

        [Fact]
        public async Task ShouldAcceptMemberToParticipate()
        {
            var command = new AnswerBetCommand(_betId, true);
            var bet = new BetOutput { Id = Guid.Parse(_betId) };
            var repository = new InMemoryBetRepository(new List<BetOutput> { bet }, new InMemoryAuthenticationService(_memberId, "username"));
            var handler = new AnswerBetHandler(repository);
            await handler.Handle(command);
            Assert.Contains(bet.Participants, x => x.Id == Guid.Parse(_memberId));
        }
    }
}
