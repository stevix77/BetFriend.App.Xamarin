namespace BetFriend.Domain.Bets.Exceptions
{
    using System;

    [Serializable]
    public class BetNotFoundException : Exception
    {
        public BetNotFoundException(Guid betId) : base($"Bet with id '{betId}' is not found")
        {
        }
    }
}
