﻿using BetFriend.Domain.Abstractions;
using BetFriend.Domain.Bets;
using BetFriend.Domain.Bets.Dto;
using BetFriend.Domain.Bets.GetBetsInProgress;
using BetFriend.Domain.Bets.LaunchBet;
using BetFriend.Domain.Bets.RetrieveBet;
using BetFriend.Domain.Users;
using BetFriend.Domain.Users.Usecases;
using BetFriend.Domain.Users.Usecases.Register;
using BetFriend.Domain.Users.Usecases.SignIn;
using BetFriend.Infrastructure;
using BetFriend.Infrastructure.Abstractions;
using BetFriend.Infrastructure.Gateways;
using BetFriend.Infrastructure.Hash;
using BetFriend.Infrastructure.Repositories.Http;
using BetFriend.Infrastructure.Repositories.InMemory;
using BetFriend.Infrastructure.Storage;
using BetFriend.MobileApp.Navigation;
using BetFriend.MobileApp.Views.DetailBet;
using BetFriend.MobileApp.Views.InProgressBet;
using BetFriend.MobileApp.Views.LaunchBet;
using BetFriend.MobileApp.Views.Register;
using BetFriend.MobileApp.Views.SignIn;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using System;
using System.Collections.Generic;

namespace BetFriend.MobileApp
{
    public static class ViewModelLocator
    {
        private static IServiceProvider _serviceProvider;

        public static T Resolve<T>()
        {
            return _serviceProvider.GetService<T>();
        }

        public static void RegisterInMemoryDependencies()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IBetRepository>(x => new InMemoryBetRepository(new List<BetOutput>()
            {
                new BetOutput
                {
                    Creator = new MemberOutput
                    {
                        Id = Guid.NewGuid(),
                        Username = "titi"
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
                        Id = Guid.NewGuid(),
                        Username = "tutu"
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
                        },
                        new MemberOutput
                        {
                            Id = Guid.NewGuid(),
                            Username = "username2"
                        }
                    }
                }
            }));
            serviceCollection.AddScoped<IUserRepository>(x => new InMemoryUserRepository("token"));
            serviceCollection.AddScoped<IMessenger, Messenger>();
            serviceCollection.AddScoped<INavigationService, ShellNavigationService>();
            serviceCollection.AddScoped<IRetrieveBetQueryHandler, RetrieveBetQueryHandler>();
            serviceCollection.AddScoped<ILaunchBetCommandHandler, LaunchBetCommandHandler>();
            serviceCollection.AddScoped<IGetBetsInProgressQueryHandler, GetBetsInProgressQueryHandler>();
            serviceCollection.AddScoped<IRegisterCommandHandler, RegisterCommandHandler>();
            serviceCollection.AddScoped<ISignInCommandHandler, SignInCommandHandler>();
            serviceCollection.AddScoped<RegisterPresenter>();
            serviceCollection.AddScoped<SignInPresenter>();
            serviceCollection.AddScoped<IRegisterPresenter>(x => x.GetRequiredService<RegisterPresenter>());
            serviceCollection.AddScoped<ISignInPresenter>(x => x.GetRequiredService<SignInPresenter>());
            serviceCollection.AddScoped<IHashPassword, FakeHashPassword>();
            serviceCollection.AddScoped<IAuthenticationService, AuthenticationService>();
            serviceCollection.AddScoped<IAuthenticationGateway>(x => new InMemoryAuthenticationGateway(new Authentication("username", "passwordpassword", "token")));
            serviceCollection.AddSingleton<LaunchBetViewModel>();
            serviceCollection.AddSingleton<InProgressBetsViewModel>();
            serviceCollection.AddSingleton<DetailBetViewModel>();
            serviceCollection.AddSingleton<RegisterViewModel>();
            serviceCollection.AddSingleton<SignInViewModel>();
            serviceCollection.AddSingleton<IDataStorage, EssentialsSecureStorage>();
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public static void RegisterDependencies()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IBetRepository>();
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<IMessenger, Messenger>();
            serviceCollection.AddScoped<INavigationService, ShellNavigationService>();
            serviceCollection.AddScoped<IRetrieveBetQueryHandler, RetrieveBetQueryHandler>();
            serviceCollection.AddScoped<ILaunchBetCommandHandler, LaunchBetCommandHandler>();
            serviceCollection.AddScoped<IGetBetsInProgressQueryHandler, GetBetsInProgressQueryHandler>();
            serviceCollection.AddScoped<IRegisterCommandHandler, RegisterCommandHandler>();
            serviceCollection.AddScoped<ISignInCommandHandler, SignInCommandHandler>();
            serviceCollection.AddScoped<RegisterPresenter>();
            serviceCollection.AddScoped<SignInPresenter>();
            serviceCollection.AddScoped<IRegisterPresenter>(x => x.GetRequiredService<RegisterPresenter>());
            serviceCollection.AddScoped<ISignInPresenter>(x => x.GetRequiredService<SignInPresenter>());
            serviceCollection.AddScoped<IHashPassword, Sha256HashPassword>();
            serviceCollection.AddScoped<IHttpService, HttpService>();
            serviceCollection.AddScoped<IAuthenticationService, AuthenticationService>();
            serviceCollection.AddScoped<IAuthenticationGateway, HttpAuthenticationGateway>();
            serviceCollection.AddScoped<IRestClient, RestClient>();
            serviceCollection.AddSingleton<LaunchBetViewModel>();
            serviceCollection.AddSingleton<InProgressBetsViewModel>();
            serviceCollection.AddSingleton<DetailBetViewModel>();
            serviceCollection.AddSingleton<RegisterViewModel>();
            serviceCollection.AddSingleton<SignInViewModel>();
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
