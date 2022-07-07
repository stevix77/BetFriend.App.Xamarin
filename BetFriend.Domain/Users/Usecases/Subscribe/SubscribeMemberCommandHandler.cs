using System.Threading.Tasks;

namespace BetFriend.Domain.Users.Usecases.Subscribe
{
    public class SubscribeMemberCommandHandler : ISubscribeMemberCommandHandler
    {
        private readonly IUserRepository _userRepository;

        public SubscribeMemberCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(SubscribeMemberCommand command)
        {
            if(!command.CurrentUser.Subscriptions.Contains(command.SubscriptionId))
            {
                await _userRepository.SubscribeAsync(command.SubscriptionId);
                command.CurrentUser.Subscriptions.Add(command.SubscriptionId);
                return;
            }

            await _userRepository.UnsubscribeAsync(command.SubscriptionId);
            command.CurrentUser.Subscriptions.Remove(command.SubscriptionId);
        }
    }
}
