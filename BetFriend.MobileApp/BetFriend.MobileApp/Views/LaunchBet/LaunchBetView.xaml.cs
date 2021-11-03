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

        private void Entry_Completed(object sender, System.EventArgs e)
        {
            if (_vm.ValidateCommand.CanExecute(null))
                _vm.ValidateCommand.Execute(null);
        }
    }
}