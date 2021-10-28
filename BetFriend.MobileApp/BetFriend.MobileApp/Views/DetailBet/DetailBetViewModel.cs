using BetFriend.Domain.Bets;
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
        public BetOutput Bet
        {
            get => _bet;
            set
            {
                if (Set(() => Bet, ref _bet, value))
                    RaisePropertyChanged(nameof(Bet));
            }
        }
        public DetailBetViewModel()
        {
        }

        internal async Task LoadBet(string value)
        {
            var handler = new RetrieveBetQueryHandler(ViewModelLocator.Resolve<IQueryBetRepository>());
            Bet = await handler.Handle(new RetrieveBetQuery(Guid.Parse(value)));
        }
    }
}
