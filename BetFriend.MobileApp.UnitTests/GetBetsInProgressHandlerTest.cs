using BetFriend.Domain.Bets;
using BetFriend.Domain.Bets.Dto;
using BetFriend.Domain.Bets.GetBetsInProgress;
using BetFriend.Domain.Users;
using BetFriend.Infrastructure.Repositories.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BetFriend.MobileApp.UnitTests
{
    public class GetBetsInProgressHandlerTest
    {
        [Fact]
        public async Task HandleShouldReturnEmptyListBetIfMemberHasNeverBet()
        {
            //arrange
            var handler = new GetBetsInProgressQueryHandler(new InMemoryBetRepository());

            //act
            var bets = await handler.Handle();

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
            var betRepository = new InMemoryBetRepository(new List<BetOutput>()
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

            //act
            var bets = await handler.Handle();

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
            var betRepository = new InMemoryBetRepository(new List<BetOutput>()
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

            //act
            IReadOnlyCollection<BetOutput> bets = await handler.Handle();

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
        public void CtorShouldThrowArgumentNullExceptionIfParamsNull()
        {
            //arrange
            IBetRepository betRepository = null;

            //act
            var record = Record.Exception(() => new GetBetsInProgressQueryHandler(betRepository));

            //assert
            Assert.IsType<ArgumentNullException>(record);

        }

    }
}
