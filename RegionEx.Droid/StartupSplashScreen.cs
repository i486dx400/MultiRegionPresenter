using Android.App;
using Android.Content.PM;
using Cirrious.MvvmCross.Droid.Views;

namespace RegionEx.Droid
{
    /// <summary>
    /// Android splash screen.  
    /// 
    /// Base implementation starts Mvx initialization.
    /// </summary>
    [Activity(
		Label = "Region Layout Example"
		, MainLauncher = true
		, Icon = "@drawable/icon"
		, Theme = "@style/Theme.Splash"
		, NoHistory = true
		, ScreenOrientation = ScreenOrientation.Landscape)]
    public class StartupSplashScreen : MvxSplashScreenActivity
    {
        public StartupSplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }

        protected override void TriggerFirstNavigate()
        {
            // override the default implementation to start the MainActivity, the host for the IMultiRegionPresenter.
            // when the activity is created it will start the initial navigation of the MvxApp.

            StartActivity(typeof(MainActivity));
            
            // do not call the base implementation
            //base.TriggerFirstNavigate();
        }
    }
}