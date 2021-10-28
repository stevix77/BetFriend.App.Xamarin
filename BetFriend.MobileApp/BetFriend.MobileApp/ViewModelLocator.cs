using BetFriend.Domain.Bets;
using BetFriend.Domain.Bets.Dto;
using BetFriend.Infrastructure.Repositories.InMemory;
using BetFriend.MobileApp.Views.InProgressBet;
using BetFriend.MobileApp.Views.LaunchBet;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Collections.Generic;
using BetFriend.MobileApp.Views.DetailBet;

namespace BetFriend.MobileApp
{
    public static class ViewModelLocator
    {
        private static IServiceProvider _serviceProvider;

        public static T Resolve<T>()
        {
            return _serviceProvider.GetService<T>();
        }

        public static void RegisterDependencies()
        {
            var queryBetRepository = new InMemoryQueryBetRepository(new List<BetOutput>()
            {
                new BetOutput
                {
                    Creator = new MemberOutput
                    {
                        Id = App.CurrentUser,
                        Username = "stevix"
                    },
                    Description = "Description bet 1",
                    Coins = 30,
                    EndDate = new DateTime(2022, 2, 2),
                    Id = Guid.NewGuid(),
                    Participants = new List<MemberOutput>
                    {
                        new MemberOutput
                        {
                            Id = Guid.NewGuid(),
                            Username = "username1"
                        }
                    }
                },
                new BetOutput
                {
                    Creator = new MemberOutput
                    {
                        Id = App.CurrentUser,
                        Username = "stevix"
                    },
                    Description = "Description bet 2",
                    Coins = 30,
                    EndDate = new DateTime(2022, 2, 2),
                    Id = Guid.NewGuid(),
                    Participants = new List<MemberOutput>
                    {
                        new MemberOutput
                        {
                            Id = Guid.NewGuid(),
                            Username = "username1"
                        }
                    }
                }
            });

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IQueryBetRepository>(x => queryBetRepository);
            serviceCollection.AddScoped<IBetRepository>(x => new InMemoryBetRepository(App.CurrentUser, queryBetRepository));
            serviceCollection.AddScoped<IMessenger, Messenger>();
            serviceCollection.AddSingleton<LaunchBetViewModel>();
            serviceCollection.AddSingleton<InProgressBetsViewModel>();
            serviceCollection.AddSingleton<DetailBetViewModel>();
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
