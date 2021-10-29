using System.Threading.Tasks;

namespace BetFriend.Domain.Users.Usecases.Register
{
    public class RegisterCommandHandler : IRegisterCommandHandler
    {
        private readonly IRegisterPresenter _presenter;
        private readonly IUserRepository _userRepository;
        private readonly IHashPassword _hashPassword;

        public RegisterCommandHandler(IRegisterPresenter registerPresenter,
                                      IUserRepository userRepository,
                                      IHashPassword hashPassword)
        {
            _presenter = registerPresenter;
            _userRepository = userRepository;
            _hashPassword = hashPassword;
        }

        public async Task Handle(RegisterCommand command)
        {
            string password = _hashPassword.Hash(command.Password);
            var user = new User(command.Username, command.Email, password);
            var token = await _userRepository.SaveAsync(user);
            if (string.IsNullOrEmpty(token))
                return;
            _presenter.Present(new RegisterResponse { Token = token });
        }
    }
}
