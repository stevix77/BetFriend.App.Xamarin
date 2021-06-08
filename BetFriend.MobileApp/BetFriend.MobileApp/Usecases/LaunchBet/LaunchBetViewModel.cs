using BetFriend.Domain.Bets.LaunchBet;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using Xamarin.Forms;

namespace BetFriend.MobileApp.Usecases.LaunchBet
{
    public class LaunchBetViewModel : ViewModelBase
    {
        private readonly IMessenger _messenger;

        public LaunchBetViewModel(IMessenger messenger) : base(messenger)
        {
            _messenger = messenger;
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (Set(nameof(Description), ref _description, value))
                    Description = value;
            }
        }

        private int _coins;
        public int Coins
        {
            get => _coins;
            set
            {
                if (Set(nameof(Coins), ref _coins, value))
                    Coins = value;
            }
        }

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

        private Command _validateCommand;
        public Command ValidateCommand
        {
            get => _validateCommand ?? (_validateCommand = new Command(async () =>
            {
                var handler = new LaunchBetCommandHandler(null);
                await handler.Handle(new LaunchBetCommand(Guid.NewGuid(), _description, _endDate, _coins));
                // redirection
            }, () => CheckValideCommand()));
        }

        private bool CheckValideCommand()
        {
            return !string.IsNullOrEmpty(_description);
        }
    }
}
