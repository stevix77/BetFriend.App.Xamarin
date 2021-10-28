﻿namespace BetFriend.MobileApp.Views.LaunchBet
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LaunchBetView : ContentPage
    {
        public LaunchBetView()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Resolve<LaunchBetViewModel>();
            //BindingContext = DependencyService.Resolve<LaunchBetViewModel>();
        }
    }
}