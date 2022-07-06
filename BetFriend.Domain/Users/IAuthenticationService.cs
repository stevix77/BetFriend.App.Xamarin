using BetFriend.Domain.Users.Dto;
using System;

namespace BetFriend.Domain.Users
{
    public interface IAuthenticationService
    {
        string Token { get; }
        string UserId { get; }
        string Username { get; }
        UserOutput User { get; }
        void SetToken(string token);
    }
}
