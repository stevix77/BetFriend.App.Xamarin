namespace BetFriend.MobileApp.UnitTests
{
    using BetFriend.Domain.Users;
    using BetFriend.Domain.Users.Usecases.Register;
    using BetFriend.Infrastructure.Hash;
    using BetFriend.Infrastructure.Repositories.InMemory;
    using BetFriend.MobileApp.UnitTests.Implems;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;


    public class RegisterHandlerTest
    {
        [Fact]
        public async Task ShouldCreateUser()
        {
            var token = "token";
            var command = new RegisterCommand("username", "username@test.fr", "passwordUsername");
            var presenter = new RegisterTestPresenter();
            var userRepository = new InMemoryUserRepository(token);
            var hashPassword = new FakeHashPassword();
            var handler = new RegisterCommandHandler(presenter, userRepository, hashPassword);

            await handler.Handle(command);

            Assert.Equal(token, presenter.Token);
        }

        [Fact]
        public async Task ShouldNotCreateUserIfUsernameAlreadyExists()
        {
            var command = new RegisterCommand("username", "username@test.fr", "passwordUsername");
            var presenter = new RegisterTestPresenter();
            var userRepository = new InMemoryUserRepository("token", new List<User> { new User(command.Username, command.Email, command.Password) });
            var hashPassword = new FakeHashPassword();
            var handler = new RegisterCommandHandler(presenter, userRepository, hashPassword);

            await handler.Handle(command);

            Assert.Null(presenter.Token);
        }

        [Fact]
        public async Task ShouldNotCreateUserIfEmailAlreadyExists()
        {
            var command = new RegisterCommand("username", "username@test.fr", "passwordUsername");
            var presenter = new RegisterTestPresenter();
            var userRepository = new InMemoryUserRepository("token", new List<User> { new User("login", command.Email, command.Password) });
            var hashPassword = new FakeHashPassword();
            var handler = new RegisterCommandHandler(presenter, userRepository, hashPassword);

            await handler.Handle(command);

            Assert.Null(presenter.Token);
        }
    }


}
