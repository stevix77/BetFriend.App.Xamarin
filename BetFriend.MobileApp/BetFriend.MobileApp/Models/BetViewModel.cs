using BetFriend.Domain.Bets.Dto;
using System;
using System.Collections.Generic;
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
            Members = bet.Members.Any() ? 
                    new ObservableCollection<MemberViewModel>(bet.Members.Select(x => new MemberViewModel(x.Id, x.Username))) : 
                    new ObservableCollection<MemberViewModel>();
            Coins = bet.Coins;
            Description = bet.Description;
            EndDate = bet.EndDate.ToString("f");
            CreatorUsername = bet.Creator.Username;
        }

        public string Id { get; }
        public Guid CreatorId { get; }
        public ObservableCollection<MemberViewModel> Members { get; }
        public int Coins { get; }
        public string Description { get; }
        public string EndDate { get; }
        public string CreatorUsername { get; }

        internal BetOutput ToBetOutput()
        {
            var bet = new BetOutput
            {
                Coins = Coins,
                Description = Description,
                Id = Guid.Parse(Id),
                EndDate = DateTime.Parse(EndDate),
                Members = new List<MemberOutput>(Members.Select(x => new MemberOutput { Id = x.Id })),
                Creator = new MemberOutput { Id = CreatorId, Username = CreatorUsername }
            };
            return bet;
        }
    }
}