using BetFriend.Domain.Bets;
using BetFriend.Domain.Bets.LaunchBet;
using BetFriend.MobileApp.Views.Home;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using Xamarin.Forms;

namespace BetFriend.MobileApp.Views.LaunchBet
{
    public class LaunchBetViewModel : ViewModelBase
    {

        private Command _validateCommand;
        private readonly ILaunchBetCommandHandler _launchBetCommandHandler;

        public LaunchBetViewModel(IMessenger messenger,
                                  ILaunchBetCommandHandler launchBetCommandHandler) : base(messenger)
        {
            _launchBetCommandHandler = launchBetCommandHandler;
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

        public DateTime MinimumDate { get; } = DateTime.Now;

        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (Set(nameof(EndDate), ref _endDate, value))
                    EndDate = value;
            }
        }

        private TimeSpan _endTime = DateTime.Now.TimeOfDay;
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

        public Command ValidateCommand
        {
            get => _validateCommand ?? (_validateCommand = new Command(async () =>
            {
                try
                {
                    await _launchBetCommandHandler.Handle(new LaunchBetCommand(Guid.NewGuid(), _description, _endDate, _coins));
                    await Shell.Current.GoToAsync($"//{nameof(HomeView)}");
                }
                catch (Exception)
                {

                }
            }, () => CheckValideCommand()));
        }

        private bool CheckValideCommand()
        {
            return !string.IsNullOrEmpty(_description);
        }
    }
}
