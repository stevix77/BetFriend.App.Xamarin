namespace BetFriend.MobileApp.Views.InProgressBet
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InProgressBetsView : ContentView
    {
        public InProgressBetsView()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Resolve<InProgressBetsViewModel>();
        }
    }
}