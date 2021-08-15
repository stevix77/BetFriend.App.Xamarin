using System;

namespace BetFriend.MobileApp.Views.InProgressBet
{
    public class BetInProgressVM
    {
        public Guid CreatorId { get; set; }
        public string CreatorUsername { get; set; }
        public int Tokens { get; set; }
        public string EndDate { get; set; }
        public string Description { get; set; }
        public int Participants { get; set; }
    }
}