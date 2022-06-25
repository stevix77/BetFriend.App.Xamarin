using BetFriend.Domain.Bets.Dto;
using System;

namespace BetFriend.MobileApp.Models
{
    public class MemberViewModel
    {
        public MemberViewModel(Guid id, string username)
        {
            Id = id;
            Username = username;
        }

        public Guid Id { get; }
        public string Username { get; }
    }
}
