using BetFriend.Domain.Abstractions;
using BetFriend.Domain.Bets.Usecases.UpdateBet;
using BetFriend.MobileApp.Models;
using BetFriend.MobileApp.Navigation;
using GalaSoft.MvvmLight;
using System;
using Xamarin.Forms;

namespace BetFriend.MobileApp.Views.EditBet
{
    internal class EditBetViewModel : ViewModelBase
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUpdateBetCommandHandler _updateBetCommandHandler;
        private readonly INavigationService _navigationService;

        public EditBetViewModel(IDateTimeProvider dateTimeProvider,
                                IUpdateBetCommandHandler updateBetCommandHandler, 
                                INavigationService navigationService)
        {
            _dateTimeProvider = dateTimeProvider;
            _updateBetCommandHandler = updateBetCommandHandler;
            _navigationService = navigationService;
        }
        public string Id { get; internal set; }
        private string _description;
        public string Description
        {
            get => _description;
            internal set
            {
                if (Set(() => Description, ref _description, value))
                {
                    RaisePropertyChanged(nameof(Description));
                    ValidateCommand.ChangeCanExecute();
                }
            }
        }
        public int Coins { get; internal set; }
        public DateTime EndDate { get; internal set; }
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
                                                                           EndDate.Add(_endTime),
                                                                           Coins));
                await _navigationService.GoBack();
            }, () => CanValideCommand());
        }

        private BetViewModel _bet;
        public BetViewModel Bet 
        {
            get => _bet; 
            internal set
            {
                _bet = value;
                Description = value.Description;
                Coins = value.Coins;
                EndDate = DateTime.Parse(value.EndDate);
                EndTime = EndDate.TimeOfDay;
            }
        }

        private bool CanValideCommand() => !string.IsNullOrEmpty(Description) &&
                                            EndDate > _dateTimeProvider.Now();
    }
}
