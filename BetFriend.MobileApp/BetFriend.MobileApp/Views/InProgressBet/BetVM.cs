using System;
using Xamarin.Forms;

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

        public Color Color { get => Color.FromUint((uint)new Random(int.MaxValue).Next(0, 255)); }
    }
}