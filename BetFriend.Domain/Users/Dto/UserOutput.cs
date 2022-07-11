using System;
using System.Collections.Generic;

namespace BetFriend.Domain.Users.Dto
{
    public class UserOutput
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public IEnumerable<Guid> Subscriptions { get; set; } = new List<Guid>();
        public int Coins { get; set; }
    }
}
