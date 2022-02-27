using BetFriend.Domain.Bets.Dto;
using BetFriend.Domain.Bets.RetrieveBet;
using BetFriend.Domain.Bets.Usecases.AnswerBet;
using BetFriend.Domain.Users;
using GalaSoft.MvvmLight;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BetFriend.MobileApp.Views.DetailBet
{
    public class DetailBetViewModel : ViewModelBase
    {
        private BetOutput _bet;
        private readonly IRetrieveBetQueryHandler _retrieveBetQueryHandler;
        private readonly IAnswerBetCommandHandler _answerBetCommandHandler;
        private readonly IAuthenticationService _authenticationService;
        private Command _joinBetCommand;


        public DetailBetViewModel(IRetrieveBetQueryHandler retrieveBetQueryHandler,
                                  IAnswerBetCommandHandler answerBetCommandHandler,
                                  IAuthenticationService authenticationService)
        {
            _retrieveBetQueryHandler = retrieveBetQueryHandler;
            _answerBetCommandHandler = answerBetCommandHandler;
            _authenticationService = authenticationService;
        }

        public BetOutput Bet
        {
            get => _bet;
            set
            {
                if (Set(() => Bet, ref _bet, value))
                    RaisePropertyChanged(nameof(Bet));
            }
        }

        public Command JoinBetCommand
        {
            get => _joinBetCommand ?? (_joinBetCommand = new Command(async () =>
            {
                var command = new AnswerBetCommand(_bet.Id.ToString(), true);
                await _answerBetCommandHandler.Handle(command);
            }, () => _bet.Creator.Id != Guid.Parse(_authenticationService.UserId)));
        }

        internal async Task LoadBet(string value)
        {
            Bet = await _retrieveBetQueryHandler.Handle(new RetrieveBetQuery(Guid.Parse(value)));
        }


    }
}
