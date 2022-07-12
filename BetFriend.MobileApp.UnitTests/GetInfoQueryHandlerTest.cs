using BetFriend.Domain.Users;
using BetFriend.Domain.Users.Dto;
using BetFriend.Domain.Users.Usecases.GetInfo;
using BetFriend.Infrastructure.Repositories.InMemory;
using BetFriend.MobileApp.UnitTests.Implems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BetFriend.MobileApp.UnitTests
{
    public class GetInfoQueryHandlerTest
    {
        [Fact]
        public async Task ShouldReturnInformationAboutUser()
        {
            var id = Guid.NewGuid();
            var user = new User("stevix", "email", "pwd");
            user.AddSubscription(Guid.Parse("4567d033-8dd4-4369-bc35-104b5bb17181"));
            user.Coins = 400;
            var repository = new InMemoryUserRepository(user);
            var handler = new GetInfoQueryHandler(repository);
            var query = new GetInfoQuery();
            var infos = await handler.Handle(query);
            Assert.Equal(user.Coins, infos.Coins);
            Assert.Equal(user.Subscriptions, infos.Subscriptions);
        }

        [Fact]
        public async Task ShouldReturnInfoEmptyWhenUserIsNotExists()
        {
            var repository = new InMemoryUserRepository("token");
            var handler = new GetInfoQueryHandler(repository);
            var query = new GetInfoQuery();
            var infos = await handler.Handle(query);
            Assert.Null(infos);
        }
    }
}
