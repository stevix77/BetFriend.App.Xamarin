namespace BetFriend.MobileApp
{
    using BetFriend.MobileApp.Views.Home;
    using BetFriend.MobileApp.Views.LaunchBet;
    using Xamarin.Forms;

    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(LaunchBetView), typeof(LaunchBetView));
            Routing.RegisterRoute(nameof(HomeView), typeof(HomeView));
        }
    }
}
