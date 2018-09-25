using System;
using System.Diagnostics;
using System.Linq;

using Android.App;
using Android.Content;
using Android.Support.CustomTabs;

using Xamarin.Auth;
using Xamarin.Forms;
using xamarin_auth_issue_306_repro;

[assembly: Dependency(typeof(LoginPresenterService))]
namespace xamarin_auth_issue_306_repro
{
    public class LoginPresenterService : ILoginPresenterService
    {
        private const string ChromeBrowserPackageId = "com.android.chrome";

        private static Activity _activity;

        internal static void Initialize(Activity activity)
        {
            _activity = activity;

            CustomTabsConfiguration.CustomTabsClosingMessage = null;
            CustomTabsConfiguration.IsDefaultShareMenuItemUsed = false;
            CustomTabsConfiguration.PackageForCustomTabs = ChromeBrowserPackageId;
        }

        public void ShowUserLogin(Authenticator authenticator)
        {
            try
            {
                var p = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
                p.Login(authenticator);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Unexpected error starting activity: " + e.GetType() + ": " + e.Message);
                Debug.WriteLine(e.StackTrace);
                throw;
            }
        }
    }
}
