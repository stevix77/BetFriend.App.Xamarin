namespace BetFriend.Domain.Bets.Dto
{
    using System;
    using System.Collections.Generic;

    public class BetOutput
    {
        public MemberOutput Creator { get; set; }
        public int Coins { get; set; }
        public string Description { get; set; }
        public Guid Id { get; set; }
        public DateTime EndDate { get; set; }
        public IReadOnlyCollection<MemberOutput> Participants { get; set; }
    }
}
