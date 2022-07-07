using System;

namespace BetFriend.Domain.Users.Usecases.Subscribe
{
    public class SubscribeMemberCommand
    {
        public Guid SubscriptionId { get; }

        public SubscribeMemberCommand(Guid subscriptionId)
        {
            SubscriptionId = subscriptionId;
        }
    }
}
