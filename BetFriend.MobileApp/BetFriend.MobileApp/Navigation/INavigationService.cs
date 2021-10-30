namespace BetFriend.MobileApp.Navigation
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public interface INavigationService
    {
        Task NavigateToAsync(string pageName, Dictionary<string, object> data);
        Task GoBack();
        Page Init(string pageName);
    }
}
