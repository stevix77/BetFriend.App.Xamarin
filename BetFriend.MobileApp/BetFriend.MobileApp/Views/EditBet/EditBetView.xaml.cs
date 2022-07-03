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
            set => (BindingContext as EditBetViewModel).Bet = JsonConvert.DeserializeObject<BetViewModel>(value);
        }

        public EditBetView()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Resolve<EditBetViewModel>();
        }
    }
}