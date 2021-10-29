using BetFriend.MobileApp.Navigation;
using BetFriend.MobileApp.Views.Home;
using GalaSoft.MvvmLight;

namespace BetFriend.MobileApp.Views.Register
{
    public class RegisterViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public RegisterViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            var shell = new AppShell();
            shell.GoToAsync($"//{nameof(HomeView)}").Wait();
            Xamarin.Forms.Application.Current.MainPage = shell;
        }
    }
}
