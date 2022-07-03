namespace BetFriend.MobileApp.Views.LaunchBet
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LaunchBetView : ContentPage
    {
        private readonly LaunchBetViewModel _vm;
        public LaunchBetView()
        {
            InitializeComponent();
            _vm = ViewModelLocator.Resolve<LaunchBetViewModel>();
            BindingContext = _vm;
        }

        protected override void OnDisappearing()
        {
            _vm.Cleanup();
            base.OnDisappearing();
        }
    }
}