﻿namespace BetFriend.MobileApp.Views.SignIn
{
    using BetFriend.Domain.Users;
    using BetFriend.Domain.Users.Usecases.SignIn;
    using System.Collections.Generic;
    using System.Linq;

    public class SignInPresenter : ISignInPresenter
    {
        private readonly ICollection<string> _errors;
        private readonly IAuthenticationService _authenticationService;

        public SignInPresenter(IAuthenticationService authenticationService)
        {
            _errors = new List<string>();
            _authenticationService = authenticationService;
        }

        public bool HasError { get => _errors.Any(); }

        public void Fail(string errorMessage)
        {
            _errors.Add(errorMessage);
        }

        public void Present(string token)
        {
            _authenticationService.SetToken(token);
            _errors.Clear();
        }

        internal string GetError()
        {
            return string.Join(" ", _errors);
        }
    }
}
