using BetFriend.Domain.Users.Usecases.Register;

namespace BetFriend.MobileApp.UnitTests.Implems
{
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
