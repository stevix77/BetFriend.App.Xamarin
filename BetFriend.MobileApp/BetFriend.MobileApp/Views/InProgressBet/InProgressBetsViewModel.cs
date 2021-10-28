namespace BetFriend.MobileApp.Views.InProgressBet
{
    using BetFriend.Domain.Bets;
    using BetFriend.Domain.Bets.Dto;
    using BetFriend.Domain.Bets.GetBetsInProgress;
    using BetFriend.MobileApp.Views.DetailBet;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Messaging;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class InProgressBetsViewModel : ViewModelBase
    {
        private readonly GetBetsInProgressQueryHandler _handler;

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

        private Command _itemSelected;
        public Command ItemSelected
        {
            get => _itemSelected ?? (_itemSelected = new Command(async (obj) =>
            {
                if (obj.GetType() == typeof(SelectedItemChangedEventArgs))
                {
                    await Shell.Current.GoToAsync($"{nameof(DetailBetView)}?bet={((SelectedItemChangedEventArgs)obj).SelectedItem as BetInProgressVM}", true);
                }
                else if (obj.GetType() == typeof(ItemTappedEventArgs))
                {
                    await Shell.Current.GoToAsync($"{nameof(DetailBetView)}?bet={(((ItemTappedEventArgs)obj).Item as BetInProgressVM).Id}", true);
                }
            }));
        }

        public async Task LoadBets()
        {
            var result = await _handler.Handle(new GetBetsInProgressQuery(App.CurrentUser));
            if (result.Any())
            {
                ResetBets();
                MapBets(result);
            }
        }

        private void ResetBets()
        {
            Bets.Clear();
        }

        private void MapBets(IReadOnlyCollection<BetOutput> bets)
        {
            foreach (var item in bets)
            {
                var bet = new BetInProgressVM
                {
                    CreatorId = item.Creator.Id,
                    CreatorUsername = item.Creator.Username,
                    Tokens = item.Coins,
                    EndDate = item.EndDate.ToLongDateString(),
                    Description = item.Description.Length > 50 ? $"{item.Description.Substring(0, 50)}..." : item.Description,
                    Participants = item.Participants.Count,
                    Id = item.Id
                };
                Bets.Add(bet);
            }
        }
    }
}
