using System.Threading.Tasks;

namespace BetFriend.Domain.Users.Usecases.GetInfo
{
    public class GetInfoQueryHandler
    {
        private readonly IUserRepository _userRepository;

        public GetInfoQueryHandler(IUserRepository repository)
        {
            _userRepository = repository;
        }

        public async Task<InfoOutput> Handle(GetInfoQuery query)
        {
            var user = await _userRepository.GetUserAsync();
            return new InfoOutput
            {
                Coins = user.Coins,
                Subscriptions = user.Subscriptions
            };
        }
    }
}
