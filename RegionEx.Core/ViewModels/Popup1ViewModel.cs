using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;

namespace RegionEx.Core.ViewModels
{
    public class Popup1ViewModel
        : MvxViewModel
    {
        public ICommand ClosePopup { get { return new MvxCommand(ClosePopupCommand); } }

        /////////////////////////////////////////////////////
        /// Used to demonstrate bindings work in the layout
        /////////////////////////////////////////////////////


        private string _title = "First Popup Screen";
        public string Title
        {
            get { return _title; }
            set 
            { 
                _title = value; 
                RaisePropertyChanged(() => Title);
                RaisePropertyChanged(() => Length);
            }
        }

        public int Length
        {
            get { return _title.Length; }
        }

        public void ClosePopupCommand()
        {
            Close(this);
        }
    }
}
