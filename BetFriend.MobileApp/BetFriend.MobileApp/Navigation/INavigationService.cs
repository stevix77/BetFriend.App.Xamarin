namespace BetFriend.MobileApp.Navigation
{
    using System.Collections.Generic;
    using System.Threading.Tasks;


    public interface INavigationService
    {
        Task NavigateToAsync(string pageName, Dictionary<string, object> data);
        Task GoBack();
    }
}
