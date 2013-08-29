using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;

namespace RegionEx.Core.ViewModels
{
    public class Main1ViewModel
        : MvxViewModel
    {
        public ICommand SecondMain { get { return new MvxCommand(SecondMainCommand); } }

        public ICommand FirstNavigation { get { return new MvxCommand(FirstNavigationCommand); } }

        public ICommand FirstPopup { get { return new MvxCommand(FirstPopupCommand); } }
        public ICommand SecondPopup { get { return new MvxCommand(SecondPopupCommand); } }

        private void SecondMainCommand()
        {
            // Display the view model
            ShowViewModel<Main2ViewModel>();
        }

        private void FirstPopupCommand()
        {
            // Display the view model
            ShowViewModel<Popup1ViewModel>();
        }

        private void SecondPopupCommand()
        {
            // Display the view model
            ShowViewModel<Popup2ViewModel>();
        }

        private void FirstNavigationCommand()
        {
            // Display the view model
            ShowViewModel<Nav1ViewModel>();
        }


    }
}
