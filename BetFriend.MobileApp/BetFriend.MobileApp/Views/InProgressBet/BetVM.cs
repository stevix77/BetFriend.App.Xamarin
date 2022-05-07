using System;
using Xamarin.Forms;

namespace BetFriend.MobileApp.Views.InProgressBet
{
    public class BetVM
    {
        public Guid CreatorId { get; internal set; }
        public string CreatorUsername { get; internal set; }
        public string Coins { get; internal set; }
        public string EndDate { get; internal set; }
        public string Description { get; internal set; }
        public string Participants { get; internal set; }
        public Guid Id { get; internal set; }

        public Color Color { get => Color.FromUint((uint)new Random(int.MaxValue).Next(0, 255)); }
    }
}