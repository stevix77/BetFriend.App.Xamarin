using BetFriend.Domain.Users.Usecases.SignIn;
using BetFriend.MobileApp.Navigation;
using BetFriend.MobileApp.Views.Home;
using BetFriend.MobileApp.Views.Register;
using GalaSoft.MvvmLight;
using Xamarin.Forms;

namespace BetFriend.MobileApp.Views.SignIn
{
    public class SignInViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ISignInCommandHandler _handler;
        private readonly SignInPresenter _presenter;

        public SignInViewModel(INavigationService navigationService,
                                 ISignInCommandHandler signInCommandHandler,
                                 SignInPresenter registerPresenter)
        {
            _navigationService = navigationService;
            _handler = signInCommandHandler;
            _presenter = registerPresenter;
        }

        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                if (Set(() => Login, ref _login, value))
                {
                    RaisePropertyChanged(nameof(Login));
                    LoginCommand.ChangeCanExecute();
                }
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                if (Set(() => Password, ref _password, value))
                {
                    RaisePropertyChanged(nameof(Password));
                    LoginCommand.ChangeCanExecute();
                }
            }
        }

        private Command _loginCommand;
        public Command LoginCommand
        {
            get => _loginCommand ?? (_loginCommand = new Command(async () =>
            {
                var command = new SignInCommand(_login, _password);
                await _handler.Handle(command).ConfigureAwait(false);
                if (_presenter.HasError)
                {
                    MessengerInstance.Send(_presenter.GetError(), this);
                    return;
                }
                var page = _navigationService.Init(nameof(HomeView));
                App.Current.MainPage = page;
            }, () => CanValidate()));
        }

        private Command _signUpCommand;
        public Command SignUpCommand
        {
            get => _signUpCommand ?? (_signUpCommand = new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PushModalAsync(new RegisterPage());
            }));
        }

        private bool CanValidate()
        {
            if (string.IsNullOrEmpty(_login) || string.IsNullOrEmpty(_password))
                return false;
            return true;
        }

        public override void Cleanup()
        {
            Login = null;
            Password = null;
            base.Cleanup();
        }
    }
}