
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BetFriend.MobileApp.Views.Register
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private readonly RegisterViewModel _vm;
        public RegisterPage()
        {
            InitializeComponent();
            _vm = ViewModelLocator.Resolve<RegisterViewModel>();
            BindingContext = _vm;
        }

        protected override void OnDisappearing()
        {
            _vm.Cleanup();
            base.OnDisappearing();
        }

        private void Entry_Completed(object sender, System.EventArgs e)
        {
            if (_vm.SignUpCommand.CanExecute(null))
                _vm.SignUpCommand.Execute(null);
        }
    }
}