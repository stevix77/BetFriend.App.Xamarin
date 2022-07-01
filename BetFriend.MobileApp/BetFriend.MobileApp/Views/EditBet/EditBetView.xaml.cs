using BetFriend.Domain.Bets.Dto;
using BetFriend.MobileApp.Views.DetailBet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BetFriend.MobileApp.Views.EditBet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(BetId), "betid")]
    [QueryProperty(nameof(Description), "description")]
    [QueryProperty(nameof(Coins), "coins")]
    [QueryProperty(nameof(EndDate), "enddate")]
    public partial class EditBetView : ContentPage
    {
        public string BetId
        {
            set => (BindingContext as EditBetViewModel).Id = value;
        }

        public string Description
        {
            set => (BindingContext as EditBetViewModel).Description = value;
        }

        public int Coins
        {
            set => (BindingContext as EditBetViewModel).Coins = value;
        }

        public string EndDate
        {
            set => (BindingContext as EditBetViewModel).EndDate = DateTime.Parse(value);
        }

        public EditBetView()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Resolve<EditBetViewModel>();
        }
    }
}