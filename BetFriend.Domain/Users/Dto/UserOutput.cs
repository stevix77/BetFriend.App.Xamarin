using System;
using System.Collections.Generic;

namespace BetFriend.Domain.Users.Dto
{
    public class UserOutput
    {
        public UserOutput()
        {
                
        }

        public UserOutput(User user)
        {
            if (user == null)
                return;

            Id = user.Id;
            Username = user.Username;
            Subscriptions = user.Subscriptions;
            Coins = user.Coins;
        }

        public Guid Id { get; set; }
        public string Username { get; set; }
        public IEnumerable<Guid> Subscriptions { get; set; } = new List<Guid>();
        public decimal Coins { get; set; }
    }
}
