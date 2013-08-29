using Android.OS;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.Fragging.Fragments;
using Cirrious.MvvmCross.Views;

namespace RegionEx.Droid.Views
{
    [Region(Resource.Id.MainRegion)]
    public class Main1View : MvxFragment, IMvxView
    {
        public override Android.Views.View OnCreateView(Android.Views.LayoutInflater inflater, Android.Views.ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);

            // Set the layout
            return this.BindingInflate(Resource.Layout.main1, null);
        }

    }
}