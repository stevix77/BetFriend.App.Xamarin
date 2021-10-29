using System;

namespace BetFriend.MobileApp.Views.InProgressBet
{
    public class BetVM
    {
        public Guid CreatorId { get; set; }
        public string CreatorUsername { get; set; }
        public int Coins { get; set; }
        public string EndDate { get; set; }
        public string Description { get; set; }
        public int Participants { get; set; }
        public Guid Id { get; internal set; }
    }
}