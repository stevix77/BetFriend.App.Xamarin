using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BetFriend.MobileApp.Usecases.LaunchBet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LaunchBetView : ContentPage
    {
        public LaunchBetView()
        {
            InitializeComponent();
            //BindingContext = AppShell.
        }
    }
}