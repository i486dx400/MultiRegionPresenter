using Android.App;
using Android.Support.V4.App;
using Android.Views;
using Cirrious.MvvmCross.ViewModels;
using System;
using System.Linq;
using Cirrious.MvvmCross.Droid.Fragging.Fragments;
using Cirrious.CrossCore;
using Android.Widget;

namespace RegionEx.Droid
{
    /// <summary>
    /// Main activity for the application.  Collaborates with MultiRegionPresenter to create a composite UI made up of multiple MvxFragment views.
    /// </summary>
    [Activity(Label = "Main Activity")]
    public class MainActivity : FragmentActivity, IMultiRegionHost
    {
        // Timeout for back button exit
        private const int lengthToWait = 3;
        // Initialize to something that won't exit on the first back button press
        private DateTime LastBackbuttonPress = DateTime.Now.AddSeconds(lengthToWait*-1);

        private View _transparentPopupView = null;

        private readonly int[] _validRegionIds = new int[] {Resource.Id.MainRegion, Resource.Id.NavigationRegion, Resource.Id.PopupRegion};

        protected override void OnCreate(Android.OS.Bundle savedInstanceState)
        {
            // Set the layout
            SetContentView(Resource.Layout.MainActivity);

            // must call base implementation to create the activity before starting MvxApp.
            base.OnCreate(savedInstanceState);

            // set the click handler for the transparent popup background
            _transparentPopupView = FindViewById<FrameLayout>(Resource.Id.PopupTransparency);
            _transparentPopupView.Click += (sender, args) => ClosePopup();
            _transparentPopupView.Visibility = ViewStates.Gone;

            // register this activity with the MultiRegionPresenter.
            var presenter = Mvx.Resolve<IMultiRegionPresenter>();
            presenter.RegisterMultiRegionHost( this);

            StartMvxApp();
        }

        /// <summary>
        /// Starts the Mvx application.  
        /// 
        /// The StartupSplashSreen overrides the default implementation of TriggerFirstNavigate to do nothing, as the
        /// app must be started after the MainActivity is created.
        /// </summary>
        protected void StartMvxApp()
        {
            var starter = Mvx.Resolve<IMvxAppStart>();
            starter.Start();
        }

        // Custom back button event handling
        public override void OnBackPressed()
        {
            // TODO: Any back functionality will likely have to tie into Mvx to show view and manage history.

            //DateTime now = DateTime.Now;
            //TimeSpan offset = new TimeSpan(0, 0, lengthToWait);

            //// Close the popup if it is open
            //if (((FirstViewModel)ViewModel).ShowPopup)
            //{
            //    ((FirstViewModel)ViewModel).ClosePopupCommand();
            //}
            //// Stop app from immediately exiting unless the back button is 
            ////    pressed twice within "lengthToWait" seconds
            //else if ((now - LastBackbuttonPress).TotalSeconds > offset.TotalSeconds)
            //{
            //    // Back button was just pressed
            //    LastBackbuttonPress = now;
            //    Toast.MakeText(this, "Press the back button again to exit.", ToastLength.Long).Show();
            //}
            //else
            //{
            //    // Normal back button behavior
            //    base.OnBackPressed();
            //}
        }

        /// <summary>
        /// Shows the MvxFragment view in the region specified by its RegionAttribute.
        /// Called from the IMultiRegionPresenter.
        /// </summary>
        /// <param name="fragment"></param>
        public void Show(MvxFragment fragment)
        {
            Show( fragment, false);
        }

        protected void Show(MvxFragment fragment, bool backStack)
        {
            if (!fragment.HasRegionAttribute())
            {
                throw new InvalidOperationException( "Fragment has no region attribute.");
            }

            int regionResourceId = fragment.GetRegionId();

            if (!_validRegionIds.Contains(regionResourceId))
            {
                throw new InvalidOperationException( "Id specified in resource attribute is invalid.");
            }

            if (regionResourceId == Resource.Id.PopupRegion)
            {
                // show popup transparency
                var transparency = FindViewById(Resource.Id.PopupTransparency);
                transparency.Visibility = ViewStates.Visible;
            }

            // load fragment into view
            var ft = SupportFragmentManager.BeginTransaction();
            ft.Replace(regionResourceId, fragment);
            if (backStack)
            {
                // TODO:
                // The back button will return to this.  However,
                //   setting this may mess up the custom
                //   OnBackPressed() behavior
                ft.AddToBackStack(null);
            }
            ft.Commit();            
        }

        /// <summary>
        /// Closes the view associated with the given ViewModel.
        /// Called from the IMultiRegionPresenter.
        /// </summary>
        /// <param name="viewModel"></param>
        public void CloseViewModel(IMvxViewModel viewModel)
        {
            var fragment = GetActiveFragmentForViewModel(viewModel);
            if (fragment != null)
            {
                CloseFragment(fragment);
            }
        }

        /// <summary>
        /// Closes all active views.
        /// Called from the IMultiRegionPresenter.
        /// </summary>
        public void CloseAll()
        {
            MvxFragment fragment;
            fragment = SupportFragmentManager.FindFragmentById(Resource.Id.MainRegion) as MvxFragment;
            if (fragment != null)
            {
                CloseFragment(fragment);
            }
            fragment = SupportFragmentManager.FindFragmentById(Resource.Id.NavigationRegion) as MvxFragment;
            if (fragment != null)
            {
                CloseFragment(fragment);
            }
            fragment = SupportFragmentManager.FindFragmentById(Resource.Id.PopupRegion) as MvxFragment;
            if (fragment != null)
            {
                CloseFragment(fragment);
            }
        }

        private MvxFragment GetActiveFragmentForViewModel(IMvxViewModel viewModel)
        {
            foreach (int regionId in _validRegionIds)
            {
                MvxFragment fragment = SupportFragmentManager.FindFragmentById(regionId) as MvxFragment;
                if (fragment != null && fragment.ViewModel == viewModel)
                    return fragment;
            }

            return null;
        }

        private void CloseFragment(MvxFragment fragment)
        {
            if (fragment.GetRegionId() == Resource.Id.PopupRegion)
            {
                _transparentPopupView.Visibility = ViewStates.Gone;
            }

            var ft = SupportFragmentManager.BeginTransaction();
            ft.Remove(fragment);
            ft.Commit();
        }

        private void ClosePopup()
        {
            _transparentPopupView.Visibility = ViewStates.Gone;

            var viewFragment = SupportFragmentManager.FindFragmentById(Resource.Id.PopupRegion) as MvxFragment;
            if (viewFragment != null)
            {
                CloseFragment(viewFragment);
            }
        }

    }
}