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
        public string Description { get; }
        public DateTime EndDate { get; }
        public int Coins { get; }

        internal static Bet Create(Guid betId, string description, DateTime endDate, int coins, IEnumerable<Answer> answers = null)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("Description is not valid");

            return new Bet(betId, description, endDate, coins, answers);
        }

        internal void AddAnswer(Answer answer)
        {
            if (_answers.ContainsKey(answer.MemberId))
                throw new BetAlreadyAnsweredException();

            _answers.Add(answer.MemberId, answer.GetAnswer());
        }
    }
}

