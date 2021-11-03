namespace BetFriend.Infrastructure.DateTime
{
    using BetFriend.Domain.Abstractions;
    using System;


    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now() => DateTime.UtcNow;
    }
}
