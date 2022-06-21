using BetFriend.Domain.Users.Usecases.Register;
using System.Collections.Generic;

namespace BetFriend.MobileApp.UnitTests.Implems
{
    internal class RegisterTestPresenter : IRegisterPresenter
    {
        public string Token { get; private set; }

        public void Present(string token)
        {
            Token = token;
        }
    }
}
