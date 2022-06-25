using BetFriend.Domain.Bets.Dto;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace BetFriend.MobileApp.Models
{

    public class BetViewModel
    {
        public BetViewModel(BetOutput bet)
        {
            Id = bet.Id.ToString();
            CreatorId = bet.Creator.Id;
            Members = bet.Participants.Any() ? 
                    new ObservableCollection<MemberViewModel>(bet.Participants.Select(x => new MemberViewModel(x.Id, x.Username))) : 
                    new ObservableCollection<MemberViewModel>();
            Coins = bet.Coins;
            Description = bet.Description;
            EndDate = bet.EndDate.ToLocalTime().ToLongDateString();
            CreatorUsername = bet.Creator.Username;
        }

        public string Id { get; }
        public Guid CreatorId { get; }
        public ObservableCollection<MemberViewModel> Members { get; }
        public int Coins { get; }
        public string Description { get; }
        public string EndDate { get; }
        public string CreatorUsername { get; }
    }
}