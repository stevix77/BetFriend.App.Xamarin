namespace BetFriend.Domain.Users.Usecases.SignIn
{
    public interface ISignInPresenter
    {
        void Present(string token);
        void Fail(string errorMessage);
    }
}