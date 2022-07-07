﻿using BetFriend.MobileApp.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace BetFriend.MobileApp.Views.Home
{
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel()
        {

        }

        private bool _isSearchMode = false;
        public bool IsSearchMode
        {
            get => _isSearchMode;
            set
            {
                if(Set(() => IsSearchMode, ref _isSearchMode, value))
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
                if(Set(() => SearchText, ref _searchText, value))
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

        private Command _searchCommand;
        public Command SearchCommand
        {
            get => _searchCommand ??= new Command<string>(async (text) =>
            {
                if (CanSearch(text))
                    Members = new ObservableCollection<MemberViewModel>(_fakemembers);
            });
        }
        public ObservableCollection<MemberViewModel> Members 
        {
            get => _members; 
            set
            {
                if (Set(() => Members, ref _members, value))
                    RaisePropertyChanged(nameof(Members));
            }
        }

        private ObservableCollection<MemberViewModel> _members = new ObservableCollection<MemberViewModel>();
        private IEnumerable<MemberViewModel> _fakemembers = new List<MemberViewModel>
        {
            new MemberViewModel(Guid.NewGuid(), Guid.NewGuid().ToString()),
            new MemberViewModel(Guid.NewGuid(), Guid.NewGuid().ToString()),
            new MemberViewModel(Guid.NewGuid(), Guid.NewGuid().ToString()),
            new MemberViewModel(Guid.NewGuid(), Guid.NewGuid().ToString()),
            new MemberViewModel(Guid.NewGuid(), Guid.NewGuid().ToString()),
            new MemberViewModel(Guid.NewGuid(), Guid.NewGuid().ToString()),
            new MemberViewModel(Guid.NewGuid(), Guid.NewGuid().ToString()),
            new MemberViewModel(Guid.NewGuid(), Guid.NewGuid().ToString())
        };
    }
}
