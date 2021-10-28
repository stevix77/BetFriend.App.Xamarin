using BetFriend.Domain.Bets;
using BetFriend.Domain.Bets.Dto;
using BetFriend.Domain.Bets.GetBetsInProgress;
using BetFriend.Infrastructure.Repositories.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BetFriend.MobileApp.UnitTests
{
    public class GetBetsInProgressHandlerTest
    {
        public GetBetsInProgressHandlerTest()
        {

        }

        [Fact]
        public async Task HandleShouldReturnEmptyListBetIfMemberHasNeverBet()
        {
            //arrange
            var memberId = Guid.NewGuid();
            var handler = new GetBetsInProgressQueryHandler(new InMemoryQueryBetRepository(null));
            var query = new GetBetsInProgressQuery(memberId);

            //act
            IReadOnlyCollection<BetOutput> bets = await handler.Handle(query);

            //assert
            Assert.Empty(bets);
        }

        [Fact]
        public async Task HandleShouldReturnOneBetIfMemberHasCreatedOneBetInProgress()
        {
            //arrange
            var memberId = Guid.NewGuid();
            var creator = new MemberOutput 
            { 
                Id = memberId, 
                Username = "creator1" 
            };
            IQueryBetRepository betRepository = new InMemoryQueryBetRepository(new List<BetOutput>()
            {
                new BetOutput
                {
                    Creator = creator,
                    Description = "desc1",
                    Coins = 30,
                    EndDate = new DateTime(2022, 2, 2),
                    Id = Guid.NewGuid()
                }
            });
            var handler = new GetBetsInProgressQueryHandler(betRepository);
            var query = new GetBetsInProgressQuery(memberId);

            //act
            IReadOnlyCollection<BetOutput> bets = await handler.Handle(query);

            //assert
            Assert.Single(bets);
            var bet = bets.First();
            Assert.Equal(memberId, bet.Creator.Id);
        }

        [Fact]
        public async Task HandleShouldReturnOneBetIfMemberParticipateToOneBetInProgress()
        {
            //arrange
            var memberId = Guid.NewGuid();
            var creator = new MemberOutput
            {
                Id = Guid.NewGuid(),
                Username = "creator1"
            };
            IQueryBetRepository betRepository = new InMemoryQueryBetRepository(new List<BetOutput>()
            {
                new BetOutput
                {
                    Creator = creator,
                    Description = "desc1",
                    Coins = 30,
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
            var handler = new GetBetsInProgressQueryHandler(betRepository);
            var query = new GetBetsInProgressQuery(memberId);

            //act
            IReadOnlyCollection<BetOutput> bets = await handler.Handle(query);

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
            var handler = new GetBetsInProgressQueryHandler(new InMemoryQueryBetRepository());

            //act
            var record = await Record.ExceptionAsync(() => handler.Handle(default));

            //assert
            Assert.IsType<ArgumentNullException>(record);
        }

        [Fact]
        public void CtorShouldThrowArgumentNullExceptionIfParamsNull()
        {
            //arrange
            IQueryBetRepository queryBetRepository = null;

            //act
            var record = Record.Exception(() => new GetBetsInProgressQueryHandler(queryBetRepository));

            //assert
            Assert.IsType<ArgumentNullException>(record);

        }

    }
}
