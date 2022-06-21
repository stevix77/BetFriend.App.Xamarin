using BetFriend.Domain.Users;
using BetFriend.Domain.Users.Usecases.Register;

namespace BetFriend.MobileApp.Views.Register
{
    public class RegisterPresenter : IRegisterPresenter
    {
        private readonly IAuthenticationService _authenticationService;
        public RegisterPresenter(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public void Present(string token)
        {
            _authenticationService.SetToken(token);
        }
    }
}
