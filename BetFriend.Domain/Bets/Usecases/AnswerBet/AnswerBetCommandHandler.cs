namespace BetFriend.Domain.Bets.Usecases.AnswerBet
{
    using BetFriend.Domain.Users;
    using System;
    using System.Threading.Tasks;


    public class AnswerBetCommandHandler : IAnswerBetCommandHandler
    {
        private readonly IBetRepository _betRepository;
        private readonly IAuthenticationService _authenticationService;

        public AnswerBetCommandHandler(IBetRepository betRepository, IAuthenticationService authenticationService)
        {
            _betRepository = betRepository;
            _authenticationService = authenticationService;
        }

        public async Task Handle(AnswerBetCommand command)
        {
            var bet = command.Bet.ToBet();
            bet.AddAnswer(new Answer(command.Answer, Guid.Parse(_authenticationService.UserId)));
            await _betRepository.AnswerBetAsync(bet);
        }
    }
}
