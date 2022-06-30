namespace BetFriend.Domain.Bets.Dto
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BetOutput
    {
        public MemberOutput Creator { get; set; }
        public int Coins { get; set; }
        public string Description { get; set; }
        public Guid Id { get; set; }
        public DateTime EndDate { get; set; }
        public IReadOnlyCollection<MemberOutput> Members { get; set; } = new List<MemberOutput>();

        internal Bet ToBet()
        {
            return Bet.Create(Id, Description, EndDate, Coins, Members.Select(x => new Answer(true, x.Id)));
        }
    }
}
