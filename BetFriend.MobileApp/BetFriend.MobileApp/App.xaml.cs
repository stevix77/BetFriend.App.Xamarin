using BetFriend.Domain.Bets;
using BetFriend.Infrastructure.Repositories.InMemory;
using BetFriend.MobileApp.Usecases.LaunchBet;
using GalaSoft.MvvmLight.Messaging;
using System;
using Xamarin.Forms;

namespace BetFriend.MobileApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjM4MTQzQDMxMzgyZTMxMmUzMFROMlU1RTMxQVY3RFV3RmNUeGF0STdnMjliVGFlVzRnVDFIcUhvVDZadlU9");
            RegisterDependency();
            var appShell = new AppShell();
            appShell.GoToAsync($"//{nameof(LaunchBetView)}").Wait();
            MainPage = appShell;
        }

        private void RegisterDependency()
        {
            DependencyService.Register<IBetRepository, InMemoryBetRepository>();
            DependencyService.Register<IMessenger, Messenger>();
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
