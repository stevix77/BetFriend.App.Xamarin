namespace BetFriend.Infrastructure.Repositories.InMemory
{
    using BetFriend.Domain.Bets;
    using BetFriend.Domain.Bets.Dto;
    using BetFriend.Domain.Users;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;


    public class InMemoryBetRepository : IBetRepository
    {
        private readonly List<Bet> _bets;
        private readonly List<BetOutput> _betOutputs;
        private readonly IAuthenticationService _authenticationService;

        public InMemoryBetRepository(List<BetOutput> betOutputs = null, IAuthenticationService authenticationService = null)
        {
            _bets = new List<Bet>();
            _betOutputs = betOutputs ?? new List<BetOutput>();
            _authenticationService = authenticationService;
        }

        public Task SaveAsync(Bet bet)
        {
            _bets.Add(bet);
            _betOutputs.Add(new BetOutput
            {
                Coins = bet.Coins,
                Description = bet.Description,
                EndDate = bet.EndDate,
                Id = bet.BetId,
                Participants = new List<MemberOutput>(),
                Creator = new MemberOutput { Id = Guid.Parse(_authenticationService.UserId), Username = _authenticationService.Username }
            });
            return Task.CompletedTask;
        }

        public Task<BetOutput> GetBetAsync(Guid betId)
        {
            var betOutput = _betOutputs.FirstOrDefault(x => x.Id == betId);
            return Task.FromResult(betOutput);
        }

        public async Task<IReadOnlyCollection<BetOutput>> GetBetsAsync()
        {
            return await Task.FromResult(_betOutputs);
        }

        public IReadOnlyCollection<Bet> GetBets()
        {
            return _bets;
        }

        public Task AnswerBetAsync(Guid betId, bool answer)
        {
            var bet = _betOutputs.First(x => x.Id == betId);
            if (!answer && !CanJoin(bet))
                bet.Participants = new List<MemberOutput>(bet.Participants.Where(x => x.Id != Guid.Parse(_authenticationService.UserId)));
            else
            {
                if(CanJoin(bet))
                    bet.Participants = new List<MemberOutput>(bet.Participants) { new MemberOutput { Id = Guid.Parse(_authenticationService.UserId), Username = _authenticationService.Username } };
            }
            return Task.CompletedTask;
        }

        private bool CanJoin(BetOutput bet)
        {
            return !bet.Participants.Any(x => x.Id == Guid.Parse(_authenticationService.UserId));
        }
    }
}
