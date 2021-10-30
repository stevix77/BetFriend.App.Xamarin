using BetFriend.Domain.Users;
using BetFriend.Domain.Users.Usecases.SignIn;
using BetFriend.Infrastructure.Repositories.InMemory;
using System.Threading.Tasks;
using Xunit;

namespace BetFriend.MobileApp.UnitTests
{
    public class SignInHandlerTest
    {
        [Fact]
        public async Task ShouldSignInIfLoginPasswordOk()
        {
            var user = new User("username", "email@email.com", "password");
            var repository = new InMemoryUserRepository("token", new() { user });
            var command = new SignInCommand("username", "password");
            var presenter = new SignInTestPresenter();
            var handler = new SignInCommandHandler(repository, presenter);

            await handler.Handle(command);


        }
    }

    internal class SignInTestPresenter : ISignInPresenter
    {
        public SignInTestPresenter()
        {
        }
    }
}
