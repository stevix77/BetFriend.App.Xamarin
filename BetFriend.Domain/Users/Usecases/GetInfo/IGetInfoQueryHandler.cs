using System.Threading.Tasks;

namespace BetFriend.Domain.Users.Usecases.GetInfo
{
    public interface IGetInfoQueryHandler
    {
        Task<InfoOutput> Handle(GetInfoQuery query);
    }
}