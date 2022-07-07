using BetFriend.Domain.Bets.Usecases.SearchUsers;
using BetFriend.Domain.Users;
using BetFriend.Domain.Users.Usecases.Subscribe;
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
        private readonly ISearchUsersQueryHandler _searchUsersQueryHandler;
        private readonly IAuthenticationService _authenticationService;
        private readonly ISubscribeMemberCommandHandler _subscribeMemberCommandHandler;

        public HomeViewModel(ISearchUsersQueryHandler searchUsersQueryHandler,
                            IAuthenticationService authenticationService,
                            ISubscribeMemberCommandHandler subscribeMemberCommandHandler)
        {
            _searchUsersQueryHandler = searchUsersQueryHandler;
            _authenticationService = authenticationService;
            _subscribeMemberCommandHandler = subscribeMemberCommandHandler;
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
                search.HasSubscribed = _authenticationService.User.Subscriptions.Contains(search.UserId);
            });
        }

        private Command _searchItemCommand;

        public Command SearchItemCommand
        {
            get => _searchItemCommand ??= new Command(async () =>
            {

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
            var users = await _searchUsersQueryHandler.Handle(new SearchUsersQuery(text));
            foreach (var user in users)
            {
                Members.Add(new SearchVM(user.Id, user.Username, HasSubscribed(user.Id)));
            }
        }

        private bool HasSubscribed(Guid id)
        {
            return _authenticationService.User.Subscriptions.Contains(id);
        }

        private ObservableCollection<SearchVM> _members = new ObservableCollection<SearchVM>();
        public ObservableCollection<SearchVM> Members
        {
            get => _members;
        }
    }
}
