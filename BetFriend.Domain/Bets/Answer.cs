using System;

namespace BetFriend.Domain.Bets
{
    internal struct Answer
    {
        private readonly bool _answer;
        private readonly Guid _memberId;

        internal Answer(bool answer, Guid memberId)
        {
            _answer = answer;
            _memberId = memberId;
        }

        internal Guid MemberId => _memberId;
        internal bool GetAnswer() => _answer;
    }
}