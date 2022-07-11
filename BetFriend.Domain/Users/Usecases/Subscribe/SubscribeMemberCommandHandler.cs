using System.Linq;
using System.Threading.Tasks;

namespace BetFriend.Domain.Users.Usecases.Subscribe
{
    public class SubscribeMemberCommandHandler : ISubscribeMemberCommandHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;

        public SubscribeMemberCommandHandler(IUserRepository userRepository, IAuthenticationService authenticationService)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
        }

        public async Task Handle(SubscribeMemberCommand command)
        {
            if (!_authenticationService.GetSubscriptions().Any(x => x == command.SubscriptionId))
            {
                await _userRepository.SubscribeAsync(command.SubscriptionId);
                _authenticationService.AddSubscription(command.SubscriptionId);
                return;
            }

            await _userRepository.UnsubscribeAsync(command.SubscriptionId);
            _authenticationService.RemoveSubscription(command.SubscriptionId);
        }
    }
}
