using BetFriend.Domain.Abstractions;
using BetFriend.Domain.Bets.Usecases.UpdateBet;
using BetFriend.Domain.Users;
using BetFriend.MobileApp.Models;
using BetFriend.MobileApp.Navigation;
using BetFriend.MobileApp.Views.DetailBet;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BetFriend.MobileApp.Views.EditBet
{
    internal class EditBetViewModel : ViewModelBase
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUpdateBetCommandHandler _updateBetCommandHandler;
        private readonly INavigationService _navigationService;
        private readonly IAuthenticationService _authenticationService;

        public EditBetViewModel(IDateTimeProvider dateTimeProvider,
                                IUpdateBetCommandHandler updateBetCommandHandler, 
                                INavigationService navigationService,
                                IAuthenticationService authenticationService)
        {
            _dateTimeProvider = dateTimeProvider;
            _updateBetCommandHandler = updateBetCommandHandler;
            _navigationService = navigationService;
            _authenticationService = authenticationService;
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (Set(() => Description, ref _description, value))
                {
                    RaisePropertyChanged(nameof(Description));
                    ValidateCommand.ChangeCanExecute();
                }
            }
        }
        private int _coins;
        public int Coins 
        {
            get => _coins;
            set
            {
                if (Set(() => Coins, ref _coins, value))
                    RaisePropertyChanged(nameof(Coins));
            }
        }

        private DateTime _endDate;
        public DateTime EndDate 
        {
            get => _endDate;
            set
            {
                if(Set(() => EndDate, ref _endDate, value))
                {
                    RaisePropertyChanged(nameof(EndDate));
                    ValidateCommand.ChangeCanExecute();
                }
            }
        }
        public DateTime MinimumDate { get; } = DateTime.Now;

        private TimeSpan _endTime;
        public TimeSpan EndTime
        {
            get => _endTime;
            set
            {
                if (Set(() => EndTime, ref _endTime, value))
                {
                    RaisePropertyChanged(nameof(EndTime));
                    EndDate = EndDate.Add(value);
                }
            }
        }

        private Command _validateCommand;
        public Command ValidateCommand
        {
            get => _validateCommand ??= new Command(async () =>
            {
                await _updateBetCommandHandler.Handle(new UpdateBetCommand(_bet.ToBetOutput(),
                                                                           _description,
                                                                           _endDate,
                                                                           _coins));
                await _navigationService.NavigateToAsync(nameof(DetailBetView),
                                                            new Dictionary<string, object>()
                                                            {
                                                                { "betid", _bet.Id }
                                                            });
            }, () => CanValideCommand());
        }

        private BetViewModel _bet;
        public BetViewModel Bet 
        {
            get => _bet; 
            internal set
            {
                if(Set(() => Bet, ref _bet, value))
                {
                    Description = value.Description;
                    Coins = value.Coins;
                    EndDate = DateTime.Parse(value.EndDate);
                    EndTime = EndDate.TimeOfDay;
                }
            }
        }

        private bool CanValideCommand() => !string.IsNullOrEmpty(Description) &&
                                            EndDate > _dateTimeProvider.Now();
    }
}
