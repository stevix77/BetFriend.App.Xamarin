using BetFriend.Domain.Users.Dto;
using System;

namespace BetFriend.Domain.Users.Usecases.Subscribe
{
    public class SubscribeMemberCommand
    {
        public UserOutput CurrentUser { get; }
        public Guid SubscriptionId { get; }

        public SubscribeMemberCommand(UserOutput currentUser, Guid subscriptionId)
        {
            CurrentUser = currentUser;
            SubscriptionId = subscriptionId;
        }
    }
}
