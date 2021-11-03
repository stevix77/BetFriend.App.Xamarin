namespace BetFriend.Domain.Abstractions
{
    public interface IDataStorage
    {
        T GetData<T>(string key);
        void SaveData<T>(T obj, string key);
        bool DeleteData(string key);
    }
}
