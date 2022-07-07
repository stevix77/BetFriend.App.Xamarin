using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BetFriend.MobileApp.Views.DetailUser
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(UserId), "userid")]
    [QueryProperty(nameof(HasSubscribed), "hassubscribed")]
    public partial class DetailUserView : ContentPage
    {
        public string UserId
        {
            set
            { 
                (BindingContext as DetailUserViewModel).UserId = Guid.Parse(value);
            }
        }
        public string HasSubscribed { set => (BindingContext as DetailUserViewModel).HasSubscribed = bool.Parse(value); }
        public DetailUserView()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Resolve<DetailUserViewModel>();
        }
    }
}