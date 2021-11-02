using System.Threading.Tasks;

namespace BetFriend.Domain.Users.Usecases.SignIn
{
    public class SignInCommandHandler : ISignInCommandHandler
    {
        private readonly IAuthenticationGateway _authenticationGateway;
        private readonly ISignInPresenter _signInPresenter;
        private readonly IHashPassword _hashPassword;

        public SignInCommandHandler(IAuthenticationGateway authenticationGateway,
                                    ISignInPresenter signInPresenter,
                                      IHashPassword hashPassword)
        {
            _authenticationGateway = authenticationGateway;
            _signInPresenter = signInPresenter;
            _hashPassword = hashPassword;
        }

        public async Task Handle(SignInCommand command)
        {
            var password = _hashPassword.Hash(command.Password);
            var token = await _authenticationGateway.GetTokenAsync(command.Login, password).ConfigureAwait(false);
            if (!string.IsNullOrEmpty(token))
                _signInPresenter.Present(token);
            else
                _signInPresenter.Fail("Identification failed");
        }
    }
}
