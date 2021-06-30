using BetFriend.Domain.Bets;
using BetFriend.Domain.Bets.Dto;
using BetFriend.Domain.Bets.GetBetsInProgressOrOver;
using BetFriend.Infrastructure.Repositories.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BetFriend.MobileApp.UnitTests
{
    public class GetBetsInProgressOrOverHandlerTest
    {
        public GetBetsInProgressOrOverHandlerTest()
        {

        }

        [Fact]
        public async Task HandleShouldReturnEmptyListBetIfMemberHasNeverBet()
        {
            //arrange
            var memberId = Guid.NewGuid();
            var handler = new GetBetsInProgressOrOverQueryHandler(new InMemoryQueryBetRepository(null));
            var query = new GetBetsInProgressOrOverQuery(memberId);

            //act
            IReadOnlyCollection<BetOutput> bets = await handler.Handle(query, default);

            //assert
            Assert.Empty(bets);
        }

        [Fact]
        public async Task HandleShouldReturnOneBetIfMemberHasCreatedOneBetInProgress()
        {
            //arrange
            var memberId = Guid.NewGuid();
            IQueryBetRepository betRepository = new InMemoryQueryBetRepository(new List<BetOutput>()
            {
                new BetOutput
                {
                    CreatorId = memberId,
                    Description = "desc1",
                    Tokens = 30,
                    EndDate = new DateTime(2022, 2, 2),
                    Id = Guid.NewGuid()
                }
            });
            var handler = new GetBetsInProgressOrOverQueryHandler(betRepository);
            var query = new GetBetsInProgressOrOverQuery(memberId);

            //act
            IReadOnlyCollection<BetOutput> bets = await handler.Handle(query, default);

            //assert
            Assert.Single(bets);
            var bet = bets.First();
            Assert.Equal(memberId, bet.CreatorId);
        }

        [Fact]
        public async Task HandleShouldReturnOneBetIfMemberParticipateToOneBetInProgress()
        {
            //arrange
            var memberId = Guid.NewGuid();
            IQueryBetRepository betRepository = new InMemoryQueryBetRepository(new List<BetOutput>()
            {
                new BetOutput
                {
                    CreatorId = Guid.NewGuid(),
                    Description = "desc1",
                    Tokens = 30,
                    EndDate = new DateTime(2022, 2, 2),
                    Id = Guid.NewGuid(),
                    Participants = new List<MemberOutput>
                    {
                        new MemberOutput
                        {
                            Id = memberId,
                            Username = "username1"
                        }
                    }
                }
            });
            var handler = new GetBetsInProgressOrOverQueryHandler(betRepository);
            var query = new GetBetsInProgressOrOverQuery(memberId);

            //act
            IReadOnlyCollection<BetOutput> bets = await handler.Handle(query, default);

            //assert
            Assert.Single(bets);
            var bet = bets.First();
            Assert.NotEmpty(bet.Participants);
            Assert.Collection(bet.Participants, x =>
            {
                Assert.Equal(memberId, x.Id);
                Assert.Equal("username1", x.Username);
            });
        }

        [Fact]
        public async Task HandleShouldThrowArgumentNullExceptionIfQueryIsNull()
        {
            //arrange
            var handler = new GetBetsInProgressOrOverQueryHandler(new InMemoryQueryBetRepository());

            //act
            var record = await Record.ExceptionAsync(() => handler.Handle(default, default));

            //assert
            Assert.IsType<ArgumentNullException>(record);
        }

        [Fact]
        public void CtorShouldThrowArgumentNullExceptionIfParamsNull()
        {
            //arrange
            IQueryBetRepository queryBetRepository = null;

            //act
            var record = Record.Exception(() => new GetBetsInProgressOrOverQueryHandler(queryBetRepository));

            //assert
            Assert.IsType<ArgumentNullException>(record);

        }

    }
}
