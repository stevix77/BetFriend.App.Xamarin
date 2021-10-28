namespace BetFriend.MobileApp.Views.Home
{
    using BetFriend.MobileApp.Views.InProgressBet;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentPage
    {
        public HomeView()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            await ViewModelLocator.Resolve<InProgressBetsViewModel>().LoadBets();
            base.OnAppearing();
        }
    }
}