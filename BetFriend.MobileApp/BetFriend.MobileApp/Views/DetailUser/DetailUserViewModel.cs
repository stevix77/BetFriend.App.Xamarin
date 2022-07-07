using BetFriend.Domain.Users;
using BetFriend.Domain.Users.Usecases.Subscribe;
using BetFriend.MobileApp.Events;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BetFriend.MobileApp.Views.DetailUser
{
    internal class DetailUserViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ISubscribeMemberCommandHandler _subscribeMemberCommandHandler;

        public DetailUserViewModel(ISubscribeMemberCommandHandler subscribeMemberCommandHandler, 
                                    IAuthenticationService authenticationService)
        {
            _subscribeMemberCommandHandler = subscribeMemberCommandHandler;
            _authenticationService = authenticationService;
        }

        private Guid _userId;
        public Guid UserId 
        {
            get => _userId;
            set
            {
                if (Set(() => UserId, ref _userId, value))
                    LoadUser();
            }
        }

        public string BtnSubscribeText
        {
            get => HasSubscribed ?
                Resources.Resource.UnSubscribe :
                Resources.Resource.Subscribe;
        }

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

        public Command SubscribeCommand
        {
            get => new Command(async () =>
            {
                await _subscribeMemberCommandHandler.Handle(new SubscribeMemberCommand(UserId));
                HasSubscribed = _authenticationService.User.Subscriptions.Contains(UserId);
                MessengerInstance.Send(new UserSubscribed(UserId, HasSubscribed));
            });
        }

        private void LoadUser()
        {

        }
    }
}
