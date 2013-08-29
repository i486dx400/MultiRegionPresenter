using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;

namespace RegionEx.Core.ViewModels
{
    public class Nav1ViewModel
        : MvxViewModel
    {
        public ICommand FirstMain { get { return new MvxCommand(FirstMainCommand); } }
        public ICommand SecondMain { get { return new MvxCommand(SecondMainCommand); } }
        public ICommand CloseNavigation { get { return new MvxCommand(CloseNavigationCommand); } }
        public ICommand CloseAllThenShowNavigation { get { return new MvxCommand(CloseAllThenShowNavigationCommand); } }

        private void FirstMainCommand()
        {
            // Display the view model
            ShowViewModel<Main1ViewModel>();
        }

        private void SecondMainCommand()
        {
            // Display the view model
            ShowViewModel<Main2ViewModel>();
        }

        private void CloseNavigationCommand()
        {
            Close(this);
        }

        private void CloseAllThenShowNavigationCommand()
        {
            ChangePresentation(new CloseAllPresentationHint());
            ShowViewModel<Nav1ViewModel>();
        }

    }
}
