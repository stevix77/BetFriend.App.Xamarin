using BetFriend.MobileApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace BetFriend.MobileApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}