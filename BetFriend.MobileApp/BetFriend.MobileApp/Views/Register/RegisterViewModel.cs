using BetFriend.Domain.Users.Usecases.Register;
using BetFriend.MobileApp.Navigation;
using BetFriend.MobileApp.Views.Home;
using BetFriend.MobileApp.Views.SignIn;
using GalaSoft.MvvmLight;
using System;
using Xamarin.Forms;

namespace BetFriend.MobileApp.Views.Register
{
    public class RegisterViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IRegisterCommandHandler _handler;
        private readonly RegisterPresenter _presenter;

        public RegisterViewModel(INavigationService navigationService,
                                 IRegisterCommandHandler registerCommandHandler,
                                 RegisterPresenter registerPresenter)
        {
            _navigationService = navigationService;
            _handler = registerCommandHandler;
            _presenter = registerPresenter;
        }

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (Set(() => Username, ref _username, value))
                {
                    RaisePropertyChanged(nameof(Username));
                    SignUpCommand.ChangeCanExecute();
                }
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                if (Set(() => Email, ref _email, value))
                {
                    RaisePropertyChanged(nameof(Email));
                    SignUpCommand.ChangeCanExecute();
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
                    SignUpCommand.ChangeCanExecute();
                }
            }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                if (Set(() => ConfirmPassword, ref _confirmPassword, value))
                {
                    RaisePropertyChanged(nameof(ConfirmPassword));
                    SignUpCommand.ChangeCanExecute();
                }
            }
        }

        private Command _signUpCommand;
        public Command SignUpCommand
        {
            get => _signUpCommand ?? (_signUpCommand = new Command(async() =>
            {
                try
                {
                    var command = new RegisterCommand(_username, _email, _password);
                    await _handler.Handle(command);
                    var page = _navigationService.Init(nameof(HomeView));
                    App.Current.MainPage = page;
                }
                catch
                {

                }
            }, () => CanValidate()));
        }

        private Command _signInCommand;
        public Command SignInCommand
        {
            get => _signInCommand ?? (_signInCommand = new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PushModalAsync(new SignInPage());
            }));
        }

        private bool CanValidate()
        {
            if (string.IsNullOrEmpty(_username)
              || string.IsNullOrEmpty(_email)
              || string.IsNullOrEmpty(_password)
              || string.IsNullOrEmpty(_confirmPassword))
                return false;
            if (!_confirmPassword.Equals(_password))
                return false;
            return true;
        }
    }
}
