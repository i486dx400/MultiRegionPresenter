using System;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Exceptions;
using Cirrious.MvvmCross.Droid.Fragging.Fragments;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Droid.Views;
using Cirrious.MvvmCross.Views;
using RegionEx.Core;

namespace RegionEx.Droid
{
    /// <summary>
    /// Implemented by the activity registered to the IMultiRegionPresenter.  Will show views in regions specitied by their RegionAttribute.
    /// </summary>
    public interface IMultiRegionHost
    {
        /// <summary>
        /// Shows the MvxFragment view in the region specified by its RegionAttribute.
        /// </summary>
        /// <param name="fragment"></param>
        void Show(MvxFragment fragment);

        /// <summary>
        /// Closes the view associated with the given ViewModel.
        /// </summary>
        /// <param name="viewModel"></param>
        void CloseViewModel(IMvxViewModel viewModel);

        /// <summary>
        /// Closes all active views.
        /// </summary>
        void CloseAll();
    }

    public interface IMultiRegionPresenter : IMvxAndroidViewPresenter
    {
        /// <summary>
        /// Allows the IMultiRegionHost to regist itself with the presenter.  This is required as the presenter is constructed before the IMultiRegionHost activity and
        /// the activity is created outside of the Mvx IoC.
        /// </summary>
        void RegisterMultiRegionHost(IMultiRegionHost host);
    }

    /// <summary>
    /// Presenter that displays MvxFragment views in specified regions of an associated IMultiRegionHost.  Views should be tagged with a RegionAttribute to indicate
    /// the region for display.
    /// </summary>
    class MultiRegionPresenter : MvxAndroidViewPresenter, IMultiRegionPresenter
    {
        private IMultiRegionHost _host = null;

        /// <summary>
        /// Allows the IMultiRegionHost to regist itself with the presenter.  This is required as the presenter is constructed before the IMultiRegionHost activity and
        /// the activity is created outside of the Mvx IoC.
        /// </summary>
        /// <param name="host"></param>
        public void RegisterMultiRegionHost(IMultiRegionHost host)
        {
            _host = host;
        }

        /// <summary>
        /// Shows the MvxViewModelRequest.  If the associated view is tagged with a RegionAttribute it is shown in the IMultiRegionHost.  If not, it is shown using the
        /// default Android presentation.
        /// </summary>
        /// <param name="request"></param>
        public override void Show(MvxViewModelRequest request)
        {
            var fragment = CreateView(request);

            if( _host != null && fragment.HasRegionAttribute())
            {
                // view has region attribute - show in the fragment host
                _host.Show(fragment);
            }
            else
            {
                // view has no region attribute - use default MvxAndroidViewPresenter Show implementation
                base.Show(request);                
            }
        }

        public override void ChangePresentation(MvxPresentationHint hint)
        {
            if (hint is MvxClosePresentationHint)
            {
                _host.CloseViewModel(((MvxClosePresentationHint)hint).ViewModelToClose);
            }
            else if (hint is CloseAllPresentationHint)
            {
                _host.CloseAll();
            }

            base.ChangePresentation(hint);
        }
        
        /// <summary>
        /// Creates a MvxFragment view initialized with the associated ViewModel.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        // modified from Cirrious.MvvmCross.Wpf.Views.MvxWpfViewsContainer
        public MvxFragment CreateView(MvxViewModelRequest request)
        {
            var viewFinder = Mvx.Resolve<IMvxViewsContainer>();          

            var viewType = viewFinder.GetViewType(request.ViewModelType);
            if (viewType == null)
                throw new MvxException("View Type not found for " + request.ViewModelType);

            // , request
            var viewObject = Activator.CreateInstance(viewType);
            if (viewObject == null)
                throw new MvxException("View not loaded for " + viewType);

            var fragment = viewObject as MvxFragment;
            if (fragment == null)
                throw new MvxException("Loaded View is not a MvxFragment " + viewType);

            var viewModelLoader = Mvx.Resolve<IMvxViewModelLoader>();
            fragment.ViewModel = viewModelLoader.LoadViewModel(request, null);

            return fragment;
        }
    }
}