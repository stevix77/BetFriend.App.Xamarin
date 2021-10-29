namespace BetFriend.MobileApp
{
    using BetFriend.MobileApp.Views.DetailBet;
    using BetFriend.MobileApp.Views.Home;
    using BetFriend.MobileApp.Views.LaunchBet;
    using BetFriend.MobileApp.Views.Register;
    using Xamarin.Forms;

    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(LaunchBetView), typeof(LaunchBetView));
            Routing.RegisterRoute(nameof(HomeView), typeof(HomeView));
            Routing.RegisterRoute(nameof(DetailBetView), typeof(DetailBetView));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        }
    }
}
