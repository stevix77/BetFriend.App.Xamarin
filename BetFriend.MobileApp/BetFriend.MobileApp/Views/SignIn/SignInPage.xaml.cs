namespace BetFriend.MobileApp.Views.SignIn
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Resolve<SignInViewModel>();
        }
    }
}