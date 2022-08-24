using System;
using System.Windows.Input;

namespace DiagnosticLabs.ViewModels.Base
{
    public class RelayCommand : ICommand
    {
        #region Private Members
        private Action<object> mAction;
        #endregion

        #region Public Events
        public event EventHandler CanExecuteChanged = (sender, e) => { };
        #endregion

        #region Constructor
        public RelayCommand(Action<object> action)
        {
            mAction = action;
        }
        #endregion

        #region Command Methods
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            mAction(parameter);
        }
        #endregion
    }
}
