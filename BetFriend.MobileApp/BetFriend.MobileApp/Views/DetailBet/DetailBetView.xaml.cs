﻿
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BetFriend.MobileApp.Views.DetailBet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(BetId), "betid")]
    public partial class DetailBetView : ContentPage
    {
        public string BetId
        {
            set => Task.Run(() => (BindingContext as DetailBetViewModel).LoadBet(value));
        }
        public DetailBetView()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Resolve<DetailBetViewModel>();
        }
    }
}