using BetFriend.Domain.Bets.RetrieveBet;
using BetFriend.Domain.Bets.Usecases.AnswerBet;
using BetFriend.Domain.Users;
using BetFriend.MobileApp.Models;
using BetFriend.MobileApp.Navigation;
using BetFriend.MobileApp.Themes;
using BetFriend.MobileApp.Views.EditBet;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BetFriend.MobileApp.Views.DetailBet
{
    public class DetailBetViewModel : ViewModelBase
    {
        private BetViewModel _bet;
        private readonly IRetrieveBetQueryHandler _retrieveBetQueryHandler;
        private readonly IAnswerBetCommandHandler _answerBetCommandHandler;
        private readonly IAuthenticationService _authenticationService;
        private readonly INavigationService _navigationService;
        private Command _joinBetCommand;
        private Command _leaveBetCommand;

        public DetailBetViewModel(IRetrieveBetQueryHandler retrieveBetQueryHandler,
                                  IAnswerBetCommandHandler answerBetCommandHandler,
                                  IAuthenticationService authenticationService,
                                  INavigationService navigationService)
        {
            _retrieveBetQueryHandler = retrieveBetQueryHandler;
            _answerBetCommandHandler = answerBetCommandHandler;
            _authenticationService = authenticationService;
            _navigationService = navigationService;
        }

        public bool IsJoinCommandVisible
        {
            get => IsNotBetCreator() && IsNotMember();
        }

        public bool IsLeaveCommandVisible
        {
            get => IsNotBetCreator() && !IsNotMember();
        }

        public bool IsEditCommandVisible => !IsNotBetCreator();

        public string IconJoinCommand
        {
            get => MaterialDesignIcons.Plus;
        }

        public string IconLeaveCommand
        {
            get => MaterialDesignIcons.Minus;
        }

        public BetViewModel Bet
        {
            get => _bet;
            private set
            {
                if (Set(() => Bet, ref _bet, value))
                {
                    RaisePropertyChanged(nameof(Bet));
                    UpdateProperties();
                }
            }
        }

        public Command JoinBetCommand
        {
            get => _joinBetCommand ??= new Command(async () =>
            {
                await Join();
            }, () => CanJoinBet());
        }

        public Command LeaveBetCommand
        {
            get => _leaveBetCommand ??= new Command(async () =>
            {
                await Leave();
            }, () => CanLeaveBet());
        }

        public Command EditCommand
        {
            get => new Command(async () =>
            {
                await _navigationService.NavigateToAsync(nameof(EditBetView),
                                                            new Dictionary<string, object>()
                                                            {
                                                                { "bet", Newtonsoft.Json.JsonConvert.SerializeObject(_bet.ToBetOutput()) }
                                                            });
            });
        }

        private async Task Leave()
        {
            var command = new AnswerBetCommand(_bet.ToBetOutput(), false);
            await _answerBetCommandHandler.Handle(command);
            _ = Bet.Members.Remove(Bet.Members.FirstOrDefault(x => x.Id == Guid.Parse(_authenticationService.UserId)));
            UpdateProperties();
        }

        private async Task Join()
        {
            var command = new AnswerBetCommand(_bet.ToBetOutput(), true);
            await _answerBetCommandHandler.Handle(command);
            Bet.Members.Insert(0, new MemberViewModel(Guid.Parse(_authenticationService.UserId),
                                                            _authenticationService.Username));
            UpdateProperties();
        }

        private void UpdateProperties()
        {
            RaisePropertyChanged(nameof(IconJoinCommand));
            RaisePropertyChanged(nameof(IsJoinCommandVisible));
            RaisePropertyChanged(nameof(IconLeaveCommand));
            RaisePropertyChanged(nameof(IsLeaveCommandVisible));
            JoinBetCommand.ChangeCanExecute();
            LeaveBetCommand.ChangeCanExecute();
        }

        private bool CanJoinBet()
        {
            return _bet != null &&
                IsNotBetCreator() &&
                IsNotMember();
        }

        private bool CanLeaveBet()
        {
            return _bet != null &&
                IsNotBetCreator() &&
                !IsNotMember();
        }

        private bool IsNotMember()
        {
            return _bet != null &&
                   !_bet.Members.Any(x => x.Id == Guid.Parse(_authenticationService.UserId));
        }

        private bool IsNotBetCreator()
        {
            return _bet != null &&
                _bet.CreatorId != Guid.Parse(_authenticationService.UserId);
        }

        internal async void LoadBet(string value)
        {
            var bet = await _retrieveBetQueryHandler.Handle(new RetrieveBetQuery(Guid.Parse(value)));
            Bet = new BetViewModel(bet);
        }


    }
}
