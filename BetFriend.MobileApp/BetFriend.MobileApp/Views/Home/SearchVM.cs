using GalaSoft.MvvmLight;
using System;

namespace BetFriend.MobileApp.Views.Home
{
    public class SearchVM : ViewModelBase
    {
        public SearchVM(Guid userId, string username, bool hasSubscribed)
        {
            UserId = userId;
            Username = username;
            HasSubscribed = hasSubscribed;
        }
        public Guid UserId { get; }
        public string Username { get; }

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
        public string BtnSubscribeText
        {
            get => HasSubscribed ?
                Resources.Resource.UnSubscribe :
                Resources.Resource.Subscribe;
        }

    }
}
