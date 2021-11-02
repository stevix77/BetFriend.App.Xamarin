namespace BetFriend.MobileApp.UnitTests
{
    using BetFriend.Domain.Users.Usecases;
    using BetFriend.Domain.Users.Usecases.SignIn;
    using BetFriend.Infrastructure.Gateways;
    using BetFriend.MobileApp.UnitTests.Implems;
    using System.Threading.Tasks;
    using Xunit;


    public class SignInHandlerTest
    {
        [Fact]
        public async Task ShouldSignInIfLoginPasswordOk()
        {
            var authentication = new Authentication("username", "passwordpassword", "token");
            var authenticationGateway = new InMemoryAuthenticationGateway(authentication);
            var command = new SignInCommand("username", "password");
            var presenter = new SignInTestPresenter();
            var handler = new SignInCommandHandler(authenticationGateway, presenter, new FakeHashPassword());

            await handler.Handle(command);

            Assert.Equal("token", presenter.GetToken());
        }

        [Fact]
        public async Task ShouldNotGetTokenIfAuthenticationFailed()
        {
            var authentication = new Authentication("user", "pass", "token");
            var authenticationGateway = new InMemoryAuthenticationGateway(authentication);
            var command = new SignInCommand("username", "password");
            var presenter = new SignInTestPresenter();
            var handler = new SignInCommandHandler(authenticationGateway, presenter, new FakeHashPassword());

            await handler.Handle(command);

            Assert.Collection(presenter.GetErrors(), x =>
            {
                Assert.Equal("Identification failed", x);
            });
        }
    }
}
