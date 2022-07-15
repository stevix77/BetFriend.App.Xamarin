using BetFriend.MobileApp.Events;
using BetFriend.MobileApp.Navigation;
using GalaSoft.MvvmLight;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BetFriend.MobileApp.Views.CloseBet
{
    public class CloseBetViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public CloseBetViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        
        public string BetId { get; internal set; }

        private bool _isSuccess;
        public bool IsSuccess
        {
            get => _isSuccess;
            set
            {
                if (Set(() => IsSuccess, ref _isSuccess, value))
                    RaisePropertyChanged(nameof(IsSuccess));
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (Set(() => Description, ref _description, value))
                    RaisePropertyChanged(nameof(Description));
            }
        }

        public ImageSource ImgSource { get; set; }

        public Command ValidateCommand
        {
            get => new Command(async () =>
            {
                await _navigationService.GoBack();
                MessengerInstance.Send(new BetOver(BetId, IsSuccess, Description));
            });
        }

        public Command PickImageCommand
        {
            get => new Command(async () =>
            {
                var result = await MediaPicker.CapturePhotoAsync();
                var stream = await result.OpenReadAsync();
                ImgSource = ImageSource.FromStream(() => stream);
                RaisePropertyChanged(nameof(ImgSource));
            });
        }
    }
}
