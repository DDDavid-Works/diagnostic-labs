using PropertyChanged;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace DiagnosticLabs.ViewModels.Base
{
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : INotifyPropertyChanged
    {
        private string _NotificationMessages;
        public string NotificationMessages
        {
            get { return _NotificationMessages; }
            set { _NotificationMessages = value; OnPropertyChanged("NotificationMessages"); }
        }

        private Visibility _NotificationMessagesVisibility;
        public Visibility NotificationMessagesVisibility
        {
            get { return this.NotificationMessages != null && this.NotificationMessages.Trim() != string.Empty ? Visibility.Visible : Visibility.Collapsed; }
            set { _NotificationMessagesVisibility = value; OnPropertyChanged("NotificationMessagesVisibility"); }
        }

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
