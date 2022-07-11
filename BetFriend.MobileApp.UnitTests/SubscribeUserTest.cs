using BetFriend.Domain.Users;
using BetFriend.Domain.Users.Dto;
using BetFriend.Domain.Users.Usecases.Subscribe;
using BetFriend.Infrastructure.Repositories.InMemory;
using BetFriend.MobileApp.UnitTests.Implems;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BetFriend.MobileApp.UnitTests
{
    public class SubscribeUserTest
    {
        [Fact]
        public async Task ShouldSubscribeWhenCurrentUserIsNotSubscribed()
        {
            var userToSubscribe = new UserOutput { Id = Guid.NewGuid(), Username = "username" };
            var currentUser = new User("stevix", "email", "pwd");
            var repository = new InMemoryUserRepository(currentUser);
            var authenticationService = new InMemoryAuthenticationService(currentUser);
            var handler = new SubscribeMemberCommandHandler(repository, authenticationService);
            var command = new SubscribeMemberCommand(userToSubscribe.Id);
            await handler.Handle(command);
            Assert.Contains(authenticationService.GetSubscriptions(), x => x == userToSubscribe.Id);
            Assert.Contains(currentUser.Subscriptions, x => x == userToSubscribe.Id);
        }

        [Fact]
        public async Task ShouldUnSubscribeWhenAlreadySubscribed()
        {
            var userToSubscribe = new UserOutput { Id = Guid.NewGuid(), Username = "username" };
            var currentUser = new User("stevix", "email", "pwd");
            currentUser.AddSubscription(userToSubscribe.Id);
            var authenticationService = new InMemoryAuthenticationService(currentUser);
            var repository = new InMemoryUserRepository(currentUser);
            var handler = new SubscribeMemberCommandHandler(repository, authenticationService);
            var command = new SubscribeMemberCommand(userToSubscribe.Id);
            await handler.Handle(command);
            Assert.Empty(authenticationService.GetSubscriptions());
            Assert.Empty(currentUser.Subscriptions);
        }
    }
}
