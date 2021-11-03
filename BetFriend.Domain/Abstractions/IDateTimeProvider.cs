using System;

namespace BetFriend.Domain.Abstractions
{
    public interface IDateTimeProvider
    {
        DateTime Now();
    }
}
