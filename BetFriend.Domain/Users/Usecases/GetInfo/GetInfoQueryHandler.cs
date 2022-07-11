using System.Threading.Tasks;

namespace BetFriend.Domain.Users.Usecases.GetInfo
{
    public class GetInfoQueryHandler : IGetInfoQueryHandler
    {
        private readonly IUserRepository _userRepository;

        public GetInfoQueryHandler(IUserRepository repository)
        {
            _userRepository = repository;
        }

        public async Task<InfoOutput> Handle(GetInfoQuery query)
        {
            var user = await _userRepository.GetUserAsync();
            return user == null ? InfoOutput.Empty : new InfoOutput
            {
                Coins = user.Coins,
                Subscriptions = user.Subscriptions
            };
        }
    }
}
