namespace BetFriend.MobileApp.Views.InProgressBet
{
    using BetFriend.Domain.Bets.Dto;
    using BetFriend.Domain.Bets.GetBetsInProgress;
    using BetFriend.MobileApp.Navigation;
    using BetFriend.MobileApp.Views.DetailBet;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Messaging;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class InProgressBetsViewModel : ViewModelBase
    {
        private readonly IGetBetsInProgressQueryHandler _handler;
        private readonly INavigationService _navigationService;

        public InProgressBetsViewModel(IMessenger messenger,
                                        IGetBetsInProgressQueryHandler getBetsInProgressQueryHandler,
                                        INavigationService navigationService) : base(messenger)
        {
            _handler = getBetsInProgressQueryHandler;
            _navigationService = navigationService;
        }

        private ObservableCollection<BetVM> _bets;

        public ObservableCollection<BetVM> Bets
        {
            get => _bets ?? (_bets = new ObservableCollection<BetVM>());
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
            get => _itemSelected ??= new Command(async (obj) =>
            {
                if (obj.GetType() == typeof(SelectedItemChangedEventArgs))
                {
                    await _navigationService.NavigateToAsync(nameof(DetailBetView),
                                                            new Dictionary<string, object>()
                                                            {
                                                                { "betid", (((SelectedItemChangedEventArgs)obj).SelectedItem as BetVM).Id }
                                                            });
                }
                else if (obj.GetType() == typeof(ItemTappedEventArgs))
                {
                    await _navigationService.NavigateToAsync(nameof(DetailBetView),
                                                            new Dictionary<string, object>()
                                                            {
                                                                { "betid", (((ItemTappedEventArgs)obj).Item as BetVM).Id }
                                                            });
                }
            });
        }

        public async Task LoadBets()
        {
            var result = await _handler.Handle();
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
                var bet = new BetVM
                {
                    CreatorId = item.Creator.Id,
                    CreatorUsername = item.Creator.Username,
                    Coins = $"{item.Coins} {Resources.Resource.LblTokens}",
                    EndDate = item.EndDate.ToShortDateString(),
                    Description = item.Description.Length > 50 ? $"{item.Description.Substring(0, 50)}..." : item.Description,
                    Participants = $"{item.Members.Count} {Resources.Resource.Participants}",
                    Id = item.Id,
                };
                Bets.Add(bet);
            }
        }
    }
}
