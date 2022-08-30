using DiagnosticLabs.Constants;
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

        private string _MessageBoxColor;
        public string MessageBoxColor
        {
            get {
                if (this.NotificationMessages != null && this.NotificationMessages.Trim() != string.Empty && this.NotificationMessages != Messages.SavedSuccessfully)
                    return "#db5e5e";
                else
                    return "#ffbd80";
            }
            set { _MessageBoxColor = value; OnPropertyChanged("MessageBoxColor"); }
        }

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void ClearNotificationMessages()
        {
            this.NotificationMessages = string.Empty;
            this.NotificationMessagesVisibility = Visibility.Hidden;
        }
    }
}
