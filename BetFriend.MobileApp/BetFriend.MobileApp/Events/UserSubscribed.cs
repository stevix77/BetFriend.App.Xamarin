using System;

namespace BetFriend.MobileApp.Events
{
    internal class UserSubscribed
    {
        public UserSubscribed(Guid userId, bool hasSubscribed)
        {
            UserId = userId;
            HasSubscribed = hasSubscribed;
        }

        public Guid UserId { get; }
        public bool HasSubscribed { get; }
    }
}
