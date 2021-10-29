﻿namespace BetFriend.Domain.Bets.LaunchBet
{
    using System;
    using System.Threading.Tasks;

    public class LaunchBetCommandHandler
    {
        private readonly IBetRepository _betRepository;

        public LaunchBetCommandHandler(IBetRepository betRepository)
        {
            _betRepository = betRepository;
        }

        public async Task Handle(LaunchBetCommand command)
        {
            ValidateCommand(command);

            await _betRepository.SaveAsync(command);
        }

        private static void ValidateCommand(LaunchBetCommand command)
        {
            if (command.EndDate <= DateTime.UtcNow)
                throw new ArgumentException("End date is not valid");

            if (string.IsNullOrEmpty(command.Description))
                throw new ArgumentException("Description is not valid");
        }
    }
}
