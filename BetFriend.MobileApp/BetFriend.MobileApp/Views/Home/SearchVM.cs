using GalaSoft.MvvmLight;
using System;
using Xamarin.Forms;

namespace BetFriend.MobileApp.Views.Home
{
    public class SearchVM : ViewModelBase
    {
        public SearchVM(Guid id, string username, bool isFollow)
        {
            UserId = id;
            Username = username;
            IsFollow = isFollow;
        }
        public Guid UserId { get; set; }
        public string Username { get; set; }

        private bool _isFollow;
        public bool IsFollow 
        {
            get => _isFollow; 
            set
            {
                if (Set(() => IsFollow, ref _isFollow, value))
                {
                    RaisePropertyChanged(nameof(IsFollow));
                    RaisePropertyChanged(nameof(BtnFollowText));
                }
            }
        }
        public string BtnFollowText { get => IsFollow ? Resources.Resource.UnFollow : Resources.Resource.Follow; }

        private Command _followCommand;
        public Command FollowCommand
        {
            get => _followCommand ??= new Command(async () =>
            {
                IsFollow = !IsFollow;
            });
        }
    }
}
