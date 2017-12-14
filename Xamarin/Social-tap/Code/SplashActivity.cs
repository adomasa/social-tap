using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Support.V7.App;
using Socialtap.Code.Controller;

namespace Socialtap.Code
{
    [Activity(Theme = "@style/SocialTapTheme.Splash", MainLauncher = true,
              NoHistory = true, Label = "SocialTap",
              ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : AppCompatActivity
    {
        static readonly new string Tag = typeof(SplashActivity).Name;

        /// Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            new Task(LoadResources).Start();
        }

        /// Background work behind the splash screen
        async void LoadResources()
        {
            // Load resources here
            await Task.Delay(500);

            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}
