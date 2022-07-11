using BetFriend.Domain.Bets.Usecases.SearchMembers;
using BetFriend.Domain.Users;
using BetFriend.Domain.Users.Usecases.Subscribe;
using BetFriend.MobileApp.Events;
using BetFriend.MobileApp.Navigation;
using BetFriend.MobileApp.Views.DetailUser;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BetFriend.MobileApp.Views.Home
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly ISearchMembersQueryHandler _searchUsersQueryHandler;
        private readonly IAuthenticationService _authenticationService;
        private readonly ISubscribeMemberCommandHandler _subscribeMemberCommandHandler;
        private readonly INavigationService _navigationService;

        public HomeViewModel(ISearchMembersQueryHandler searchUsersQueryHandler,
                            IAuthenticationService authenticationService,
                            ISubscribeMemberCommandHandler subscribeMemberCommandHandler,
                            INavigationService navigationService)
        {
            _searchUsersQueryHandler = searchUsersQueryHandler;
            _authenticationService = authenticationService;
            _subscribeMemberCommandHandler = subscribeMemberCommandHandler;
            _navigationService = navigationService;
            MessengerInstance.Register<UserSubscribed>(this, (userSubscribed) => UpdateSearch(userSubscribed));
        }

        private void UpdateSearch(UserSubscribed userSubscribed)
        {
            var search = Members.FirstOrDefault(x => x.UserId == userSubscribed.UserId);
            if (search != null)
                search.HasSubscribed = userSubscribed.HasSubscribed;
        }

        private bool _isSearchMode = false;
        public bool IsSearchMode
        {
            get => _isSearchMode;
            set
            {
                if (Set(() => IsSearchMode, ref _isSearchMode, value))
                {
                    RaisePropertyChanged(nameof(IsSearchMode));
                    if (!value)
                        Members.Clear();
                }
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (Set(() => SearchText, ref _searchText, value))
                {
                    RaisePropertyChanged(nameof(SearchText));
                    IsSearchMode = !string.IsNullOrEmpty(value);
                    Search(value);
                }
            }
        }

        private void Search(string value)
        {
            if (!IsSearchMode)
                return;

            if (CanSearch(value))
                SearchCommand.Execute(value);
        }

        private static bool CanSearch(string value)
        {
            return value.Length >= 3;
        }

        private Command _subscribeCommand;
        public Command SubscribeCommand
        {
            get => _subscribeCommand ??= new Command<SearchVM>(async (search) =>
            {
                await _subscribeMemberCommandHandler.Handle(new SubscribeMemberCommand(search.UserId));
                search.HasSubscribed = _authenticationService.GetSubscriptions().Contains(search.UserId);
            });
        }

        private Command _searchItemCommand;

        public Command SearchItemCommand
        {
            get => _searchItemCommand ??= new Command(async (obj) =>
            {
                if (obj.GetType() == typeof(SelectedItemChangedEventArgs))
                {
                    await _navigationService.NavigateToAsync(nameof(DetailUserView),
                                                            new Dictionary<string, object>()
                                                            {
                                                                { "userid", (((SelectedItemChangedEventArgs)obj).SelectedItem as SearchVM).UserId },
                                                                { "hassubscribed", (((SelectedItemChangedEventArgs)obj).SelectedItem as SearchVM).HasSubscribed }
                                                            });
                }
                else if (obj.GetType() == typeof(ItemTappedEventArgs))
                {
                    await _navigationService.NavigateToAsync(nameof(DetailUserView),
                                                            new Dictionary<string, object>()
                                                            {
                                                                { "userid", (((ItemTappedEventArgs)obj).Item as SearchVM).UserId },
                                                                { "hassubscribed", (((ItemTappedEventArgs)obj).Item as SearchVM).HasSubscribed }
                                                            });
                }
            });
        }

        private Command _searchCommand;
        public Command SearchCommand
        {
            get => _searchCommand ??= new Command<string>(async (text) =>
            {
                if (CanSearch(text))
                    await SearchUsers(text);
            });
        }

        private async Task SearchUsers(string text)
        {
            var users = await _searchUsersQueryHandler.Handle(new SearchMembersQuery(text));
            foreach (var user in users)
            {
                Members.Add(new SearchVM(user.Id, user.Username, HasSubscribed(user.Id)));
            }
        }

        private bool HasSubscribed(Guid id)
        {
            return _authenticationService.GetSubscriptions().Contains(id);
        }

        private readonly ObservableCollection<SearchVM> _members = new ObservableCollection<SearchVM>();
        public ObservableCollection<SearchVM> Members
        {
            get => _members;
        }
    }
}
