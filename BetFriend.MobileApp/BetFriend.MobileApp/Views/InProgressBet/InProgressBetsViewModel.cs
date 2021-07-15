namespace BetFriend.MobileApp.Views.InProgressBet
{
    using BetFriend.Domain.Bets;
    using BetFriend.Domain.Bets.Dto;
    using BetFriend.Domain.Bets.GetBetsInProgress;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Messaging;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class InProgressBetsViewModel : ViewModelBase
    {
        private readonly GetBetsInProgressQueryHandler _handler;

        public InProgressBetsViewModel() 
        { 
        
        }
        public InProgressBetsViewModel(IMessenger messenger) : base(messenger)
        {
            _handler = new GetBetsInProgressQueryHandler(ViewModelLocator.Resolve<IQueryBetRepository>());
        }

        private ObservableCollection<BetInProgressVM> _bets;

        public ObservableCollection<BetInProgressVM> Bets
        {
            get => _bets ?? (_bets = new ObservableCollection<BetInProgressVM>());
            set
            {
                if (Set(() => Bets, ref _bets, value))
                {
                    RaisePropertyChanged(nameof(Bets));
                }
            }
        }

        public async Task LoadBets()
        {
            var result = await _handler.Handle(new GetBetsInProgressQuery(App.Me));
            if (result.Any())
                MapBets(result);
        }

        private void MapBets(IReadOnlyCollection<BetOutput> bets)
        {
            foreach (var item in bets)
            {
                var bet = new BetInProgressVM
                {
                    CreatorId = item.Creator.Id,
                    CreatorUsername = item.Creator.Username,
                    Tokens = item.Tokens,
                    EndDate = item.EndDate,
                    Description = item.Description,
                    Participants = item.Participants.Count
                };
                Bets.Add(bet);
            }
        }
    }
}
