namespace BetFriend.MobileApp
{
    using BetFriend.Domain.Abstractions;
    using BetFriend.Domain.Users;
    using BetFriend.MobileApp.Views.DetailBet;
    using BetFriend.MobileApp.Views.EditBet;
    using BetFriend.MobileApp.Views.Home;
    using BetFriend.MobileApp.Views.LaunchBet;
    using BetFriend.MobileApp.Views.Register;
    using BetFriend.MobileApp.Views.SignIn;
    using Xamarin.Forms;

    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(LaunchBetView), typeof(LaunchBetView));
            Routing.RegisterRoute(nameof(HomeView), typeof(HomeView));
            Routing.RegisterRoute(nameof(DetailBetView), typeof(DetailBetView));
            Routing.RegisterRoute(nameof(EditBetView), typeof(EditBetView));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        }

        private void MenuItem_Clicked(object sender, System.EventArgs e)
        {
            ViewModelLocator.Resolve<IAuthenticationService>().SetToken(null);
            ViewModelLocator.Resolve<IDataStorage>().DeleteData("token");
            App.Current.MainPage = new SignInPage();
        }
    }
}
