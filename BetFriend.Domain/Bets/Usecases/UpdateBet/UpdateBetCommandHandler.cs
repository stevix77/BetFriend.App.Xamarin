using System.Threading.Tasks;

namespace BetFriend.Domain.Bets.Usecases.UpdateBet
{
    public class UpdateBetCommandHandler : IUpdateBetCommandHandler
    {
        private readonly IBetRepository _betRepository;

        public UpdateBetCommandHandler(IBetRepository betRepository)
        {
            _betRepository = betRepository;
        }

        public async Task Handle(UpdateBetCommand command)
        {
            var bet = command.Bet.ToBet();
            if (bet.HasMembers())
                return;

            bet.Update(command.Description,
                       command.EndDate,
                       command.Coins);
            await _betRepository.UpdateAsync(bet);
        }
    }
}
