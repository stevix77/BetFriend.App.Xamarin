namespace BetFriend.MobileApp.UnitTests.Implems
{
    using BetFriend.Domain.Users.Usecases.SignIn;
    using System.Collections.Generic;


    internal class SignInTestPresenter : ISignInPresenter
    {
        private string _token;
        private ICollection<string> _errors;

        public SignInTestPresenter()
        {
            _errors = new List<string>();
        }

        public void Fail(string errorMessage)
        {
            _errors.Add(errorMessage);
        }

        public void Present(string token)
        {
            _token = token;
        }

        internal IEnumerable<string> GetErrors() => _errors;

        internal string GetToken()
        {
            return _token;
        }
    }
}
