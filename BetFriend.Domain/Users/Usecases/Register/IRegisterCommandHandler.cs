namespace BetFriend.Domain.Users.Usecases.Register
{
    using System.Threading.Tasks;

    public interface IRegisterCommandHandler
    {
        Task Handle(RegisterCommand command);
    }
}
