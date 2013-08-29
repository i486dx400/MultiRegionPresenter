using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;

namespace RegionEx.Core.ViewModels
{
    public class Popup2ViewModel
        : MvxViewModel
    {

        public ICommand ClosePopup { get { return new MvxCommand(ClosePopupCommand); } }

        /////////////////////////////////////////////////////
        /// Used to demonstrate bindings work in the layout
        /////////////////////////////////////////////////////

        private string _title = "Second Popup Screen";
        public string Title
        {
            get { return _title; }
            set 
            { 
                _title = value; 
                RaisePropertyChanged(() => Title);
                RaisePropertyChanged(() => Both);
            }
        }

        private string _comment = "Check it out!";
        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                RaisePropertyChanged(() => Comment);
                RaisePropertyChanged(() => Both);
            }
        }


        public string Both
        {
            get { return _title + " " + _comment; }
        }

        public void ClosePopupCommand()
        {
            Close( this);
        }
    }
}
