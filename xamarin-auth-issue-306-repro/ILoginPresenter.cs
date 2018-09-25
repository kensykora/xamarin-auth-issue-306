using System;

using Xamarin.Auth;

namespace xamarin_auth_issue_306_repro
{
    public interface ILoginPresenterService
    {
        void ShowUserLogin(Authenticator authenticator);
    }
}
