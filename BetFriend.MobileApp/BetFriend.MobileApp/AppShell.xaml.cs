using BetFriend.MobileApp.Usecases.LaunchBet;
using System;
using Xamarin.Forms;

namespace BetFriend.MobileApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(LaunchBetView), typeof(LaunchBetView));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
