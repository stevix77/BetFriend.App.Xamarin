namespace BetFriend.MobileApp.Navigation
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class ShellNavigationService : INavigationService
    {
        public async Task GoBack()
        {
            await Shell.Current.GoToAsync("..", true);
        }

        public async Task NavigateToAsync(string pageName, Dictionary<string, object> data)
        {
            var param = GenerateQueryParams(data);
            await Shell.Current.GoToAsync($"{pageName}{param}");
        }

        private static string GenerateQueryParams(Dictionary<string, object> data)
        {
            if (data is null || !data.Any())
                return string.Empty;

            var builder = new StringBuilder("?");
            foreach(var item in data)
                builder.Append($"&{item.Key}={item.Value}");
            return builder.ToString();
        }

        public Page Init(string pageName)
        {
            var appShell = new AppShell();
            appShell.GoToAsync($"//{pageName}").Wait();
            return appShell;
        }
    }
}
