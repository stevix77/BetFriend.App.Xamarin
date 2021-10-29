﻿namespace BetFriend.MobileApp
{
    using BetFriend.MobileApp.Views.Home;
    using System;
    using Xamarin.Forms;

    public partial class App : Application
    {
        private static readonly Guid _currentUser = Guid.NewGuid();
        private static readonly string _currentUsername = "Toto";
        public static Guid CurrentUser { get => _currentUser; }
        public static string CurrentUsername { get => _currentUsername; }
        public App()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDU3NTY2QDMxMzkyZTMxMmUzMFQ5a3FEem5ldkRvTzhVUndDS0ZhcXI3ZE9oaXVIRzF4UFVTeTdmcDFCV289");
            ViewModelLocator.RegisterDependencies();
            var appShell = new AppShell();
            appShell.GoToAsync($"//{nameof(HomeView)}").Wait();
            MainPage = appShell;
        }
    }
}
