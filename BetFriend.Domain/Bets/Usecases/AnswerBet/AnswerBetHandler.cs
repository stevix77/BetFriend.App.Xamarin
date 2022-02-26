namespace BetFriend.Domain.Bets.Usecases.AnswerBet
{
    using BetFriend.Domain.Users;
    using System;
    using System.Threading.Tasks;


    public class AnswerBetHandler
    {
        private readonly IBetRepository _betRepository;

        public AnswerBetHandler(IBetRepository betRepository)
        {
            _betRepository = betRepository;
        }

        public async Task Handle(AnswerBetCommand command)
        {
            await _betRepository.AnswerBetAsync(Guid.Parse(command.BetId), command.Answer);
        }
    }
}
