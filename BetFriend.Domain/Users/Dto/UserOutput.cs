using System;
using System.Collections.Generic;

namespace BetFriend.Domain.Users.Dto
{
    public class UserOutput
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public ICollection<Guid> Subscriptions { get; } = new List<Guid>();
    }
}
