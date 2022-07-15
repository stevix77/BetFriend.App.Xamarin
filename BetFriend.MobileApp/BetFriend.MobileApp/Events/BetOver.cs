namespace BetFriend.MobileApp.Events
{
    internal class BetOver
    {
        public BetOver(string betId, bool isSuccess, string description)
        {
            BetId = betId;
            IsSuccess = isSuccess;
            Description = description;
        }

        public string BetId { get; }
        public bool IsSuccess { get; }
        public string Description { get; }
    }
}
