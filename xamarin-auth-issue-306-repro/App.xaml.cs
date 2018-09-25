using System;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace xamarin_auth_issue_306_repro
{
    public partial class App : Application
    {
        public static OAuth2Authenticator Authenticator { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage
            {
                BindingContext = new LoginViewModel()
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
