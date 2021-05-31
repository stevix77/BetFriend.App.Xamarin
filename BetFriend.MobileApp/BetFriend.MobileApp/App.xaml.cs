using BetFriend.Domain.Bets;
using BetFriend.Infrastructure.Repositories.InMemory;
using Xamarin.Forms;

namespace BetFriend.MobileApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<IBetRepository, InMemoryBetRepository>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
