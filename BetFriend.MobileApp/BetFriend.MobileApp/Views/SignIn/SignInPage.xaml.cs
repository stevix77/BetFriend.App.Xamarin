namespace BetFriend.MobileApp.Views.SignIn
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPage : ContentPage
    {
        private readonly SignInViewModel _vm;
        public SignInPage()
        {
            InitializeComponent();
            _vm = ViewModelLocator.Resolve<SignInViewModel>();
            BindingContext = _vm;
        }

        protected override void OnDisappearing()
        {
            _vm.Cleanup();
            base.OnDisappearing();
        }

        private void Entry_Completed(object sender, System.EventArgs e)
        {
            if (_vm.LoginCommand.CanExecute(null))
                _vm.LoginCommand.Execute(null);
        }
    }
}