using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

using Xamarin.Auth;
using Xamarin.Forms;

using System.ComponentModel;

namespace xamarin_auth_issue_306_repro
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        const string CLIENT_ID = "273713162849-2s978u2hkp61nso94hbki837fpqotqri.apps.googleusercontent.com";
        const string REDIRECT_URL = "com.googleusercontent.apps.273713162849-2s978u2hkp61nso94hbki837fpqotqri:/oauth2redirect";
        const string AUTH_URL = "https://accounts.google.com/o/oauth2/v2/auth";
        const string TOKEN_URL = "https://www.googleapis.com/oauth2/v4/token";

        public ICommand LoginCommand { get; private set; }

        private string _loggedInStatus = "Not Logged In";

        public event PropertyChangedEventHandler PropertyChanged;

        public string LoggedInStatus
        { 
            get => _loggedInStatus;
            set {
                _loggedInStatus = value;
                var handler = PropertyChanged;
                handler?.Invoke(this, new PropertyChangedEventArgs(nameof(LoggedInStatus)));
            } 
        }

        public LoginViewModel()
        {
            LoginCommand = new Command(LoginCommandHandler);
        }

        private void LoginCommandHandler()
        {
            var loginPresenter = DependencyService.Get<ILoginPresenterService>();
            App.Authenticator = new OAuth2Authenticator(
                CLIENT_ID,
                clientSecret: null,
                scope: "openid",
                authorizeUrl: new Uri(AUTH_URL),
                accessTokenUrl: new Uri(TOKEN_URL),
                redirectUrl: new Uri(REDIRECT_URL),
                isUsingNativeUI: true);

            App.Authenticator.Completed += OnAuthCompleted;
            App.Authenticator.Error += OnAuthError;

            loginPresenter.ShowUserLogin(App.Authenticator);
        }

        void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            App.Authenticator.Completed -= OnAuthCompleted;
            App.Authenticator.Error -= OnAuthError;

            LoggedInStatus = e.IsAuthenticated ? "Logged In" : "Not Logged In";

            Debug.WriteLine("Authentication Completed: " + e.Account?.Username);
        }

        void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            LoggedInStatus = "Login Error: " + e.Message;

            Debug.WriteLine("Authentication Error: " + e.Message);
            if (e.Exception != null)
            {
                Debug.WriteLine(e.Exception.GetType() + ": " + e.Exception.Message);
                Debug.WriteLine(e.Exception.StackTrace);
            }
        }
    }
}
