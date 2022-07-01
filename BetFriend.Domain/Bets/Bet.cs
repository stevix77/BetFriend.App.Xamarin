using BetFriend.Domain.Abstractions;
using BetFriend.Domain.Bets.Exceptions;
using BetFriend.Domain.Bets.LaunchBet;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BetFriend.Domain.Bets
{
    public class Bet
    {
        private Bet(Guid betId,
                    string description,
                    DateTime endDate,
                    int coins,
                    IEnumerable<Answer> answers)
        {
            BetId = betId;
            Description = description;
            EndDate = endDate;
            Coins = coins;
            if (answers != null)
                foreach (var item in answers)
                    _answers.Add(item.MemberId, item.GetAnswer());
        }

        private Dictionary<Guid, bool> _answers = new Dictionary<Guid, bool>();

        public Guid BetId { get; }
        public string Description { get; private set; }
        public DateTime EndDate { get; private set; }
        public int Coins { get; private set; }

        internal static Bet Create(Guid betId, string description, DateTime endDate, int coins, IEnumerable<Answer> answers = null)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("Description is not valid");

            return new Bet(betId, description, endDate, coins, answers);
        }

        internal void AddAnswer(Answer answer)
        {
            if (_answers.ContainsKey(answer.MemberId) && HasSameAnswer(answer))
                throw new BetAlreadyAnsweredException();

            _answers[answer.MemberId] = answer.GetAnswer();
        }

        internal bool HasMembers() => _answers.Any();

        internal void Update(string description, DateTime? endDate, int? coins)
        {
            UpdateDescription(description);
            UpdateEndDate(endDate);
            UpdateCoins(coins);
        }

        private bool HasSameAnswer(Answer answer)
        {
            return _answers[answer.MemberId] == answer.GetAnswer();
        }

        private void UpdateDescription(string description)
        {
            if (description is not null)
                Description = description;
        }

        private void UpdateEndDate(DateTime? endDate)
        {
            if (endDate.HasValue)
                EndDate = endDate.Value;
        }

        private void UpdateCoins(int? coins)
        {
            if (coins.HasValue)
                Coins = coins.Value;
        }
    }
}

