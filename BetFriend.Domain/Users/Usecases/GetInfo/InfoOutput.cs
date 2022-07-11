using System;
using System.Collections.Generic;
using System.Text;

namespace BetFriend.Domain.Users.Usecases.GetInfo
{
    public class InfoOutput
    {
        public decimal Coins { get; internal set; }
        public IEnumerable<Guid> Subscriptions { get; internal set; }
    }
}
