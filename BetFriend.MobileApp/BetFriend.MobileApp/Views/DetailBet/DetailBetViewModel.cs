using BetFriend.Domain.Bets.Dto;
using BetFriend.Domain.Bets.RetrieveBet;
using GalaSoft.MvvmLight;
using System;
using System.Threading.Tasks;

namespace BetFriend.MobileApp.Views.DetailBet
{
    public class DetailBetViewModel : ViewModelBase
    {
        private BetOutput _bet;
        private readonly IRetrieveBetQueryHandler _handler;

        public BetOutput Bet
        {
            get => _bet;
            set
            {
                if (Set(() => Bet, ref _bet, value))
                    RaisePropertyChanged(nameof(Bet));
            }
        }

        public DetailBetViewModel(IRetrieveBetQueryHandler retrieveBetQueryHandler)
        {
            _handler = retrieveBetQueryHandler;
        }

        internal async Task LoadBet(string value)
        {
            Bet = await _handler.Handle(new RetrieveBetQuery(Guid.Parse(value)));
        }
    }
}
