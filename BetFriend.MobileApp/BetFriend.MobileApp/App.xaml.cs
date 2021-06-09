namespace BetFriend.MobileApp
{
    using BetFriend.Domain.Bets;
    using BetFriend.Infrastructure.Repositories.InMemory;
    using BetFriend.MobileApp.Views.LaunchBet;
    using GalaSoft.MvvmLight.Messaging;
    using System;
    using Xamarin.Forms;

    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDU3NTY2QDMxMzkyZTMxMmUzMFQ5a3FEem5ldkRvTzhVUndDS0ZhcXI3ZE9oaXVIRzF4UFVTeTdmcDFCV289");
            RegisterDependency();
            var appShell = new AppShell();
            appShell.GoToAsync($"//{nameof(LaunchBetView)}").Wait();
            MainPage = appShell;
        }

        private void RegisterDependency()
        {
            DependencyService.RegisterSingleton<IBetRepository>(new InMemoryBetRepository(Guid.NewGuid()));
            DependencyService.Register<IMessenger, Messenger>();
            DependencyService.Register<LaunchBetViewModel>();
        }
    }
}
