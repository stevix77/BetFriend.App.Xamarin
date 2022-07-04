using BetFriend.Domain.Bets.Dto;
using BetFriend.MobileApp.Models;
using Newtonsoft.Json;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BetFriend.MobileApp.Views.EditBet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(Bet), "bet")]
    public partial class EditBetView : ContentPage
    {
        public string Bet
        {
            set
            {
                var betOutput = JsonConvert.DeserializeObject<BetOutput>(value);
                (BindingContext as EditBetViewModel).Bet = new BetViewModel(betOutput);
            }
        }

        public EditBetView()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Resolve<EditBetViewModel>();
        }
    }
}