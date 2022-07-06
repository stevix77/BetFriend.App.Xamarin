using System.Threading.Tasks;

namespace BetFriend.Domain.Users.Usecases.Subscribe
{
    public interface ISubscribeMemberCommandHandler
    {
        Task Handle(SubscribeMemberCommand command);
    }
}
