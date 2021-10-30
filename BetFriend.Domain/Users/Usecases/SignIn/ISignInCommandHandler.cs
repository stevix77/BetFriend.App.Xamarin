using System.Threading.Tasks;

namespace BetFriend.Domain.Users.Usecases.SignIn
{
    public interface ISignInCommandHandler
    {
        Task Handle(SignInCommand command);
    }
}