namespace BetFriend.Domain.Bets.LaunchBet
{
    using System;
    using System.Threading.Tasks;

    public class LaunchBetCommandHandler : ILaunchBetCommandHandler
    {
        private readonly IBetRepository _betRepository;

        public LaunchBetCommandHandler(IBetRepository betRepository)
        {
            _betRepository = betRepository;
        }

        public async Task Handle(LaunchBetCommand command)
        {
            ValidateCommand(command);
            var bet = new Bet(command.BetId, command.Description, command.EndDate, command.Coins);
            await _betRepository.SaveAsync(bet);
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
