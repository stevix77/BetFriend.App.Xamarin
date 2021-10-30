using System.Threading.Tasks;

namespace BetFriend.Domain.Users.Usecases.SignIn
{
    public class SignInCommandHandler : ISignInCommandHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly ISignInPresenter _signInPresenter;

        public SignInCommandHandler(IUserRepository repository, ISignInPresenter signInPresenter)
        {
            _userRepository = repository;
            _signInPresenter = signInPresenter;
        }

        public Task Handle(SignInCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}
