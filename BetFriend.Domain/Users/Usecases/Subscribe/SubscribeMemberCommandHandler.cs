﻿using System.Threading.Tasks;

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
            if (!_authenticationService.User.Subscriptions.Contains(command.SubscriptionId))
            {
                await _userRepository.SubscribeAsync(command.SubscriptionId);
                _authenticationService.User.Subscriptions.Add(command.SubscriptionId);
                return;
            }

            await _userRepository.UnsubscribeAsync(command.SubscriptionId);
            _authenticationService.User.Subscriptions.Remove(command.SubscriptionId);
        }
    }
}
