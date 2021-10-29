using BetFriend.Domain.Users;
using BetFriend.Domain.Users.Usecases.Register;
using BetFriend.Infrastructure.Repositories.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BetFriend.MobileApp.UnitTests
{
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
        public async Task ShouldNotCreateUserIfUsernameOrEmailAlreadyExists()
        {
            var command = new RegisterCommand("username", "username@test.fr", "passwordUsername");
            var presenter = new RegisterTestPresenter();
            var userRepository = new InMemoryUserRepository("token", new List<User> { new User(command.Username, command.Email, command.Password) });
            var hashPassword = new FakeHashPassword();
            var handler = new RegisterCommandHandler(presenter, userRepository, hashPassword);

            await handler.Handle(command);

            Assert.Null(presenter.Token);
        }
    }

    internal class FakeHashPassword : IHashPassword
    {
        public FakeHashPassword()
        {
        }

        public string Hash(string password)
        {
            return password + password;
        }
    }

    internal class RegisterTestPresenter : IRegisterPresenter
    {
        public RegisterTestPresenter()
        {
        }

        public string Token { get; private set; }

        public void Present(RegisterResponse response)
        {
            Token = response.Token;
        }
    }
}
