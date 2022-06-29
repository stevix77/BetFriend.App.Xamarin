namespace BetFriend.Domain.Bets.Usecases.AnswerBet
{
    using System;
    using System.Threading.Tasks;


    public class AnswerBetCommandHandler : IAnswerBetCommandHandler
    {
        private readonly IBetRepository _betRepository;

        public AnswerBetCommandHandler(IBetRepository betRepository)
        {
            _betRepository = betRepository;
        }

        public async Task Handle(AnswerBetCommand command)
        {
            await _betRepository.AnswerBetAsync(command.Bet.BetId, command.Answer);
        }
    }
}
