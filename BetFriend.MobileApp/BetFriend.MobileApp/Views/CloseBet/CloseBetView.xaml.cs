using BetFriend.Domain.Bets.Dto;
using BetFriend.MobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BetFriend.MobileApp.Views.CloseBet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(BetId), "betid")]
    public partial class CloseBetView : ContentPage
    {
        public string BetId
        {
            set
            {
                (BindingContext as CloseBetViewModel).BetId = value;
            }
        }

        public CloseBetView()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Resolve<CloseBetViewModel>();
        }
    }
}