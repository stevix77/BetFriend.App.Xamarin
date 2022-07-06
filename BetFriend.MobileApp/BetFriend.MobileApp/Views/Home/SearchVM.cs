using GalaSoft.MvvmLight;
using System;
using Xamarin.Forms;

namespace BetFriend.MobileApp.Views.Home
{
    public class SearchVM : ViewModelBase
    {
        public SearchVM(Guid id, string username, bool hasSubscribed)
        {
            UserId = id;
            Username = username;
            HasSubscribed = hasSubscribed;
        }
        public Guid UserId { get; set; }
        public string Username { get; set; }

        private bool _hasSubscribed;
        public bool HasSubscribed
        {
            get => _hasSubscribed; 
            set
            {
                if (Set(() => HasSubscribed, ref _hasSubscribed, value))
                {
                    RaisePropertyChanged(nameof(HasSubscribed));
                    RaisePropertyChanged(nameof(BtnSubscribeText));
                }
            }
        }
        public string BtnSubscribeText { get => HasSubscribed ? Resources.Resource.UnSubscribe : Resources.Resource.Subscribe; }

        private Command _subscribeCommand;
        public Command SubscribeCommand
        {
            get => _subscribeCommand ??= new Command(async () =>
            {
                HasSubscribed = !HasSubscribed;
            });
        }
    }
}
