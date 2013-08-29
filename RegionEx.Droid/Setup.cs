using Android.Content;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Droid.Platform;
using Cirrious.MvvmCross.Droid.Views;
using Cirrious.MvvmCross.ViewModels;

namespace RegionEx.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            // Register the multi region presenter presenter
            var regionPresenter = new MultiRegionPresenter();
            Mvx.RegisterSingleton<IMultiRegionPresenter>(regionPresenter);
            return regionPresenter;
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }
		
        protected override IMvxTrace CreateDebugTrace()
        {
            return new MvxDebugTrace();
        }

        // Visibility plug-in not required

        //public override void LoadPlugins(IMvxPluginManager pluginManager)
        //{
        //    // Load the visibility converter so it can be called from the
        //    //   layout bindings
        //    pluginManager.EnsurePluginLoaded<PluginLoader>();
        //    pluginManager.EnsurePluginLoaded<Cirrious.MvvmCross.Plugins.Visibility.PluginLoader>();
        //    base.LoadPlugins(pluginManager);
        //}
    }
}