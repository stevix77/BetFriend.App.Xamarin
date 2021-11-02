﻿using Autofac;
using BetFriend.Domain.Bets;
using BetFriend.Domain.Bets.Dto;
using BetFriend.Infrastructure.Repositories.InMemory;
using BetFriend.MobileApp.Views.InProgressBet;
using BetFriend.MobileApp.Views.LaunchBet;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;

namespace BetFriend.MobileApp
{
    public static class ViewModelLocator
    {
        private static IContainer _container;

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(new InMemoryBetRepository(App.Me)).As<IBetRepository>().SingleInstance();
            builder.RegisterInstance(new InMemoryQueryBetRepository(new List<BetOutput>()
            {
                new BetOutput
                {
                    Creator = new MemberOutput
                    {
                        Id = App.Me,
                        Username = "stevix"
                    },
                    Description = "desc1",
                    Tokens = 30,
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
            })).As<IQueryBetRepository>().SingleInstance();
            builder.RegisterType<Messenger>().As<IMessenger>().InstancePerLifetimeScope();

            builder.RegisterType<LaunchBetViewModel>();
            builder.RegisterType<InProgressBetsViewModel>().InstancePerLifetimeScope();

            _container = builder.Build();
        }
    }
}
