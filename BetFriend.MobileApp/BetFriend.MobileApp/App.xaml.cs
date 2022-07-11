namespace BetFriend.MobileApp
{
    using BetFriend.Domain.Abstractions;
    using BetFriend.Domain.Users;
    using BetFriend.Domain.Users.Usecases.GetInfo;
    using BetFriend.MobileApp.Views.Home;
    using BetFriend.MobileApp.Views.SignIn;
    using Microsoft.AppCenter;
    using Microsoft.AppCenter.Analytics;
    using Microsoft.AppCenter.Crashes;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public partial class App : Application
    {
        private const string KEY_STORAGE_TOKEN = "token";

        public App()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDU3NTY2QDMxMzkyZTMxMmUzMFQ5a3FEem5ldkRvTzhVUndDS0ZhcXI3ZE9oaXVIRzF4UFVTeTdmcDFCV289");
            ViewModelLocator.Register();
            MainPage = InitMainPage();
        }

        protected override void OnSleep()
        {
            var authenticationService = ViewModelLocator.Resolve<IAuthenticationService>();
            if (!string.IsNullOrEmpty(authenticationService.Token))
                ViewModelLocator.Resolve<IDataStorage>().SaveData(authenticationService.Token, KEY_STORAGE_TOKEN);
            base.OnSleep();
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=79efb9b7-36d4-45e4-be57-728e1df644a7;" +
                  "uwp=a2494c59-4149-4a03-b0b7-95d3a5f5191b;" +
                  "ios=4761a9e1-abf2-4ba2-9820-73ad7c950e29",
                  typeof(Analytics), typeof(Crashes));
            base.OnStart();
        }

        private Page InitMainPage()
        {
            var storage = ViewModelLocator.Resolve<IDataStorage>();
            var token = storage.GetData<string>(KEY_STORAGE_TOKEN);
            if (string.IsNullOrEmpty(token))
                return new SignInPage();
            SetToken(token);
            Task.Run(async() =>
            {
                var info = await ViewModelLocator.Resolve<IGetInfoQueryHandler>().Handle(new());
                if (info != InfoOutput.Empty)
                    SetInfo(info);
            });
            var appShell = new AppShell();
            appShell.GoToAsync($"//{nameof(HomeView)}").Wait();
            return appShell;
        }

        private void SetInfo(InfoOutput info)
        {
            var authenticationService = ViewModelLocator.Resolve<IAuthenticationService>();
            authenticationService.SetInfo(info.Coins, info.Subscriptions);
        }

        private void SetToken(string token)
        {
            var authenticationService = ViewModelLocator.Resolve<IAuthenticationService>();
            authenticationService.SetToken(token);
        }
    }
}
