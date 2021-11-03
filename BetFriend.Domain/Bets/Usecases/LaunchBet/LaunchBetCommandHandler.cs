namespace BetFriend.Domain.Bets.LaunchBet
{
    using BetFriend.Domain.Abstractions;
    using System;
    using System.Threading.Tasks;

    public class LaunchBetCommandHandler : ILaunchBetCommandHandler
    {
        private readonly IBetRepository _betRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public LaunchBetCommandHandler(IBetRepository betRepository, IDateTimeProvider dateTimeProvider)
        {
            _betRepository = betRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task Handle(LaunchBetCommand command)
        {
            ValidateCommand(command);
            var bet = new Bet(command.BetId, command.Description, command.EndDate, command.Coins);
            await _betRepository.SaveAsync(bet);
        }

        private void ValidateCommand(LaunchBetCommand command)
        {
            if (command.EndDate <= _dateTimeProvider.Now())
                throw new ArgumentException("End date is not valid");

            if (string.IsNullOrEmpty(command.Description))
                throw new ArgumentException("Description is not valid");
        }
    }
}
