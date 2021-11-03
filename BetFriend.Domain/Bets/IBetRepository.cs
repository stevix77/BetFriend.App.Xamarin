﻿namespace BetFriend.Domain.Bets
{
    using BetFriend.Domain.Bets.Dto;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;


    public interface IBetRepository
    {
        Task SaveAsync(Bet bet);
        Task<IReadOnlyCollection<BetOutput>> GetBetsAsync();
        Task<BetOutput> GetBetAsync(Guid betId);
    }
}
