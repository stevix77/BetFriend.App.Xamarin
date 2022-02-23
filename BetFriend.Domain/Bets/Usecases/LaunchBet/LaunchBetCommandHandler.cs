namespace BetFriend.Domain.Bets.LaunchBet
{
    using BetFriend.Domain.Abstractions;
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
            var bet = Bet.Create(command, _dateTimeProvider);
            await _betRepository.SaveAsync(bet);
        }
    }
}
