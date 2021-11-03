namespace BetFriend.Infrastructure.Storage
{
    using BetFriend.Domain.Abstractions;
    using Newtonsoft.Json;
    using System.Threading.Tasks;
    using Xamarin.Essentials;

    public class EssentialsSecureStorage : IDataStorage
    {
        public bool DeleteData(string key)
        {
            try
            {
                return SecureStorage.Remove(key);
            }
            catch
            {
                return false;
            }
        }

        public T GetData<T>(string key)
        {
            try
            {
                var task = Task.Run(async () => await SecureStorage.GetAsync(key));
                task.Wait();
                if (!string.IsNullOrEmpty(task.Result))
                    return JsonConvert.DeserializeObject<T>(task.Result);
                else
                    return default;
            }
            catch
            {
                return default;
            }
        }

        public void SaveData<T>(T obj, string key)
        {
            try
            {
                SecureStorage.SetAsync(key, JsonConvert.SerializeObject(obj));
            }
            catch
            {
            }
        }
    }
}
