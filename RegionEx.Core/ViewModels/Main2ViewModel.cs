using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;

namespace RegionEx.Core.ViewModels
{
    public class Main2ViewModel
        : MvxViewModel
    {
        public ICommand FirstMain { get { return new MvxCommand(FirstMainCommand); } }

        public ICommand FirstPopup { get { return new MvxCommand(FirstPopupCommand); } }
        public ICommand SecondPopup { get { return new MvxCommand(SecondPopupCommand); } }

        private void FirstMainCommand()
        {
            // Display the view model
            ShowViewModel<Main1ViewModel>();
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
    }
}
