using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Content.PM;

using Xamarin.Forms;

namespace xamarin_auth_issue_306_repro.Droid
{
    [Activity(Label = "interceptor", NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
    [
        IntentFilter
        (
            actions: new[] { Intent.ActionView },
            Categories = new[]
                    {
                        Intent.CategoryDefault,
                        Intent.CategoryBrowsable
                    },
            DataSchemes = new[]
                    {
                        "com.googleusercontent.apps.273713162849-2s978u2hkp61nso94hbki837fpqotqri"
                    },
            DataPaths = new []
                    {
                        "/oauth2redirect"
                    },
            AutoVerify = true
        )
    ]
    public class BossSchemeInterceptorActivity : Activity
     {
        
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            System.Diagnostics.Debug.WriteLine($">>>uri_android = {Intent.Data.ToString()}");

            App.Authenticator.OnPageLoading(new Uri(Intent.Data.ToString()));

            var intent = new Intent(this, typeof(MainActivity));
            intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
            StartActivity(intent);

            this.Finish();

            return;
        }

    }
}